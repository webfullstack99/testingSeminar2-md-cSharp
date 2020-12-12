using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GeneralTest.Defines
{
    class Helper
    {
        public static String getRootPath()
        {

            String result = "";
            try
            {
                result = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "");
                result = result.Replace("\\GeneralTest\\bin\\Debug", "");
            }
            catch (DirectoryNotFoundException dirEx)
            {
                Console.WriteLine("Directory not found: " + dirEx.Message);
            }
            return result;

        }
        public static string[] splitFileLine(String line)
        {
            //string[] result = Regex.Split(line, @"\t|\|{1,2}");
            string[] result = Regex.Split(line, @"\t");
            return result;
        }

        public static string convertToPureLink(String link)
        {
            String result = Regex.Replace(link, "\\?.*$", "");
            return result.Trim();
        }

    }

}
