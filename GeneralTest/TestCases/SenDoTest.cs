using System;
using System.Collections.Generic;
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

        SenDoPage sendoPage;

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

        // SCRIPT ONE
        [Test]
        [Category("ScriptOne")]
        public void scriptOne_testTwo()
        {
            int expectedQty = 5;
            int expectedDiscount = 30;
            runScriptOne(expectedQty, expectedDiscount);
        }

        [Test]
        [Category("ScriptOne")]
        public void scriptOne_testOne()
        {
            int expectedQty = 10;
            int expectedDiscount = 30;
            runScriptOne(expectedQty, expectedDiscount);
        }

        [Test]
        [Category("ScriptOne")]
        public void scriptOne_testThree()
        {
            int expectedQty = 5;
            int expectedDiscount = 20;
            runScriptOne(expectedQty, expectedDiscount);
        }

        [Test]
        [Category("ScriptOne")]
        public void scriptOne_testFour()
        {
            int expectedQty = 8;
            int expectedDiscount = 35;
            runScriptOne(expectedQty, expectedDiscount);
        }

        // SCRIPT TWO
        [Test]
        [Category("ScriptTwo")]
        public void scriptTwo_testOne()
        {
            sendoPage = new SenDoPage(getDriver());
            sendoPage.navigateToHomePage();
            sendoPage.enterSearch($"MacBook Pro 13\" 2020 Touch Bar 1.4GHz Core i5 256GB - Xám - 00694883");
            sendoPage.clickSearchBtn();
        }

        // SCRIPT THREE
        [Test]
        [Category("ScriptThree")]
        public void scriptThree_testOne()
        {
            // show info
            sendoPage = new SenDoPage(getDriver());
            sendoPage.navigateToHomePage();
            sendoPage.closeAdPopup();
        }


        // SUPPORTED METHODS ===============
        private void runScriptOne(int expectedQty, int expectedDiscount)
        {
            // show info
            sendoPage = new SenDoPage(getDriver());
            sendoPage.navigateToHomePage();

            // get filtered list
            List<Product> itemList = sendoPage.getDiscountProductList(expectedQty, expectedDiscount);

            // print msg
            String msg = getMessage(itemList, expectedQty, expectedDiscount);
            Console.WriteLine(msg);


            // print result
            showProductListInfo(itemList);

            // check test
            logTestResult(itemList, expectedQty, expectedDiscount, msg);
        }

        private void logTestResult(List<Product> itemList, int expectedQty, int expectedDiscount, String msg)
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

        private String getListHtmlInfo(List<Product> itemList)
        {
            String result = "";
            for (int i = 0; i < itemList.Count; i++)
            {
                result += itemList.ElementAt(i).getHtmlInfo() + "<br/>";
            }
            return result;
        }

        private String getMessage(List<Product> itemList, int expectedQty, int expectedDiscount)
        {
            String msg = $"{itemList.Count} out of {expectedQty} (discount > {expectedDiscount}%)";
            msg = (itemList.Count == expectedQty) ? "Get success " + msg : msg;
            return msg;
        }

        private void showProductListInfo(List<Product> itemList)
        {
            foreach (Product item in itemList)
            {
                Console.WriteLine(item.getInfo());
            }
        }
    }
}
