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
            scrollDown(2000);
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

        public bool closeAdPopup()
        {
            bool status = false;
            int waitingTime = 5;
            String adPopupCloseButtonClass = ".modal-dialog.wrapper_2P2b .close_3d2Y";
            String notificationPopupCloseButtonClass = ".modal-dialog.modalDialog_1Oud .closeBtn_2s1w";

            try
            {
                waitForElementByCssSelector(adPopupCloseButtonClass, waitingTime);
                IWebElement adPopupCloseBtnElm = seleDriver.FindElement(By.CssSelector(adPopupCloseButtonClass));
                adPopupCloseBtnElm.Click();
                status = true;
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
                status = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("notification popup not found");
            }
            return status;
        }
        public void enterSearch(String value)
        {
            waitForElementByCssSelector(searchInputSlt);
            IWebElement elm = seleDriver.FindElement(By.CssSelector(searchInputSlt));
            elm.SendKeys(value);
        }

        public void clickSearchBtn()
        {
            waitForElementByCssSelector(searchBtnSlt);
            IWebElement elm = seleDriver.FindElement(By.CssSelector(searchBtnSlt));
            System.Threading.Thread.Sleep(1000);
            elm.Click();
        }

        public String viewFirstProductInSearchList()
        {
            String firstItemSlt = ".ReactVirtualized__Grid.ReactVirtualized__List > .ReactVirtualized__Grid__innerScrollContainer > div:first-child > .list_1VuX:first-child > .item_3KnU:first-child";
            waitForElementByCssSelector(firstItemSlt);
            IWebElement item = seleDriver.FindElement(By.CssSelector(firstItemSlt));
            String itemLink = Helper.convertToPureLink(item.GetAttribute("href"));
            item.FindElement(By.TagName("img"));
            seleDriver.Navigate().GoToUrl(itemLink);
            return itemLink;
        }

        public void clickAddToCart()
        {
            String addToCartButtonSlt = ".btn_2o3k.addtocart_3CEN";
            waitForElementByCssSelector(addToCartButtonSlt);
            IWebElement addToCartButton = seleDriver.FindElement(By.CssSelector(addToCartButtonSlt));
            System.Threading.Thread.Sleep(1000);
            addToCartButton.Click();
        }

        public void clickCart()
        {
            String cartSlt = ".headerCart_3wHw";
            waitForElementByCssSelector(cartSlt);
            IWebElement cart = seleDriver.FindElement(By.CssSelector(cartSlt));
            String link = cart.GetAttribute("href");
            seleDriver.Navigate().GoToUrl(link);
        }

        public void loginBySendoAccount()
        {
            // account credential
            String phoneNumber = "0812467871";
            String password = "Peteranhsendo*";
            //

            String dialogSlt = ".modal-dialog.modalClassic_2uBq";

            // click login
            String loginButtonSlt = $"{dialogSlt} .leftNav_1YQo .navLink_v24_";
            waitForElementByCssSelector(loginButtonSlt);
            IWebElement loginButton = seleDriver.FindElement(By.CssSelector(loginButtonSlt));
            loginButton.Click();

            // enter username
            String usernameInputSlt = ".form-control.input-standard[name=\"username\"]";
            waitForElementByCssSelector(usernameInputSlt);
            IWebElement usernameInput = seleDriver.FindElement(By.CssSelector(usernameInputSlt));
            usernameInput.SendKeys(phoneNumber);

            // enter password
            String passwordInputSlt = ".form-control.input-standard[name=\"password\"]";
            IWebElement passwordInput = seleDriver.FindElement(By.CssSelector(passwordInputSlt));
            passwordInput.SendKeys(password);

            // click submit
            String submitButtonSlt = ".btnLogin_1eqO";
            IWebElement submitButton = seleDriver.FindElement(By.CssSelector(submitButtonSlt));
            submitButton.Click();
        }

        // ADD PRODUCT TO CART
        public void addProductsToCart(List<ProductOrder> productOrderList)
        {
            if (productOrderList.Count > 0)
            {
                foreach (ProductOrder orderData in productOrderList) orderProduct(orderData);
            }
        }

        private void orderProduct(ProductOrder orderData)
        {
            enterSearch(orderData.getTitle());
            clickSearchBtn();
            String itemLink = viewFirstProductInSearchList();
            orderData.setLink(itemLink);
            clickAddToCart();
        }

        // UPDATE PRODUCT QTY
        public void updateCartItemQtyByProductOrderList(List<ProductOrder> productOrderList)
        {
            bool isClosedAd = false;
            String cartItemSlt = ".cartItem_3lPn";
            String qtyContainerSlt = ".item_xSrP.quantityInput_2F-x";
            String qtyCaretSlt = $"{qtyContainerSlt} .caret_1Zer";
            String qtyInputSlt = $"{qtyContainerSlt} .input_2BHT";
            waitForElementByCssSelector(cartItemSlt);
            IList<IWebElement> cartItemList = seleDriver.FindElements(By.CssSelector(".cartItem_3lPn"));
            int i = 0;
            foreach (IWebElement cartItem in cartItemList)
            {
                try
                {
                    int qtyValue = Int32.Parse(cartItem.FindElement(By.CssSelector(qtyInputSlt)).Text);
                    String link = cartItem.FindElement(By.CssSelector(".cartHoriz_4sOH")).GetAttribute("href");
                    ProductOrder productOrder = findOrderProductByLink(productOrderList, link);
                    if (productOrder != null)
                    {
                        // RUN UPDATE QTY
                        if (qtyValue != productOrder.getQty())
                        {
                            if (i >= 1 && !isClosedAd)
                            {
                                isClosedAd = closeAdPopup();
                            }
                            waitForElementByCssSelector(qtyCaretSlt);
                            //moveToElement(qtyCaretSlt);
                            IWebElement qtyCaret = cartItem.FindElement(By.CssSelector(qtyCaretSlt));
                            ScrollToView(qtyCaret);
                            System.Threading.Thread.Sleep(1000);
                            qtyCaret.Click();
                            System.Threading.Thread.Sleep(1000);
                            onUpdateQty(qtyValue, productOrder.getQty());
                        }
                    }
                }catch(Exception e)
                {
                    Console.WriteLine("update err " + e.Message);
                }
                i++;
            }


        }

        public void onUpdateQty(int current, int expected)
        {
            String dialogSlt = ".modal-dialog.cartProductOption_10tp";

            String plusButtonSlt = $"{dialogSlt} .btn_1IRK:last-child";
            String confirmButtonSlt = $"{dialogSlt} .btn_1zsT";
            IWebElement plusButton = seleDriver.FindElement(By.CssSelector(plusButtonSlt));
            IWebElement confirmButton = seleDriver.FindElement(By.CssSelector(confirmButtonSlt));
            waitForElementByCssSelector(plusButtonSlt);
            int clickingTime = expected - current;
            for (int i = 0; i < clickingTime; i++) plusButton.Click();
            confirmButton.Click();
        }

        // SUPPORTED METHODS
        private ProductOrder findOrderProductByLink(List<ProductOrder> productOrderList, String link)
        {
            foreach (ProductOrder productOrder in productOrderList)
            {
                if (productOrder.getLink().Trim().Equals(link.Trim())) return productOrder;
            }
            return null;
        }

        private void moveToElement(String slt)
        {
            var element = seleDriver.FindElement(By.CssSelector(slt));
            Actions actions = new Actions(seleDriver);
            actions.MoveToElement(element);
            actions.Perform();
        }

        public void ScrollTo(int xPosition = 0, int yPosition = 0)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)seleDriver;
            var script = String.Format("window.scrollTo({0}, {1})", xPosition, yPosition);
            js.ExecuteScript(script);
        }

        public IWebElement ScrollToView(By selector)
        {
            var element = seleDriver.FindElement(selector);
            ScrollToView(element);
            return element;
        }

        public void ScrollToView(IWebElement element)
        {
            if (element.Location.Y > 200)
            {
                ScrollTo(0, element.Location.Y - 100); // Make sure element is in the view but below the top navigation pane
            }

        }
    }

}
