using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WeatherApp.Methods;
using WeatherApp.WeatherDateData;


namespace WeatherApp.Methods
{
    public static class Functions
    {
        public static string path = "../../../Files/";

        public static string pathToResult = path + "result.txt";
        public static List<WeatherDateData.WeatherDateData> GetCorrectDataList()
        {
            string pathName = path + "tempdata.txt";

            List<WeatherDateData.WeatherDateData> data = new List<WeatherDateData.WeatherDateData>();
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
                        if (hour < 24 && (year == 2016 || year == 2017) && month > 5 && month < 13 && day < 32)
                        {
                            WeatherDateData.WeatherDateData a = new WeatherDateData.WeatherDateData()
                            {
                                Date = Convert.ToDateTime(arr[0]),
                                Environment = arr[1].Trim(),
                                Temprature = Convert.ToDouble(temp),
                                Air_Humidity = Convert.ToDouble(arr[3])
                            };
                            if (a.Temprature > 0 && a.Temprature < 50 && a.Air_Humidity > 80)
                            {
                                a.MoldIndex = (Convert.ToDecimal(a.Air_Humidity * a.Temprature)) / 50;
                            }
                            else
                            {
                                a.MoldIndex = 0;
                            }
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
        public static DateTime SelectDate()
        {
            Regex regex = new Regex("^(?<year>[0-9]{4})\\-(?<month>[0-9]{2})-(?<day>[0-9]{2})");
            bool correctInput = false;
            string chooseDay = "";
            while (correctInput == false)
            {
                Console.Write("Enter date of which day you want look at between 2016-06-01 to 2016-12-31: ");
                chooseDay = Console.ReadLine();
                MatchCollection matches = regex.Matches(chooseDay);
                try
                {
                    int year = int.Parse(regex.Match(chooseDay).Groups["year"].Value);
                    int month = int.Parse(regex.Match(chooseDay).Groups["month"].Value);
                    int day = int.Parse(regex.Match(chooseDay).Groups["day"].Value);
                    if (matches.Count > 0 && year == 2016 && month > 5 && month < 13 && day < 32)
                    {
                        correctInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect date");
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Wrong input. Use format yyyy-MM-dd");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Wrong input. Use format yyyy-MM-dd");
                }

            }
            return Convert.ToDateTime(chooseDay);
        }
        public static void ShowAverageTemp(this string environment, List<WeatherDateData.WeatherDateData> tempList, DateTime choosenDay)
        {
            try
            {
                var averageTemp = tempList
                    .Where(x => x.Date.Date == choosenDay && x.Environment == environment)
                    .Average(x => x.Temprature);

                Console.WriteLine((environment == "Inne" ? "Inside " : "Outside ") + "temperature: "
                    + Math.Round(averageTemp, 1) + " °C");
            }
            catch
            {
                Console.WriteLine((environment == "Inne" ? "Inside " : "Outside ") + "temperature: "
                    + "The date contains no data");
            }
        }
        public static void CreateAverageTempForEachDay(this string environment, List<WeatherDateData.WeatherDateData> templist)
        {
            List<WeatherDateData.WeatherDateData> dayList = new List<WeatherDateData.WeatherDateData>();

            var result = templist
                .Where(x => x.Environment == environment)
                .GroupBy(x => x.Date.Date);

            var result2 = result
                .OrderByDescending(x => x.Average(x => x.Temprature));
            Console.WriteLine(environment == "Inne" ? "Inside" : "Outside"); 
            Console.WriteLine("Date\t\tTemperature");
            foreach (var a in result2)
            {
                Console.WriteLine(a.Key.Date.ToString("yyyy-MM-dd") + "\t" + Math.Round(a.Average(x => x.Temprature), 1) + " °C");

            }

        }
        public static void CreateAverageHuminityForEachDay(this string environment, List<WeatherDateData.WeatherDateData> templist)
        {
            List<WeatherDateData.WeatherDateData> dayList = new List<WeatherDateData.WeatherDateData>();

            var result = templist
                .Where(x => x.Environment == environment)
                .GroupBy(x => x.Date.Date);

            var result2 = result
                .OrderBy(x => x.Average(x => x.Air_Humidity));

            Console.WriteLine(environment == "Inne" ? "Inside" : "Outside");
            Console.WriteLine("Date\t\tAir Humidity");
            foreach (var a in result2)
            {
                Console.WriteLine(a.Key.Date.ToString("yyyy-MM-dd") + "\t" + Math.Round(a.Average(x => x.Air_Humidity), 1) + " %");


            }

        }
        public static void CreateAverageMoldingForEachDay(this string environment, List<WeatherDateData.WeatherDateData> templist)
        {
            List<WeatherDateData.WeatherDateData> dayList = new List<WeatherDateData.WeatherDateData>();

            var result = templist
                .Where(x => x.Environment == environment)
                .GroupBy(x => x.Date.Date);

            var result2 = result
                .OrderByDescending(x => x.Average(x => x.MoldIndex));

            Console.WriteLine(environment == "Inne" ? "Inside" : "Outside");
            Console.WriteLine("Date\t\tMoldIndex");
            foreach (var a in result2)
            {
                Console.WriteLine(a.Key.Date.ToString("yyyy-MM-dd") + "\t" + Math.Round(a.Average(x => x.MoldIndex), 1) + " %");
            }

        }
        public static void CreateAverageTempForEachMonth(this string environment, List<WeatherDateData.WeatherDateData> templist)
        {
            List<WeatherDateData.WeatherDateData> dayList = new List<WeatherDateData.WeatherDateData>();

            var result = templist
                .Where(x => x.Environment == environment)
                .GroupBy(x => x.Date.Month);

            var result2 = result
                .OrderByDescending(x => x.Average(x => x.Temprature));
            string[] months = new string[] { "January" ,
            "February","March","April","May","June","July","August","September","October","November","December"};
            Console.WriteLine("Month".PadRight(20) + "Temp");
            Console.WriteLine("-------------------------");
            foreach (var a in result2)
            {
                Console.WriteLine(months[a.Key - 1].PadRight(20) + "" + Math.Round(a.Average(x => x.Temprature), 2));

            }
            using (StreamWriter writer = new StreamWriter(pathToResult, true))
            {
                if (environment == "Inne")
                {
                    writer.WriteLine("Temperature inside");

                }
                else
                {
                    writer.WriteLine("Temperature outside");
                }
                foreach (var a in result2)
                {
                    writer.WriteLine(months[a.Key - 1].PadRight(20) + "" + Math.Round(a.Average(x => x.Temprature), 2));

                }
            }

        }
        public static List<WeatherDateData.WeatherDateData> GetListForFall()
        {
            string pathName = path + "tempdata.txt";

            List<WeatherDateData.WeatherDateData> data = new List<WeatherDateData.WeatherDateData>();
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
                        if (hour < 24 && (year == 2016 || year == 2017) && month > 7 && month < 13 && day < 32)
                        {
                            WeatherDateData.WeatherDateData a = new WeatherDateData.WeatherDateData()
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
        public static void CreateListForMeteorlogicalSeason(this string environment, List<WeatherDateData.WeatherDateData> templist, int temp, bool save)
        {
            var dayList = new List<IGrouping<DateTime, WeatherDateData.WeatherDateData>>();

            var result = templist
                .Where(x => x.Environment == environment)
                .GroupBy(x => x.Date.Date).ToList();
            int fallCount = 0;

            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].Average(x => x.Temprature) < temp)
                {
                    fallCount++;
                    dayList.Add(result[i]);
                    if (fallCount == 5)
                    {
                        break;
                    }
                }
                else
                {
                    fallCount = 0;
                    dayList.Clear();
                }
            }

            if (dayList.Count > 0)
            {
                Console.WriteLine("Date".PadRight(17) + "Temp");
                Console.WriteLine("------------------------");
                for (int i = 0; i < dayList.Count; i++)
                {
                    Console.WriteLine(dayList[0].Key.Date.ToString("yyyy-MM-dd") + "\t"
                        + Math.Round(dayList[0].Average(x => x.Temprature), 1).ToString().PadRight(5) + "°C");
                    Console.WriteLine((temp > 9 ? "Fall" : "Winter") + " has begun.");
                    break;
                }
                if (save == true)
                {
                    using (StreamWriter writer = new StreamWriter(pathToResult, true))
                    {
                        if (temp == 10)
                        {
                            writer.WriteLine("Fall has begun.");

                        }
                        else
                        {
                            writer.WriteLine("Winter has begun.");
                        }
                        foreach (var a in dayList)
                        {
                            writer.WriteLine(dayList[0].Key.Date.ToString("yyyy-MM-dd") + "\t"
                            + Math.Round(dayList[0].Average(x => x.Temprature), 1).ToString().PadRight(5) + "°C");

                            break;

                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("No valid data was found try with another temp input.");
            }

        }
        public static void CreateListAverageHuminityForEachMonth(this string environment, List<WeatherDateData.WeatherDateData> templist)
        {
            List<WeatherDateData.WeatherDateData> dayList = new List<WeatherDateData.WeatherDateData>();

            var result = templist
                .Where(x => x.Environment == environment)
                .GroupBy(x => x.Date.Month);

            var result2 = result
                .OrderByDescending(x => x.Average(x => x.Temprature));
            string[] months = new string[] { "January" ,
            "February","March","April","May","June","July","August","September","October","November","December"};
            Console.WriteLine("Month".PadRight(20) + "Air Humidity");
            Console.WriteLine("--------------------------------");
            foreach (var a in result2)
            {
                Console.WriteLine(months[a.Key - 1].PadRight(23) + "" + Math.Round(a.Average(x => x.Air_Humidity), 2));

            }
            using (StreamWriter writer = new StreamWriter(pathToResult, true))
            {
                if (environment == "Inne")
                {
                    writer.WriteLine("Air Humidity inside");

                }
                else
                {
                    writer.WriteLine("Air Humidity outside");
                }
                foreach (var a in result2)
                {
                    writer.WriteLine(months[a.Key - 1].PadRight(20) + "" + Math.Round(a.Average(x => x.Air_Humidity), 2));

                }
            }

        }
        public static void CreateListForMoldingEachMonth(this string environment, List<WeatherDateData.WeatherDateData> templist)
        {
            List<WeatherDateData.WeatherDateData> dayList = new List<WeatherDateData.WeatherDateData>();

            var result = templist
                .Where(x => x.Environment == environment)
                .GroupBy(x => x.Date.Month);

            var result2 = result
                .OrderByDescending(x => x.Average(x => x.MoldIndex));
            string[] months = new string[] { "January" ,
            "February","March","April","May","June","July","August","September","October","November","December"};
            Console.WriteLine("Month".PadRight(20) + "MoldIndex");
            Console.WriteLine("--------------------------------");
            foreach (var a in result2)
            {
                Console.WriteLine(months[a.Key - 1].PadRight(23) + "" + Math.Round(a.Average(x => x.MoldIndex), 2));

            }
            using (StreamWriter writer = new StreamWriter(pathToResult, true))
            {
                if (environment == "Inne")
                {
                    writer.WriteLine("MoldIndex inside");

                }
                else
                {
                    writer.WriteLine("MoldIndex outside");
                }
                foreach (var a in result2)
                {
                    writer.WriteLine(months[a.Key - 1].PadRight(20) + "" + Math.Round(a.Average(x => x.MoldIndex), 2));

                }
            }

        }
        public static void WriteOutMoldingVariableToFile()
        {
            using (StreamWriter writer = new StreamWriter(pathToResult, true))
            {
                writer.WriteLine("Algorithm for molding");
                writer.WriteLine("if(a.Temprature >0 && a.Temprature< 50 && a.Air_Humidity> 80){a.MoldIndex = (Convert.ToDecimal");
                writer.WriteLine("(a.Air_Humidity * a.Temprature)) / 50;} else{a.MoldIndex = 0 }");
            }
        }
        public static void CreateTextFile()
        {
            File.WriteAllText(path + "result.txt", "");
        }
        public static void CreateTextFileWithData(List<WeatherDateData.WeatherDateData> tempList, List<WeatherDateData.WeatherDateData> fallList)
        {
            string outside = "Ute";
            string inside = "Inne";
            CreateTextFile();

            outside.CreateAverageTempForEachMonth(tempList);
            inside.CreateAverageTempForEachMonth(tempList);

            outside.CreateListAverageHuminityForEachMonth(tempList);
            inside.CreateListAverageHuminityForEachMonth(tempList);

            outside.CreateListForMoldingEachMonth(tempList);
            inside.CreateListForMoldingEachMonth(tempList);

            outside.CreateListForMeteorlogicalSeason(fallList, 1, true);
            outside.CreateListForMeteorlogicalSeason(fallList, 10, true);

            WriteOutMoldingVariableToFile();
            Console.WriteLine("File with data was created");
        }
    }
}
