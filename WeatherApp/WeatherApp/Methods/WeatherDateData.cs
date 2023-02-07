using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Methods
{
    public class WeatherDateData
    {
        public DateTime Date { get; set; }
        public string Environment { get; set; }
        public double Temprature { get; set; }
        public double Air_Humidity { get; set; }
        //public double? MoldIndex { get; set; }=null;
    }
}
