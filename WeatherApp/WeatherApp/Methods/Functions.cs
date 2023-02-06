using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WeatherApp.Methods
{
    public class Functions
    {
        public static string path = "../../../Files/";
        public static void GetDates()
        {
            string pathName = path + "tempdata.txt";
           
            //Console.Write("Enter the year: ");
            //string year = Console.ReadLine();
            //Console.Write("Enter the month: ");
            //string month = Console.ReadLine();
            //Console.WriteLine("Enter the date: ");
            //string date = Console.ReadLine();

            //Regex firstChain = new Regex(@$"^{year}.{month}.{date}.*Ute.*$");
            Regex secondChain = new Regex(@"");
            List<WeatherDateData> data = new List<WeatherDateData>();
            using (StreamReader reader = new StreamReader(pathName))
            {

            Regex outsideChain = new Regex(@".*Inne.*$");
                string fileContent = reader.ReadLine();
                while (fileContent != null)
                {
                    string[]arr=fileContent.Split(',');
                    arr[2].Replace('.', ',');
                    WeatherDateData a = new WeatherDateData()
                    {
                        Date = Convert.ToDateTime(arr[0]),
                        Environment = arr[1],
                        Temprature = Convert.ToDouble(arr[2]),
                        Air_Humidity= Convert.ToDouble(arr[3])
                       
                    };
                    data.Add(a);

                    //MatchCollection match =firstChain.Matches(fileContent);
                    
                    fileContent = reader.ReadLine();
                    //foreach (var m in match)
                    //{
                    //    Console.WriteLine(m);
                    //}
                }

                //foreach(var b in reader)
                //{
                //    Console.WriteLine(reader);
                //}
                //MatchCollection matches = firstChain.Matches(pathName);
                //if (matches.Count != 0)
                //{
                //    foreach (var a in matches)
                //    {
                //        Console.WriteLine(a);
                //    }

                //}
            }

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
    }
}
