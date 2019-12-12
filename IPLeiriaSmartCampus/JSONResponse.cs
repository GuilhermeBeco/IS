using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPLeiriaSmartCampus
{
    public class JSONResponse
    {
        public int SensorID { get; set;}
        public string start { get; set;}
        public string end { get; set;}
        public int AQID { get; set;}
        public string cred { get; set; }
    }
}