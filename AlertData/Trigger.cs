namespace AlertData
{
    public class Trigger
    {
        public int SensorID { get; set; }
        public string operacao { get; set; }
        public string campo { get; set; }
        public float valor { get; set; }
        
        public string email { get; set; }

    }
}