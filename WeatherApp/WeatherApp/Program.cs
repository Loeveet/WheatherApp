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


            var tempList = Functions.GetCorrectDataList();

            var fallList = Functions.GetListForFall();


            Menu.FirstMenu(tempList, fallList);


        }
    }
}