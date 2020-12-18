using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralTest.Defines;
using GeneralTest.PageMethods;
using NUnit.Framework;
using OpenQA.Selenium;

namespace GeneralTest.TestCases
{
    class Sendo_script_2 : SenDoTest
    {

        private List<ProductOrder> productOrderList;

        [Test]
        [Category("ScriptTwo")]
        public void testOne()
        {
            sendoPage = new SenDoPage(getDriver());
            sendoPage.navigateToHomePage();
            addProductOrderListFromFile();
            sendoPage.addProductsToCart(productOrderList);
            sendoPage.clickCart();
            sendoPage.updateCartItemQtyByProductOrderList(productOrderList);
        }

        private void addProductOrderListFromFile()
        {
            productOrderList = new List<ProductOrder>();
            String filePath = Helper.getRootPath() + "GeneralTest\\data\\script2_data.csv";
            String[] lines = readFile(filePath);
            foreach (string line in lines)
            {
                String[] lineSplitArr = Helper.splitFileLine(line);
                productOrderList.Add(new ProductOrder(lineSplitArr[0], Int32.Parse(lineSplitArr[1])));
            }
        }
    }
}
