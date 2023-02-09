using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Methods;
using WeatherApp.WeatherDateData;

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
            Create_TextFile_With_Data,
            End_Program = 9

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
                string inside = "Inne";
                string outside = "Ute";
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
                        outside.ShowAverageTemp(tempList, choosenDay);
                        Console.WriteLine();
                        inside.ShowAverageTemp(tempList, choosenDay);
                        break;
                    case MainMenu.Average_Temperature_Per_Day:
                        outside.CreateAverageTempForEachDay(tempList);
                        Console.WriteLine();
                        inside.CreateAverageTempForEachDay(tempList);
                        break;
                    case MainMenu.Average_Air_Humidity_Per_Day:
                        outside.CreateAverageHuminityForEachDay(tempList);
                        Console.WriteLine();
                        inside.CreateAverageHuminityForEachDay(tempList);
                        break;
                    case MainMenu.Average_Risk_For_Mold_Per_Day:
                        outside.CreateAverageMoldingForEachDay(tempList);
                        Console.WriteLine();
                        inside.CreateAverageMoldingForEachDay(tempList);
                        break;
                    case MainMenu.Date_For_Metrological_Fall:
                        outside.CreateListForMeteorlogicalSeason(fallList, 10, false);
                        break;
                    case MainMenu.Date_For_Metrological_Winter:
                        outside.CreateListForMeteorlogicalSeason(fallList, 0, false);
                        break;
                    case MainMenu.Create_TextFile_With_Data:
                        Functions.CreateTextFileWithData(tempList, fallList);
                        break;
                    case MainMenu.End_Program:
                        menuLoop = false;
                        break;
                }
                Console.ReadKey();
            }
        }
    }
}
