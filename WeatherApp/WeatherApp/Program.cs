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

            var fallList = Functions.GetListForFall();

            Functions.CreateListForWhenFallOccurs(fallList, "Ute");
         

            //var choosenDay = Functions.SelectDate();

            //Functions.ShowAverageTemp(tempList, choosenDay, "Inne");
            //Functions.ShowAverageTemp(tempList, choosenDay, "Ute");
            //Functions.CreateAverageTempForEachDay(tempList,"Ute");
            //Console.WriteLine("------------");
            //Functions.CreateAverageTempForEachDay(tempList, "Inne");

            //Functions.CreateAverageHuminityForEachDay(tempList, "Ute");
            //Console.WriteLine("----");
            //Functions.CreateAverageHuminityForEachDay(tempList, "Inne");

            //Functions.CreateAverageTempForEachMonth(tempList, "Ute");
            //Functions.CreateAverageTempForEachMonth(tempList, "Inne");


        }
    }
}