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

            //Functions.CreateTextFile();

            //Functions.CreateListForMeteorlogicalSeason(fallList, "Ute", 1, true);
            //Functions.CreateListForMeteorlogicalSeason(fallList, "Ute", 10, true);


            //Functions.CreateAverageTempForEachMonth(tempList, "Ute");
            //Functions.CreateAverageTempForEachMonth(tempList, "Inne");

            //Functions.CreateListAverageHuminityForEachMonth(tempList, "Ute");
            //Functions.CreateListAverageHuminityForEachMonth(tempList, "Inne");

            //Functions.CreateListForMoldingEachMonth(tempList, "Inne");
            //Functions.CreateListForMoldingEachMonth(tempList, "Ute");

            //Functions.WriteOutMoldingVariableToFile();

        }
    }
}