using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlertData
{
 
    class Sensor
    {
        public int SensorID { get; set; }
        public string Local { get; set; }
        public string username { get; set; }
        public override string ToString()
        {
            return Local;
        }
    }
    
}
