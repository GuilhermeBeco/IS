using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlertData
{
    class AQ
    {
        public int Id { get; set; }
        public int SensorID { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public int Battery { get; set; }
        public DateTime Timestamp { get; set; }
        public override string ToString()
        {
            return "ID: " + Id + " | SensorID: " + SensorID + " | Temperature: " + Temperature + " | Humidity: " + Humidity + " | Battery: " + Battery + " | Timestamp: " + Timestamp + "\n ";
        }
    }
   
}
