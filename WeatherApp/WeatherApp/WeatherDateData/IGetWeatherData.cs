using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.WeatherDateData
{
    internal interface Interface1
    {
        DateTime Date { get; set; }
        string Environment { get; set; }
        double Temprature { get; set; }
        double Air_Humidity { get; set; }
        //double? MoldIndex { get; set; }=null;
    }
}
