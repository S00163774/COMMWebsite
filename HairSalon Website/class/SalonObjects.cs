using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HairSalon_Website
{
    public class ProductCategory
    {
        public string ProductCategoryID { get; set; }
        public string ProductCategoryName { get; set; }
    }

    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public double ProductPrice { get; set; }
        public int ProductStock { get; set; }
        public string ProductURL { get; set; }
        public int ProductCategoryID { get; set; }

    }

    public class User
    {
        public int UserID { get; set; }
        public string UserFirstName { get; set; }
        public string UserSurname { get; set; }
        public string UserMobileNumber { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserType { get; set; }
    }
}