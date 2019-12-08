using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlertData
{
    class FileManager
    {
        private string filePath { get; set; }
        private string txtFile { get; set; }
        public FileManager (string filePath)
        {
            this.filePath = filePath;
        }
        public List<Trigger> getTriggers()
        {
            readFile();
          List<Trigger> triggers = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Trigger>>(txtFile);
            return triggers;
        }
        public void writeTriggers(List<Trigger> triggers)
        {
            string s= Newtonsoft.Json.JsonConvert.SerializeObject(triggers);
            writeFile(s);   
        }

        private void readFile()
        { 
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    txtFile = streamReader.ReadToEnd();
                }
           
        }
        private void readByLine()
        {
            var fileStream = new FileStream(@"c:\file.txt", FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    txtFile += line;
                }
            }
        }      

        private void writeFile(string s)
        {
            using (StreamWriter outputFile = new StreamWriter(filePath))
            {
                outputFile.WriteLine(s);
            }
        }
    }
}
