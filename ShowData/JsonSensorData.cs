using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowData
{
    class JsonSensorData
    {
        public int  AQID { get; set; }
        public int SensorID { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string cred { get; set; }
    }
}
