using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Methods;

namespace WeatherApp
{
    internal class Menu
    {
        enum MainMenu
        {
            Average_Temperature_Per_Choosen_Day = 1,
            Average_Temperature_Per_Day,
            Average_Air_Humidity_Per_Day,
            Average_Risk_For_Mold_Per_Day,
            Date_For_Metrological_Fall,
            Date_For_Metrological_Winter,
            Log_Out = 9

        }

        public static void FirstMenu(List<WeatherDateData.WeatherDateData> tempList, List<WeatherDateData.WeatherDateData> fallList)
        {
            bool menuLoop = true;
            while (menuLoop)
            {
                Console.Clear();
                foreach (int i in Enum.GetValues(typeof(MainMenu)))
                {
                    Console.WriteLine($"{i}. {Enum.GetName(typeof(MainMenu), i).Replace("_", " ")}");
                }
                int nr;
                MainMenu menu = (MainMenu)99; //Default
                if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr) || nr > Enum.GetNames(typeof(MainMenu)).Length - 1)
                {
                    menu = (MainMenu)nr;
                    Console.Clear();
                }
                switch (menu)
                {
                    case MainMenu.Average_Temperature_Per_Choosen_Day:
                        var choosenDay = Functions.SelectDate();
                        Functions.ShowAverageTemp(tempList, choosenDay, "Inne");
                        Functions.ShowAverageTemp(tempList, choosenDay, "Ute");
                        break;
                    case MainMenu.Average_Temperature_Per_Day:
                        Functions.CreateAverageTempForEachDay(tempList, "Ute");
                        Console.WriteLine();
                        Functions.CreateAverageTempForEachDay(tempList, "Inne");
                        break;
                    case MainMenu.Average_Air_Humidity_Per_Day:
                        Functions.CreateAverageHuminityForEachDay(tempList, "Ute");
                        Console.WriteLine();
                        Functions.CreateAverageHuminityForEachDay(tempList, "Inne");
                        break;
                    case MainMenu.Average_Risk_For_Mold_Per_Day:
                        Functions.CreateAverageMoldingForEachDay(tempList, "Ute");
                        Console.WriteLine();
                        Functions.CreateAverageMoldingForEachDay(tempList, "Inne");
                        break;
                    case MainMenu.Date_For_Metrological_Fall:
                        Functions.CreateListForMeteorlogicalSeason(fallList, "Ute", 10, false);
                        break;
                    case MainMenu.Date_For_Metrological_Winter:
                        Functions.CreateListForMeteorlogicalSeason(fallList, "Ute", 1, false);
                        break;


                }
                Console.ReadKey();
            }
        }
    }
}
