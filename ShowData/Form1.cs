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
using System.Windows.Forms.DataVisualization.Charting;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace ShowData
{
    public delegate void updateUIDel();
    public partial class Form1 : Form
    {
        string username = "";
        string password = "";
        bool authed = false;
        List<Sensor> list = new List<Sensor>();
        List<AQ> aqs = new List<AQ>();
        Task<string> s;
        AQ aq;
        int idRecv = 0;
        MqttClient mcClient;
        Sensor sensorAtual;
        string topic = "dataISMosquittoTest";
        string topicSensors = "newSensorsInsertIS";
        BindingSource bindingSource = new BindingSource();
        bool updateSensors = false;
        bool updateBox = false;
        private string cred;
        
        private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            idRecv = int.Parse(Encoding.UTF8.GetString(e.Message));
            if (e.Topic == topic) {
                if(authed)
                updateTable();
            }

            else
            {
                if (authed)
                {
                    list.Clear();
                    loadSensors();
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
            mcClient = new MqttClient(IPAddress.Parse("127.0.0.1"));
            mcClient.Connect(Guid.NewGuid().ToString());
            if (!mcClient.IsConnected)
            {
                Console.WriteLine("Error connecting to message broker...");
                return;
            }
            mcClient.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            mcClient.Subscribe(new string[] { topicSensors }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
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
            d.AQID = 0;
            d.cred = cred;
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(d);            
            string res = Post("https://localhost:44327/api/aq/all",data,"application/json");
            aqs.Clear();
            aqs= Newtonsoft.Json.JsonConvert.DeserializeObject<List<AQ>>(res);
            richTextBoxData.Text = "";
            
            updateUI();
            
            updateBox = true;
        }

        private void updateUI()
        {

            chart1.Invoke((MethodInvoker)delegate
            {
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "yyyy/MM/dd   HH:mm";
                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Hours;
                chart1.Series["Temperature"].Points.Clear();
                chart1.Series["Humidity"].Points.Clear();
            });
           
            foreach (AQ aq in aqs)
            {
                richTextBoxData.Invoke((MethodInvoker)delegate { richTextBoxData.Text += aq.ToString(); });
                chart1.Invoke((MethodInvoker)delegate {
                    chart1.Series["Temperature"].Points.AddXY(aq.Timestamp, aq.Temperature);
                    chart1.Series["Humidity"].Points.AddXY(aq.Timestamp, aq.Humidity);
                });
                
            }
            labelBatteryValue.Invoke((MethodInvoker)delegate
            {
                try
                {
                    labelBatteryValue.Text = aqs[aqs.Count - 1].Battery.ToString();
                }catch(Exception e)
                {
                    labelBatteryValue.Text = "Sem dados";
                }
            });
            labelDataValue.Invoke((MethodInvoker)delegate
            {
                try
                {
                    labelDataValue.Text = aqs[aqs.Count - 1].Timestamp.ToString();
                }catch(Exception e)
                {
                    labelDataValue.Text = "Sem dados";
                }
            });
            labelTemperatureValue.Invoke((MethodInvoker)delegate
            {
                try
                {
                    labelTemperatureValue.Text = aqs[aqs.Count - 1].Temperature.ToString();
                }catch(Exception e)
                {
                    labelTemperatureValue.Text = "Sem dados";
                }
            });
            labelHumidityValue.Invoke((MethodInvoker)delegate {
                try
                {
                    labelHumidityValue.Text = aqs[aqs.Count - 1].Humidity.ToString();
                }
                catch
                {
                    labelHumidityValue.Text = "Sem dados";
                    richTextBoxData.Text = "Sem dados";
                }
            });
      
                
        }
 
        private void updateTable()
        {
            JsonSensorData d = new JsonSensorData();
            if (sensorAtual != null)
            {
                d.SensorID = sensorAtual.SensorID;
                d.start = "";
                d.end = "";
                d.AQID = idRecv;
                d.cred = cred;
                string data = Newtonsoft.Json.JsonConvert.SerializeObject(d);
                string res = Post("https://localhost:44327/api/aq/all", data, "application/json");//mudar o post para cred
                List<AQ> aux = new List<AQ>();
                aux = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AQ>>(res);
                foreach (AQ a in aux)
                {
                    Console.WriteLine(a.Id);
                    aqs.Add(a);
                }
                updateBox = false;
                updateUI();
                Console.WriteLine(d.AQID);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxSensors.SelectedIndexChanged += new System.EventHandler(itemChanged);
        }

        private void loadSensors()
        {        
            string jsonToData = "{ \"cred\": \"" + cred + "\" }";
            string response = Post("https://localhost:44327/api/sensors/authed", jsonToData, "application/json");  
            list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Sensor>>(response);
            comboBoxSensors.Invoke((MethodInvoker)delegate { 
                comboBoxSensors.Items.Clear();

                foreach (Sensor sen in list)
                {
                    comboBoxSensors.Items.Add(sen);
                }
            });
            richTextBoxData.Invoke((MethodInvoker)delegate
            {
                richTextBoxData.Text = "";
            });
            labelBatteryValue.Invoke((MethodInvoker)delegate
            {
                labelBatteryValue.Text = "";
            });
            labelDataValue.Invoke((MethodInvoker)delegate
            {
                labelDataValue.Text = "";
            });
            labelTemperatureValue.Invoke((MethodInvoker)delegate
            {
                labelTemperatureValue.Text = "";
            });
            labelHumidityValue.Invoke((MethodInvoker)delegate {
                labelHumidityValue.Text = "";
            });

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
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                //labelLogin.Text = "Non Authed";
                return "non authed";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            username = textBoxUsername.Text;
            password = textBoxPassword.Text;
            var model = new AuthModel();
            model.username = username;
            model.password = password;
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            string response = Post("https://localhost:44327/api/users/auth", data, "application/json");
            authed = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(response);
            if (authed)
            {
                labelLogin.Text = "Authed";
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(username + ":" + password);
                cred = System.Convert.ToBase64String(plainTextBytes);
                loadSensors();
            }
            else
                labelLogin.Text = "Fail";
        }
    }
}
