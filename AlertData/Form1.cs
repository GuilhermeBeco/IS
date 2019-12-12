﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace AlertData
{
    public partial class Form1 : Form
    {
        List<Sensor> list = new List<Sensor>();
        List<AQ> aqs = new List<AQ>();
        List<Trigger> triggers = new List<Trigger>();
        Task<string> s;
        FileManager fm = new FileManager("triggers.txt");
        Sensor sensorAtual;
        string topic = "dataISMosquittoTest";
        string topicSensors = "newSensorsInsertIS";
        private MqttClient mcClient;
        int idRecv = 0;
        private string username;
        private string password;
        private bool authed = false;
        private string cred;

        private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            idRecv = int.Parse(Encoding.UTF8.GetString(e.Message));
            if (e.Topic == topic)
            {
                JsonSensorData d = new JsonSensorData();
                d.SensorID = 0;
                d.start = "";
                d.end = "";
                d.AQID = idRecv;
                string data = Newtonsoft.Json.JsonConvert.SerializeObject(d);
                string res = Post("https://localhost:44327/api/aq/all", data, "application/json"); //mudar para o post com cred
                List<AQ> aux = new List<AQ>();
                aux = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AQ>>(res);
                foreach (AQ a in aux)
                {
                    Console.WriteLine(a.Id);
                    aqs.Add(a);
                }
                processaTriggers();
            }
            else
            {
                comboBoxSensors.Items.Clear();
                loadSensors();
            }
        }

        public Form1()
        {
            InitializeComponent();
            //login
            mcClient = new MqttClient(IPAddress.Parse("127.0.0.1"));
            mcClient.Connect(Guid.NewGuid().ToString());
            if (!mcClient.IsConnected)
            {
                Console.WriteLine("Error connecting to message broker...");
                return;
            }
            mcClient.Subscribe(new string[] { topic, topicSensors }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            mcClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
        }

        public void sendEmail(string body, string email)
        {
            //debug de email
            MailMessage o = new MailMessage("IStesting@outlook.pt", email, "Alerta de valores", body);
            NetworkCredential netCred = new NetworkCredential("IStesting@outlook.pt", "IsWebService");
            SmtpClient smtpobj = new SmtpClient("SMTP.office365.com", 587);
            smtpobj.EnableSsl = true;
            smtpobj.Credentials = netCred;
            try
            {
                smtpobj.Send(o);
            } catch (Exception e)
            {
                Console.WriteLine(o.ToString());
                Console.WriteLine(body);
            }

        }


        private void processaTriggers()
        {
            foreach (Trigger t in triggers)
            {
                foreach (AQ a in aqs)
                {
                    if (t.SensorID == a.SensorID)
                    {
                        if (t.campo == "Temperature")
                        {
                            if (t.operacao == "<")
                            {
                                if (a.Temperature < t.valor)
                                {
                                    sendEmail("A temperatura no sensor" + t.SensorID + " está < que " + t.valor, t.email);
                                }
                            }
                            else if (t.operacao == ">")
                            {
                                if (a.Temperature > t.valor)
                                {
                                    sendEmail("A temperatura no sensor" + t.SensorID + " está > que " + t.valor, t.email);
                                }
                            }
                            else
                            {
                                if (a.Temperature == t.valor)
                                {
                                    sendEmail("A temperatura no sensor" + t.SensorID + " está = a " + t.valor, t.email);
                                }
                            }
                        }
                        else if (t.campo == "Humidity")
                        {
                            if (t.operacao == "<")
                            {
                                if (a.Humidity < t.valor)
                                {
                                    sendEmail("A humidade no sensor" + t.SensorID + " está < que " + t.valor, t.email);
                                }
                            }
                            else if (t.operacao == ">")
                            {
                                if (a.Humidity > t.valor)
                                {
                                    sendEmail("A humidade no sensor" + t.SensorID + " está > que " + t.valor, t.email);
                                }
                            }
                            else
                            {
                                if (a.Humidity == t.valor)
                                {
                                    sendEmail("A humidade no sensor" + t.SensorID + " está = a " + t.valor, t.email);
                                }
                            }
                        }
                        else
                        {
                            if (t.operacao == "<")
                            {
                                if (a.Battery < t.valor)
                                {
                                    sendEmail("A bateria do sensor" + t.SensorID + " está < que " + t.valor, t.email);
                                }
                            }
                            else if (t.operacao == ">")
                            {
                                if (a.Battery > t.valor)
                                {
                                    sendEmail("A bateria do sensor" + t.SensorID + " está > que " + t.valor, t.email);
                                }
                            }
                            else
                            {
                                if (a.Battery == t.valor)
                                {
                                    sendEmail("A bateria do sensor" + t.SensorID + " está = a " + t.valor, t.email);
                                }
                            }
                        }
                    }
                }
            }
            aqs.Clear();
        }
        private void loadSensors()
        {
            s = GetAsync("https://localhost:44327/api/sensors/");//mudar para o post com cred
            list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Sensor>>(s.Result);
            comboBoxSensors.Items.Clear();
            foreach (Sensor sen in list)
            {
                comboBoxSensors.Items.Add(sen);
            }
            triggers = fm.getTriggers();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            buttonCreateTrigger.Click += ButtonCreateTrigger_Click;
            buttonSaveAll.Click += ButtonSaveAll_Click;
        }

        private void ButtonSaveAll_Click(object sender, EventArgs e)
        {
            fm.writeTriggers(triggers);
        }

        private void ButtonCreateTrigger_Click(object sender, EventArgs e)
        {
            int index = 0;
            index = comboBoxSensors.SelectedIndex;
            sensorAtual = list[index];
            Trigger t = new Trigger();
            t.SensorID = sensorAtual.SensorID;
            t.operacao = comboBoxOperacao.SelectedItem.ToString();
            t.campo = comboBoxCampos.SelectedItem.ToString();
            t.valor = float.Parse(textBoxValor.Text);
            t.email = textBoxEmail.Text;
            triggers.Add(t);
        }
        //todo config mqtt e fazer a função do evento dele (Acrescentar o obter aqs nas ultimas 24h??)

        public Task<string> GetAsync(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEndAsync();
            }
        }
        public string Post(string uri, string data, string contentType, string method = "POST")
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.ContentLength = dataBytes.Length;
            request.ContentType = contentType;
            request.Method = method;

            using (Stream requestBody = request.GetRequestStream())
            {
                requestBody.Write(dataBytes, 0, dataBytes.Length);
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private void Form1_closing(object sender, FormClosingEventArgs e)
        {
            if (mcClient.IsConnected)
            {
                mcClient.Unsubscribe(new string[] { topic }); //Put this in a button to see notif!
                mcClient.Disconnect(); //Free process and process's resources
            }
        }
       
       
        private void button1_Click(object sender, EventArgs e)
        {
            username = textBoxUsername.Text;
            password = textBoxPassword.Text;
            string data = "{ username: \"" + username + "\", password: \"" + password + "\" }";
            string response = Post("https://localhost:44327/api/users/auth", data, "application/json");
            authed = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(response);
            if (authed)
            {
                labelLogin.Text = "Authed";
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(username+":"+password);
                cred= System.Convert.ToBase64String(plainTextBytes);
                loadSensors();
            }
            else
                labelLogin.Text = "Fail";
        }       
    }
}

