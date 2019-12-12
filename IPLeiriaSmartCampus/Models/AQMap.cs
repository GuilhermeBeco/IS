using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPLeiriaSmartCampus.Models
{
    public class AQMap
    {
        //public int Id { get; set; }
        public int SensorID { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public int Battery { get; set; }
        public String Timestamp { get; set; }

        public string cred { get; set; }
    }
}