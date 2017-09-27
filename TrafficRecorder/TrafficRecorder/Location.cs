using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TrafficRecorder
{
    class Location
    {
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip {get; set;}
        public float X { get; set; } //Latitude;
        public float Y { get; set; } //Longtitude;

        private string _strFormat = "{};{};{};{},{} {};[{},{}]";

        public Location()
        {
            X = 0;
            Y = 0;
        }
        public string ToStr()
        {
            return (string.Format(_strFormat, Name, AddressLine1, AddressLine2, City, State, Zip, X, Y));
        }
        public string URLEncodedString()
        {
            string url = "";
            if((X ==0 ) && (Y==0))
            {
                url= HttpUtility.UrlEncode(AddressLine1 + "%2C" + AddressLine2 + "%2C" + City + "%2C" + State + " " + Zip);
            }
            else
            {
                url = X + "," + Y;
            }
            return url;
        }
    }
}
