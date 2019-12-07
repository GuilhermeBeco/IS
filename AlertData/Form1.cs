using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
       
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            s = GetAsync("https://localhost:44327/api/sensors/");
            list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Sensor>>(s.Result);
            comboBoxSensors.Items.Clear();
            foreach (Sensor sen in list)
            {
                comboBoxSensors.Items.Add(sen);
            }
            triggers = fm.getTriggers();
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
            fm.writeTriggers(triggers);
        }
    }
}
