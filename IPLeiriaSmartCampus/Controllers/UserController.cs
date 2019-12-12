using IPLeiriaSmartCampus.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace IPLeiriaSmartCampus.Controllers
{
    public class UserController : ApiController
    {
        static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["IPLeiriaSmartCampus.Properties.Settings.ConnStr"].ConnectionString;

        
        [Route("api/users")]
        [HttpDelete]
        public IHttpActionResult DeleteUser(string username, string password)
        {
            int rows = 0;
            User toDelete = findUser(username);
            if (toDelete != null)
            {
                var data = Encoding.UTF8.GetBytes(password);
                var sha1 = new SHA1CryptoServiceProvider();
                var sha1data = sha1.ComputeHash(data);
                string hashedPass = Encoding.UTF8.GetString(sha1data);
                if (hashedPass.Equals(toDelete.Password))
                {
                    string query = "delete from users where username = @username";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@username", username);
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
                        return Ok(rows);
                    }
                }
                else
                {
                    return BadRequest("A password não está certa");
                }
            }
            else
            {
                return BadRequest("O user não existe");
            }
        }


        [Route("api/users")]
        [HttpPost]
        public IHttpActionResult GetAllUsers(string cred)
        {
            List<User> users = new List<User>();
            if (cred != null && ValidateUser(cred))
            {
                string query = "Select username,name from users";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            User u = new User();
                            u.Username = reader["username"].ToString();
                            u.Name = reader["name"].ToString();
                            users.Add(u);
                        }
                        reader.Close();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
                return Ok(users);
            }
            return BadRequest("Não Autenticado");
        }

        [Route("api/users")]
        [HttpPost]
        public IHttpActionResult CreateUser(UserMapper user)
        {
            int rows = 0;
            if (user.cred != null && ValidateUser(user.cred))
            {
                if (findUser(user.Username) == null && user.Password == user.PasswordConfirmation)
                {
                    string query = "insert into users (username,password,name) values (@username,@password,@name) ";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        var sha1 = new SHA1CryptoServiceProvider();
                        var data = Encoding.UTF8.GetBytes(user.Password);
                        byte[] sha1data = sha1.ComputeHash(data);
                        string hashedPass = Encoding.UTF8.GetString(sha1data);
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@username", user.Username);
                        command.Parameters.AddWithValue("@name", user.Name);
                        command.Parameters.AddWithValue("@password", hashedPass);
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
                    return Ok(rows);
                }
                else
                {
                    return BadRequest("O username já existe ou a password não está confirmada");
                }
            }
            return BadRequest("Não Autenticado");
        }
      

        public static User findUser(string username)
        {
            string query = "select * from users where username = @username";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        User u = new User();
                        u.Username = reader["username"].ToString();
                        u.Password = reader["password"].ToString();
                        u.Name = reader["name"].ToString();
                        return u;
                    }
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return null;
        }

        public static void generateEmptyUser()
        {
            string query = "insert into users (username,password,name) values (@username,@password,@name)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", "N/A");
                command.Parameters.AddWithValue("@password", "N/A");
                command.Parameters.AddWithValue("@name", "N/A");
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static bool ValidateUser(string ba)
        {
            string decoded;
            byte[] data = System.Convert.FromBase64String(ba);
            decoded = System.Text.UTF8Encoding.UTF8.GetString(data);
            string[] cred = decoded.Split(':');
            string username = cred[0];
            string password = cred[1];
            User user = findUser(username);
            if (user != null)
            {
                var sha1 = new SHA1CryptoServiceProvider();
                var dataHash = Encoding.UTF8.GetBytes(password);
                byte[] sha1data = sha1.ComputeHash(dataHash);
                string hashedPass = Encoding.UTF8.GetString(sha1data);

                Debug.WriteLine("hashedPass do creed: "+hashedPass+" | hashedPass do user: "+ Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(user.Password)));
                if (user.Password.Equals(hashedPass))
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        [Route("api/users/auth")]
        [HttpPost]
        public IHttpActionResult authUser(AuthModel model)
        {
            bool res = false;
            User user = findUser(model.username);
            if (user != null)
            {
                var sha1 = new SHA1CryptoServiceProvider();
                var dataHash = Encoding.UTF8.GetBytes(model.password);
                byte[] sha1data = sha1.ComputeHash(dataHash);
                string hashedPass = Encoding.UTF8.GetString(sha1data);

                Debug.WriteLine("hashedPass do creed: " + hashedPass + " | hashedPass do user: " + Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(user.Password)));
                if (user.Password.Equals(hashedPass))
                {
                    res = true;
                    return Ok(res);
                }
                return Ok(res);
            }
            return Ok(res);
        }


    }
}
