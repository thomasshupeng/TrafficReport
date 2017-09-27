using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BingMapsRESTToolkit;

namespace TrafficRecorder
{
    class RoutList: List<Route>
    { }
    class Route
    {
        private string FileName;

        public string Name { get; set; }
        public SimpleWaypoint From = null;
        public SimpleWaypoint Via = null;
        public SimpleWaypoint To = null;
        public TimeSpan NormalDuration { get; set; }
        public TimeSpan CurrentDuration { get; set; }
        
        private void WriteFileHeader()
        {
            StreamWriter RecordFile = System.IO.File.AppendText(FileName);
            RecordFile.WriteLine(From.Address);
            RecordFile.WriteLine(Via.Address);
            RecordFile.WriteLine(To.Address);
        }
        private void ReadFileHeader()
        {

        }
        public void AddingRecord(DateTime dt, TimeSpan Duration)
        {
            if(FileName.Length ==0)
            {
                FileName = System.Environment.CurrentDirectory + "\\" + Name + ".dat";
            }
            if (!File.Exists(FileName))
            {
                WriteFileHeader();
            }
            StreamWriter  RecordFile = System.IO.File.AppendText(FileName);
            RecordFile.WriteLine(dt.ToString() + Duration.TotalMinutes);
        }
    }
}
