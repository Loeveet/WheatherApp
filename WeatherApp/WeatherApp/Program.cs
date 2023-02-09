using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using WeatherApp.Methods;
using WeatherApp.WeatherDateData;

namespace WeatherApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var tempList = Functions.GetCorrectDataList(5);
            var fallList = Functions.GetCorrectDataList(7);
            Menu.FirstMenu(tempList, fallList);






        }
    }
}