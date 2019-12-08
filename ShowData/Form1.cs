using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace ShowData
{
    public partial class Form1 : Form
    {
        
        List<Sensor> list = new List<Sensor>();
        List<AQ> aqs = new List<AQ>();
        Task<string> s;
        AQ aq;
        int idRecv = 0;
        MqttClient mcClient;
        Sensor sensorAtual;
        string topic = "dataISMosquittoTest";
        BindingSource bindingSource = new BindingSource();

        private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            idRecv = int.Parse(Encoding.UTF8.GetString(e.Message));
            
            updateTable();
        }

        public Form1()
        {
            InitializeComponent();
            mcClient = new MqttClient("test.mosquitto.org");
            mcClient.Connect(Guid.NewGuid().ToString());
            if (!mcClient.IsConnected)
            {
                Console.WriteLine("Error connecting to message broker...");
                return;
            }
            mcClient.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            mcClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
        }


        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (mcClient.IsConnected)
            {
                mcClient.Unsubscribe(new string[] { topic }); //Put this in a button to see notif!
                mcClient.Disconnect(); //Free process and process's resources
            }
        }

        private void itemChanged (object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            int index = 0;
            index = comboBox.SelectedIndex;
            sensorAtual = list[index];
            JsonSensorData d = new JsonSensorData();
            d.SensorID = sensorAtual.SensorID;
            d.start = "";
            d.end = "";
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(d);            
            string res = Post("https://localhost:44327/api/aq/all",data,"application/json");
            aqs.Clear();
            aqs= Newtonsoft.Json.JsonConvert.DeserializeObject<List<AQ>>(res);
            updateUI();
            
           

        }
        private void updateUI()
        {
            //richTextBoxData.Text = "";
            foreach(AQ aq in aqs)
            {
               // richTextBoxData.Text += aq.ToString();
            }
            labelBatteryValue.Text = aqs[aqs.Count - 1].Battery.ToString();
            labelTemperatureValue.Text = aqs[aqs.Count - 1].Temperature.ToString();
            labelHumidityValue.Text = aqs[aqs.Count - 1].Humidity.ToString();
            labelDataValue.Text = aqs[aqs.Count - 1].Timestamp.ToString();
            Console.WriteLine("AQS len = " + aqs.Count);
        }
 
        private void updateTable()
        {
            JsonSensorData d = new JsonSensorData();
            d.SensorID = sensorAtual.SensorID;
            d.start = "";
            d.end = "";
            d.AQID = idRecv;
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(d);
            string res = Post("https://localhost:44327/api/aq/all", data, "application/json");
            List<AQ> aux = new List<AQ>();
            aux = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AQ>>(res);
            foreach(AQ a in aux)
            {
                Console.WriteLine(a.Id);
                aqs.Add(a);
            }
            Console.WriteLine("após o updateTable: " + aqs.Count);
            updateUI();
            
           
            Console.WriteLine(d.AQID);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            s = GetAsync("https://localhost:44327/api/sensors/");
            list= Newtonsoft.Json.JsonConvert.DeserializeObject<List<Sensor>>(s.Result);
            comboBoxSensors.Items.Clear();
            foreach (Sensor sen in list)
            {                
                comboBoxSensors.Items.Add(sen);
            }
            richTextBoxData.Text = "";
            labelBatteryValue.Text = "";
            labelTemperatureValue.Text = "";
            labelHumidityValue.Text = "";
            labelDataValue.Text = "";
            comboBoxSensors.SelectedIndexChanged += new System.EventHandler(itemChanged);
            
       
        }

        public Task<string> GetAsync(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return  reader.ReadToEndAsync();
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
    }
}
