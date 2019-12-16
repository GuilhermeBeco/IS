using System;
using System.Collections.Generic;
using System.Linq;
using IPLeiriaSmartCampus.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using uPLibrary.Networking.M2Mqtt;
using System.Text;

namespace IPLeiriaSmartCampus.Controllers
{
    public class SensorController : ApiController
    {
        static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["IPLeiriaSmartCampus.Properties.Settings.ConnStr"].ConnectionString;
        MqttClient mcClient = new MqttClient(IPAddress.Parse("127.0.0.1"));

        string topic =  "newSensorsInsertIS";


        [Route("api/sensors/all")]
        [HttpPost]
        public IHttpActionResult GetAllSensors(GetAllSensors response)
        {
            //todo orientar o realtime do show data e do alert data para sensores novos
            List<Sensor> sensors = new List<Sensor>();
            if (response.cred != null && UserController.ValidateUser(response.cred))
            {
                string query = "Select * from sensor where id >= @id";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    try
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@id", response.SensorID);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Sensor sensor = new Sensor();
                            sensor.SensorID = int.Parse(reader["id"].ToString());
                            sensor.Local = reader["local"].ToString();

                            sensors.Add(sensor);
                        }
                        reader.Close();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }

                return Ok(sensors);//Respecting HTTP errors (200 OK)
            }
            return BadRequest("Não Autenticado");
        }

        [Route("api/sensors/authed/")]
        [HttpPost]
        public IHttpActionResult GetAllSensorsLoad([FromBody] CredMod mod)
        {
            List<Sensor> sensors = new List<Sensor>();
            string query = "Select * from sensor";
            if (mod.cred != null && UserController.ValidateUser(mod.cred))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Sensor sensor = new Sensor();
                            sensor.SensorID = int.Parse(reader["id"].ToString());
                            sensor.Local = reader["local"].ToString();
                            if (reader["username"].ToString().Equals(""))
                            {
                                sensor.username = "N/A";
                            }
                            else
                            {
                                sensor.username = reader["username"].ToString();
                            }

                            sensors.Add(sensor);
                        }
                        reader.Close();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }

                return Ok(sensors);//Respecting HTTP errors (200 OK)
            }
            return BadRequest("Não Autenticado");
        }

        [Route("api/sensors/")]
        [HttpPost]
        public IHttpActionResult PostSensor(Sensor sensor)
        {
            int rows = 0;
            if (sensor.cred != null && UserController.ValidateUser(sensor.cred))
            {
                if (!SensorExists(sensor.SensorID))

                {
                    string query = "Insert into dbo.sensor (username, id, local) values (@username,@id, @local)";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@id", sensor.SensorID);
                        command.Parameters.AddWithValue("@local", sensor.Local);
                        if (UserController.findUser(sensor.username) != null)
                        {
                            command.Parameters.AddWithValue("@username", sensor.username);
                        }
                        else if (UserController.findUser("N/A") == null)
                        {
                            UserController.generateEmptyUser();
                            command.Parameters.AddWithValue("@username", "N/A");
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@username", "N/A");
                        }

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
                        mcClient.Connect(Guid.NewGuid().ToString());
                        if (!mcClient.IsConnected)
                        {
                            Console.WriteLine("Error connecting to message broker...");

                        }
                        mcClient.Publish("newSensorsInsertIS", Encoding.UTF8.GetBytes(sensor.SensorID.ToString()));


                    }
                    return Ok(rows);//Respecting HTTP errors (200 OK)
                }
                else
                {
                    return BadRequest("Sensor already exists");
                }
            }
            return BadRequest("Não Autenticado");
        }

        public static bool SensorExists(int id)
        {
            Sensor sensor = new Sensor();
            string query = "Select id from sensor where id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
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

        public bool hasUser(int id)
        {
            string query = "select username from sensor where id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string n = reader["username"].ToString();
                        if (n.Equals("N/A"))
                        {
                            return false;
                        }

                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return true;
        }

        [Route("api/sensors/")]
        [HttpPut]
        public IHttpActionResult PutUser(JsonResponseSensor response)
        {
            int rows = 0;
            if (response.cred != null && UserController.ValidateUser(response.cred))
            {
                if (!hasUser(response.SensorID) && SensorExists(response.SensorID) && UserController.findUser(response.username) != null)
                {
                    string query = "Update sensor set username = @user where id = @id";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@id", response.SensorID);
                        command.Parameters.AddWithValue("@user", response.username);

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
                    return Ok(rows);//Respecting HTTP errors (200 OK)
                }
                else
                {
                    return BadRequest("O sensor já tem um utilizador ou o sensor não existe ou o utilizador não existe");
                }
            }
            return BadRequest("Não Autenticado");
        }

    }
}
