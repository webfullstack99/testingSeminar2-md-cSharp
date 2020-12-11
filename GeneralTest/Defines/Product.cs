using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralTest.Defines
{
    class Product
    {
        private String title;
        private String price;
        private String oldPrice;
        private int discount;

        public Product(String title, String price, String oldPrice, int discount)
        {
            this.title = title;
            this.price = price;
            this.oldPrice = oldPrice;
            this.discount = discount;
        }

        public String getTitle()
        {
            return title;
        }

        public void setTitle(String value)
        {
            title = value;
        }
        public String getPrice()
        {
            return price;
        }

        public void setPrice(String value)
        {
            price = value;
        }
        public String getOldPrice()
        {
            return oldPrice;
        }

        public void setOldPrice(String value)
        {
            oldPrice = value;
        }
        public int getDiscount()
        {
            return discount;
        }

        public void setDiscount(int value)
        {
            discount = value;
        }

        public String getInfo()
        {
            return $"\n\ntitle: {getTitle()}\nprice: {getPrice()}\ndiscount: {getDiscount()}";
        }

        public String getHtmlInfo()
        {
            return $"<div>" +
                        $"<p>Title: {getTitle()}\n</p>" +
                        $"<p>Price: {getPrice()}\n</p>" +
                        $"<p>Discount: {getDiscount()}\n</p>" +
                    $"<div>";
        }
    }
}
