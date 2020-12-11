using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralTest.Defines;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace GeneralTest.PageMethods
{
    class SenDoPage : Page
    {

        public String searchInputSlt = ".searchBox_1T3n input";
        public String searchBtnSlt = ".btn.searchButton_3d_3.orange";
        public String homeProductClass = "item_3ggE";
        public String homeProductDiscountBadge = "discountBadge_1GZz";


        public SenDoPage(IWebDriver seleDriver) : base(seleDriver) { }

        public void navigateToHomePage()
        {
            seleDriver.Navigate().GoToUrl("https://www.sendo.vn/");
        }

        public void moveToNavbar()
        {
            //IWebElement gmailLink = seleDriver.FindElement(By.LinkText(".megaMenuContainer a[href=\"/cong-nghe\I"]"));
            IWebElement gmailLink = seleDriver.FindElement(By.LinkText("Điện thoại"));
            Actions actionProvider = new Actions(seleDriver);
            // Performs mouse move action onto the element
            actionProvider.MoveToElement(gmailLink).Build().Perform();
        }

        public void clickProductBranch()
        {
            String linkText = "Samsung";
            waitForElementByLinkText(linkText);
            seleDriver.FindElement(By.LinkText(linkText)).Click();
            //IWebElement gmailLink = seleDriver.FindElement(By.LinkText(".megaMenuContainer a[href=\"/cong-nghe\I"]"));

        }

        public void checkFreeShipCheckbox()
        {
            String slt = ".container_iKiQ label:last-child";
            waitForElementByCssSelector(slt);
            var cb = seleDriver.FindElement(By.CssSelector(slt));
            cb.Click();
        }

        public void chooseBestSeller()
        {
            var element = seleDriver.FindElement(By.XPath("//li[text()='Bán chạy']"));
            element.Click();
        }

        public List<Product> getFilteredItemList()
        {
            System.Threading.Thread.Sleep(2000);
            // list_1VuX grid5_gtk- item_3KnU discountBadge_1GZz
            String discountClassName = ".discountBadge_1GZz";
            IList<IWebElement> itemList = seleDriver.FindElements(By.ClassName("item_3KnU"));
            List<Product> filteredItemList = new List<Product>();
            Console.WriteLine("\n\nTotal: " + itemList.Count);
            int i = 0;
            foreach (IWebElement item in itemList)
            {
                try
                {
                    IWebElement discountElem = item.FindElement(By.CssSelector(discountClassName));
                    String discountPercentString = discountElem.Text;
                    int discountPercent = Int32.Parse(discountPercentString.Replace("%", "").Trim());
                    if (discountPercent > 35)
                    {
                        String title = item.FindElement(By.CssSelector(".truncateMedium_Tofh")).Text;
                        String price = item.FindElement(By.CssSelector(".currentPrice_2hr9")).Text;
                        String oldPrice = item.FindElement(By.CssSelector(".oldPrice_itl0")).Text;
                        filteredItemList.Add(new Product(title, price, oldPrice, discountPercent));
                    }
                }
                catch (Exception ex) { }
            }
            return filteredItemList;
        }

        public List<Product> getDiscountProductList(int productQty, int discount)
        {

            IJavaScriptExecutor js = (IJavaScriptExecutor)seleDriver;
            js.ExecuteScript("window.scrollBy(0,2000)", "");
            System.Threading.Thread.Sleep(3000);
            // list_1VuX grid5_gtk- item_3KnU discountBadge_1GZz
            IList<IWebElement> itemList = seleDriver.FindElements(By.ClassName(homeProductClass));
            List<Product> filteredItemList = new List<Product>();
            Console.WriteLine("\n\nTotal: " + itemList.Count);
            Console.WriteLine($"Expected: {productQty} item, discount greater: {discount}%");
            foreach (IWebElement item in itemList)
            {
                try
                {
                    IWebElement discountElem = item.FindElement(By.CssSelector($".{homeProductDiscountBadge}"));
                    String discountPercentString = discountElem.Text;
                    int discountPercent = Int32.Parse(discountPercentString.Replace("%", "").Trim());
                    if (discountPercent > discount)
                    {
                        String title = item.FindElement(By.CssSelector(".truncateMedium_Tofh")).Text;
                        String price = item.FindElement(By.CssSelector(".currentPrice_2hr9")).Text;
                        String oldPrice = item.FindElement(By.CssSelector(".oldPrice_itl0")).Text;
                        filteredItemList.Add(new Product(title, price, oldPrice, discountPercent));
                        if (filteredItemList.Count >= productQty) break;
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return filteredItemList;
        }

        public void closeAdPopup()
        {
            int waitingTime = 15;
            String adPopupCloseButtonClass = ".modal-dialog.wrapper_2P2b .close_3d2Y";
            String notificationPopupCloseButtonClass = ".modal-dialog.modalDialog_1Oud .closeBtn_2s1w";

            try
            {
                waitForElementByCssSelector(adPopupCloseButtonClass, waitingTime);
                IWebElement adPopupCloseBtnElm = seleDriver.FindElement(By.CssSelector(adPopupCloseButtonClass));
                adPopupCloseBtnElm.Click();
            }
            catch (Exception e)
            {
                Console.WriteLine("ad popup not found");
            }

            try
            {
                waitForElementByCssSelector(notificationPopupCloseButtonClass, waitingTime);
                IWebElement notificationCloseBtnElm = seleDriver.FindElement(By.CssSelector(notificationPopupCloseButtonClass));
                notificationCloseBtnElm.Click();
            }
            catch (Exception e)
            {
                Console.WriteLine("notification popup not found");
            }
        }
        public void enterSearch(String value)
        {
            IWebElement elm = seleDriver.FindElement(By.CssSelector(searchInputSlt));
            elm.SendKeys(value);
        }

        public void clickSearchBtn()
        {
            IWebElement elm = seleDriver.FindElement(By.CssSelector(searchBtnSlt));
            elm.Click();
        }
    }

}
