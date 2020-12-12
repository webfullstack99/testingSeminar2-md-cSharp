using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralTest.Config;
using GeneralTest.Defines;
using GeneralTest.PageMethods;
using NUnit.Framework;

namespace GeneralTest.TestCases
{
    class SenDoTest : ExtentReport
    {

        protected SenDoPage sendoPage;

        [Test]
        public void filterDiscountProductTest()
        {
            sendoPage = new SenDoPage(getDriver());
            sendoPage.navigateToHomePage();
            sendoPage.moveToNavbar();
            sendoPage.clickProductBranch();
            sendoPage.checkFreeShipCheckbox();
            sendoPage.chooseBestSeller();

            // get filtered list
            List<Product> itemList = sendoPage.getFilteredItemList();

            // show info
            Console.WriteLine("filtered: " + itemList.Count);
            foreach (Product item in itemList)
            {
                Console.WriteLine(item.getInfo());
            }
        }

        protected String[] readFile(String filePath)
        {

            // Read a text file line by line.  
            string[] lines = File.ReadAllLines(filePath);
            return lines;
        }

        protected void logTestResult(List<Product> itemList, int expectedQty, int expectedDiscount, String msg)
        {

            if (itemList.Count != expectedQty)
            {
                logFail(msg);
                if (itemList.Count > 0) logInfo(getListHtmlInfo(itemList));
                Assert.IsTrue(false);
            }
            else
            {
                logPass(msg);
                logInfo(getListHtmlInfo(itemList));
            }
        }

        protected String getListHtmlInfo(List<Product> itemList)
        {
            String result = "";
            for (int i = 0; i < itemList.Count; i++)
            {
                result += itemList.ElementAt(i).getHtmlInfo() + "<br/>";
            }
            return result;
        }

        protected String getMessage(List<Product> itemList, int expectedQty, int expectedDiscount)
        {
            String msg = $"{itemList.Count} out of {expectedQty} (discount > {expectedDiscount}%)";
            msg = (itemList.Count == expectedQty) ? "Get success " + msg : msg;
            return msg;
        }

        protected void showProductListInfo(List<Product> itemList)
        {
            foreach (Product item in itemList)
            {
                Console.WriteLine(item.getInfo());
            }
        }
    }
}
