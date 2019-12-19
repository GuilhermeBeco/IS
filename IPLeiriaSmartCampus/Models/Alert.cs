using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPLeiriaSmartCampus.Models
{
    public class Alert
    { 
        public int id { get; set; }
        public string body { get; set; }
        public DateTime timestamp { get; set; }

    }
}