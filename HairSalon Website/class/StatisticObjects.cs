using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HairSalon_Website
{
    public class ProductOrderCount
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string NoOfOrders { get; set; }

        public override string ToString()
        {
            return this.ProductID.ToString() + "," + this.ProductName + "," + this.NoOfOrders.ToString();
        }
    }
}