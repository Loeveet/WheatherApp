using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WeatherApp.Methods
{
    public class Functions
    {
        public static string path = "../../../Files/";
        public static List<WeatherDateData> GetCorrectDataList()
        {
            string pathName = path + "tempdata.txt";

            List<WeatherDateData> data = new List<WeatherDateData>();
            using (StreamReader reader = new StreamReader(pathName))
            {
                Regex checkTime = new Regex(@"(?<hour>[0-2][0-9]):([0-5][0-9]):([0-5][0-9])$");
                Regex checkDate = new Regex("^(?<year>201[6-7])\\-(?<month>[0-1][0-9])-(?<day>[0-3][0-9])");
                string fileContent = reader.ReadLine();
                while (fileContent != null)
                {
                    string[] arr = fileContent.Split(',');
                    string temp = arr[2].Replace('.', ',');
                    if (checkTime.Match(arr[0]).Success && checkDate.Match(arr[0]).Success)
                    {
                        int hour = int.Parse(checkTime.Match(arr[0]).Groups["hour"].Value);
                        int year = int.Parse(checkDate.Match(arr[0]).Groups["year"].Value);
                        int month = int.Parse(checkDate.Match(arr[0]).Groups["month"].Value);
                        int day = int.Parse(checkDate.Match(arr[0]).Groups["day"].Value);
                        if (hour < 24 && (year == 2016 || year == 2017) && month < 13 && day < 32)
                        {
                            WeatherDateData a = new WeatherDateData()
                            {
                                Date = Convert.ToDateTime(arr[0]),
                                Environment = arr[1].Trim(),
                                Temprature = Convert.ToDouble(temp),
                                Air_Humidity = Convert.ToDouble(arr[3])
                            };
                            data.Add(a);
                        }
                        fileContent = reader.ReadLine();
                    }
                }
            }
            var newData = data
                .Where(x => x.Temprature < 40 || x.Temprature > -10 || x.Air_Humidity < 101
                || (x.Environment == "Inne" && (x.Environment == "Ute")))
                .ToList();
            return newData;
        }
        public static int TryNumber(int number, int maxValue, int minValue)
        {
            bool correctInput = false;
            while (!correctInput)
            {
                if (!int.TryParse(Console.ReadLine(), out number) || number > maxValue || number < minValue)
                {
                    Console.Write("Wrong input, try again: ");
                }
                else
                {
                    correctInput = true;
                }
            }
            return number;
        }
        public static DateTime SelectDate()
        {
            Regex regex = new Regex("^(?<year>201[6-7])\\-(?<month>[0-1][0-9])-(?<day>[0-3][0-9])");
            bool correctInput = false;
            string chooseDay = "";
            while (correctInput == false)
            {
                Console.Write("Enter date of which day you want look at: ");
                chooseDay = Console.ReadLine();
                MatchCollection matches = regex.Matches(chooseDay);
                int year = int.Parse(regex.Match(chooseDay).Groups["year"].Value);
                int month = int.Parse(regex.Match(chooseDay).Groups["month"].Value);
                int day = int.Parse(regex.Match(chooseDay).Groups["day"].Value);
                if (matches.Count > 0 && (year == 2016 || year == 2017) && month < 13 && day < 32)
                {
                    correctInput = true;

                }
                else
                {
                    Console.WriteLine("Wrong input.");
                }
            }
            return Convert.ToDateTime(chooseDay);
        }
        public static void ShowAverageTemp(List<WeatherDateData> tempList, DateTime choosenDay, string environment)
        {
            var averageTemp = tempList
                .Where(x => x.Date.Date == choosenDay && x.Environment == environment)
                .Average(x => x.Temprature);

            Console.WriteLine(Math.Round(averageTemp, 2));
        }
        public static void CreateAverageTempForEachDay(List<WeatherDateData>templist,string enviorment)
        {
            List<WeatherDateData> dayList = new List<WeatherDateData>();

            var result = templist
                .Where(x => x.Environment == enviorment)
                .GroupBy(x => x.Date.Date);

            var result2 = result
                .OrderByDescending(x => x.Average(x => x.Temprature));
                                  
                foreach(var a in result2)
                {
                Console.WriteLine(a.Key.Date.ToString("yyyy-MM-dd") +"\t"+ Math.Round(a.Average(x => x.Temprature),2));
                
                }
            
        }
    }
}
