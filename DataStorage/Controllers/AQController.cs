using DataStorage.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace DataStorage.Controllers
{
    public class AQController : ApiController
    {
        MqttClient mcClient = new MqttClient(IPAddress.Parse("127.0.0.1"));
        List<AQMap> aqsSemSensor = new List<AQMap>();
        static string connectionStringDS = System.Configuration.ConfigurationManager.ConnectionStrings["DataStorage.Properties.Settings.ConnStr"].ConnectionString;
        string[] topics = { "dataISMosquittoTest" };

        public static bool SensorExists(int id)
        {
            Sensor sensor = new Sensor();
            string query = "Select id from sensor where id = @id";
            using (SqlConnection connection = new SqlConnection(connectionStringDS))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        if (int.Parse(reader["id"].ToString()) == id)
                        {
                            return true;
                        }

                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return false;

            }
        }
        public int getId()
        {
            int id = 0;
            string querySelect = "Select top 1 id from AQ order by id desc";
            using (SqlConnection connection = new SqlConnection(connectionStringDS))
            {
                SqlCommand command = new SqlCommand(querySelect, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        id = int.Parse(reader["id"].ToString());
                    }
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return id;
        }

        [Route("api/ok")]
        public IHttpActionResult ServerStatus()
        {
            return Ok();
        }


        [Route("api/aq/burst/")]
        [HttpPost]
        public IHttpActionResult PostAQS([FromBody]List<AQMap> dadosSensores)
        {
            string topic = "dataISMosquittoTest";
            string topicSensor = "newSensorsInsertIS";
            int rows = 0;
            int fails = 0;
            int id = getId();
            int idMqtt = id++;
            string query = "Insert into dbo.AQ (SensorID, Temperature, Humidity, Battery, Timestamp,id ) values (@Sensor, @Temp, @Hum, @Bat, @Time, @id)";
            using (SqlConnection connection = new SqlConnection(connectionStringDS))
            {
                foreach (AQMap aq in dadosSensores)
                {

                    SqlCommand command = new SqlCommand(query, connection);
                    if (SensorExists(aq.SensorID))
                    {
                        System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                        dtDateTime = dtDateTime.AddSeconds(double.Parse(aq.Timestamp)).ToLocalTime();
                        id++;
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@Sensor", aq.SensorID);
                        command.Parameters.AddWithValue("@Temp", aq.Temperature);
                        command.Parameters.AddWithValue("@Hum", aq.Humidity);
                        command.Parameters.AddWithValue("@Bat", aq.Battery);
                        command.Parameters.AddWithValue("@Time", dtDateTime);

                        try
                        {
                            connection.Open();
                            rows = rows + command.ExecuteNonQuery();
                            connection.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);

                        }

                    }
                    else
                    {
                        fails++;
                        aqsSemSensor.Add(aq);
                    }
                }
                mcClient.Connect(Guid.NewGuid().ToString());
                if (!mcClient.IsConnected)
                {
                    Console.WriteLine("Error connecting to message broker...");

                }
                mcClient.Publish("dataISMosquittoTest", Encoding.UTF8.GetBytes(idMqtt.ToString()));

                mcClient.Subscribe(new string[] { topicSensor }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
                mcClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
                return Ok("Foram inseridas " + rows + " linhas e falharam " + fails + " pois não existem alguns sensores");
            }
        }

        private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Debug.WriteLine("Vim ao evento");
            int id = getId();
            string topic = "dataISMosquittoTest";
            int idMqtt = id++;
            int rows = 0;
            try
            {
                foreach (AQMap aq in aqsSemSensor)
                {
                    if (SensorExists(aq.SensorID))
                    {
                        string query = "Insert into AQ (id,SensorID, Temperature, Humidity, Battery, Timestamp ) values (@id,@Sensor, @Temp, @Hum, @Bat, @Time)";
                        using (SqlConnection connection = new SqlConnection(connectionStringDS))
                        {
                            SqlCommand command = new SqlCommand(query, connection);
                            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                            dtDateTime = dtDateTime.AddSeconds(double.Parse(aq.Timestamp)).ToLocalTime();
                            id++;
                            command.Parameters.AddWithValue("@id", id);
                            command.Parameters.AddWithValue("@Sensor", aq.SensorID);
                            command.Parameters.AddWithValue("@Temp", aq.Temperature);
                            command.Parameters.AddWithValue("@Hum", aq.Humidity);
                            command.Parameters.AddWithValue("@Bat", aq.Battery);
                            command.Parameters.AddWithValue("@Time", dtDateTime);

                            try
                            {
                                connection.Open();
                                rows += command.ExecuteNonQuery();
                                connection.Close();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            aqsSemSensor.Remove(aq);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            mcClient.Connect(Guid.NewGuid().ToString());
            if (!mcClient.IsConnected)
            {
                Console.WriteLine("Error connecting to message broker...");
            }
            mcClient.Publish("dataISMosquittoTest", Encoding.UTF8.GetBytes(idMqtt.ToString()));

            if (mcClient.IsConnected)
            {
                mcClient.Unsubscribe(new string[] { topic }); //Put this in a button to see notify!
                mcClient.Disconnect();
            }
        }
    }
}
