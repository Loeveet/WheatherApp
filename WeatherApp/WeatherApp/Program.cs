using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using WeatherApp.Methods;

namespace WeatherApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var tempList = Functions.GetCorrectDataList();
            //var choosenDay = Functions.SelectDate();

            //Functions.ShowAverageTemp(tempList, choosenDay, "Inne");
            //Functions.ShowAverageTemp(tempList, choosenDay, "Ute");
            Functions.CreateAverageTempForEachDay(tempList,"Ute");
            Console.WriteLine("------------");
            Functions.CreateAverageTempForEachDay(tempList, "Inne");


        }
    }
}