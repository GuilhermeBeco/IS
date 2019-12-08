using IPLeiriaSmartCampus.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace IPLeiriaSmartCampus.Controllers
{
  
    public class AQController : ApiController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["IPLeiriaSmartCampus.Properties.Settings.ConnStr"].ConnectionString;
        MqttClient mcClient = new MqttClient(IPAddress.Parse("2001:41d0:a:fed0::1"));

        string[] topics = { "dataISMosquittoTest" };
 
        [Route("api/aq/")]
        public IHttpActionResult GetLastAQ()
        {
            AQ aq = new AQ();
            string query = "Select top 1 * from AQ order by timestamp desc ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        aq.Id = int.Parse(reader["id"].ToString());
                        aq.SensorID = int.Parse(reader["SensorID"].ToString());
                        aq.Temperature = float.Parse(reader["Temperature"].ToString());
                        aq.Humidity = float.Parse(reader["Humidity"].ToString());
                        aq.Battery = int.Parse(reader["Battery"].ToString());
                        aq.Timestamp = DateTime.Parse(reader["Timestamp"].ToString());

                    }
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            return Ok(aq);//Respecting HTTP errors (200 OK)
        }

        [Route("api/ok")]
        public IHttpActionResult ServerStatus(int sensor)
        {
            return Ok();
        }

        // owin (tokens)

        [Route("api/aq/{sensor}")]
        public IHttpActionResult GetLastAQSensor(int sensor)
        {
            AQ aq = new AQ();
            string query = "Select top 1 * from AQ where sensorid = @sensor order by timestamp desc";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@sensor", sensor);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    int o = 0;
                    while (reader.Read())
                    {
                        aq.Id = int.Parse(reader["id"].ToString());
                        aq.SensorID = int.Parse(reader["SensorID"].ToString());
                        aq.Temperature = float.Parse(reader["Temperature"].ToString());
                        aq.Humidity = float.Parse(reader["Humidity"].ToString());
                        aq.Battery = int.Parse(reader["Battery"].ToString());
                        aq.Timestamp = DateTime.Parse(reader["Timestamp"].ToString());

                    }
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            return Ok(aq);//Respecting HTTP errors (200 OK)
        }

        [Route("api/aq/all")]
        [HttpPost]
        
        public IHttpActionResult GetAllAqSensorFilter(JSONResponse response)
        {
            List<AQ> aqs = new List<AQ>();
            string query = "";
            int aqId = 0;
            bool filter = false;
            if (response.start != "" && response.end != "")
            {
                if (response.AQID != 0)
                {
                    if(response.SensorID!=0)
                        query = "Select  * from AQ where sensorid = @sensor and id >= @id and timestamp between @start and @end order by timestamp desc ";
                    else
                        query = "Select  * from AQ where id >= @id and timestamp between @start and @end order by timestamp desc ";
                    aqId = response.AQID;
                }
                else
                {
                    if(response.SensorID!=0)
                        query = "Select  * from AQ where sensorid = @sensor and timestamp between @start and @end order by timestamp desc ";
                    else
                        query = "Select  * from AQ where timestamp between @start and @end order by timestamp desc ";
                }
                filter = true;
            }
            else
            {
                if (response.AQID != 0)
                {
                    if(response.SensorID!=0)
                        query = "Select * from AQ  where sensorid = @sensor and id >= @id";
                    else
                        query = "Select * from AQ  where id >= @id";
                    aqId = response.AQID;
                }
                else
                {   
                    if(response.SensorID!=0)
                        query = "Select * from AQ  where sensorid = @sensor ";
                    else
                        query = "Select * from AQ ";
                }
                filter = false;
            }
           
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                if (response.SensorID != 0) 
                    command.Parameters.AddWithValue("@sensor", response.SensorID);
                if (filter)
                {
                    System.DateTime dtDateTimeStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                    dtDateTimeStart = dtDateTimeStart.AddSeconds(double.Parse(response.start)).ToLocalTime();
                    System.DateTime dtDateTimeEnd = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                    dtDateTimeEnd = dtDateTimeEnd.AddSeconds(double.Parse(response.end)).ToLocalTime();
                    command.Parameters.AddWithValue("@start", dtDateTimeStart);
                    command.Parameters.AddWithValue("@end", dtDateTimeEnd);
                }
                if (aqId != 0)
                {
                    command.Parameters.AddWithValue("@id", aqId);
                }
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        AQ aq = new AQ();
                        aq.Id = int.Parse(reader["id"].ToString());
                        aq.SensorID = int.Parse(reader["SensorID"].ToString());
                        aq.Temperature = float.Parse(reader["Temperature"].ToString());
                        aq.Humidity = float.Parse(reader["Humidity"].ToString());
                        aq.Battery = int.Parse(reader["Battery"].ToString());
                        aq.Timestamp = DateTime.Parse(reader["Timestamp"].ToString());
                        aqs.Add(aq);
                    }
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return Ok(aqs);//Respecting HTTP errors (200 OK)
        }


        [Route("api/aq/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteAQ(int id)
        {
            int rows = 0;
            string query = "delete from AQ where id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    connection.Open();
                    rows = command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return Ok(rows);
        }

        public int getId()
        {
            int id = 0;
            string querySelect = "Select top 1 id from AQ order by id desc";
            using (SqlConnection connection = new SqlConnection(connectionString))
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

        [Route("api/aq/burst/")]
        [HttpPost]
        public IHttpActionResult PostAQS([FromBody]List<AQMap> dadosSensores)
        {
            string topic = "dataISMosquittoTest";
            int rows = 0;
            int fails = 0;
            int id = getId();
            int idMqtt = id++;
            string query = "Insert into dbo.AQ (SensorID, Temperature, Humidity, Battery, Timestamp,id ) values (@Sensor, @Temp, @Hum, @Bat, @Time, @id)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                foreach (AQMap aq in dadosSensores)
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    if (SensorController.SensorExists(aq.SensorID))
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
                    }
                }
                mcClient.Connect(Guid.NewGuid().ToString());
                if (!mcClient.IsConnected)
                {
                    Console.WriteLine("Error connecting to message broker...");

                }
                mcClient.Publish("data", Encoding.UTF8.GetBytes(idMqtt.ToString()));
                
                if (mcClient.IsConnected)
                {
                    mcClient.Unsubscribe(new string[] { topic }); //Put this in a button to see notify!
                    mcClient.Disconnect();
                }
                return Ok("Foram inseridas " + rows + " linhas e falharam " + fails + " pois não existem alguns sensores");
            }
        }


        [Route("api/aq/")]
        [HttpPost]
        public IHttpActionResult PostAQ(AQMap aq)
        {
            string topic = "dataISMosquittoTest";
            int rows = 0;
            int id = getId();
            if (SensorController.SensorExists(aq.SensorID))
            {
                string query = "Insert into AQ (id,SensorID, Temperature, Humidity, Battery, Timestamp ) values (@id,@Sensor, @Temp, @Hum, @Bat, @Time)";
                using (SqlConnection connection = new SqlConnection(connectionString))
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
                }

                mcClient.Connect(Guid.NewGuid().ToString());
                if (!mcClient.IsConnected)
                {
                    Console.WriteLine("Error connecting to message broker...");

                }
                System.Diagnostics.Debug.WriteLine(mcClient.Publish("data", Encoding.UTF8.GetBytes(id.ToString())));
                System.Diagnostics.Debug.WriteLine(id.ToString());
              /*  if (mcClient.IsConnected)
                {
                    mcClient.Unsubscribe(new string[] { topic }); //Put this in a button to see notify!
                    mcClient.Disconnect();
                }*/
                return Ok(rows);
            }
            else
            {
                return BadRequest("O sensor não existe");
            }
        }

    }
}
