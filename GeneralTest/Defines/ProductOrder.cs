using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralTest.Defines
{
    class ProductOrder
    {

        private String title;
        private int qty;
        private String link;

        public ProductOrder(String title, int qty)
        {
            setTitle(title);
            setQty(qty);
        }

        public void setLink
            (String link)
        {
            this.link = link;
        }

        public String getLink()
        {
            return link;
        }

        public void setTitle
            (String title)
        {
            this.title = title;
        }

        public String getTitle()
        {
            return title;
        }

        public void setQty(int qty)
        {
            this.qty = qty;
        }

        public int getQty()
        {
            return qty;
        }

        public String getInfo()
        {
            return $"{getTitle()}; {getQty()}";
        }
    }
}
