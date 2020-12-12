using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralTest.Defines;
using GeneralTest.PageMethods;
using NUnit.Framework;

namespace GeneralTest.TestCases
{
    class Sendo_script_1 : SenDoTest
    {
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

        protected void runScriptOne(int expectedQty, int expectedDiscount)
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

    }
}
