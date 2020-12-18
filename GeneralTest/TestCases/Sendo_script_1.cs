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
        public void testOne()
        {
            runTest(10, 30);
        }

        [Test]
        [Category("ScriptOne")]
        public void testTwo()
        {
            runTest(5, 20);
        }

        [Test]
        [Category("ScriptOne")]
        public void testThree()
        {
            int expectedQty = 8;
            int expectedDiscount = 35;
            runTest(expectedQty, expectedDiscount);
        }

        [Test]
        [Category("ScriptOne")]
        public void testFour()
        {
            runTest(5, 45);
        }

        protected void runTest(int expectedQty, int expectedDiscount)
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
