using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
    }
}
