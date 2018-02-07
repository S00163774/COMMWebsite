using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace HairSalon_Website
{
    public class DatabaseControl
    {
        const string DatabaseConnection = "Server=tcp:boothserver.database.windows.net,1433;" +
        "Initial Catalog=boothtest;Persist Security Info=False;" +
        "User ID=S00163774;Password=BOOTHserver%;" +
        "MultipleActiveResultSets=False;Encrypt=True;" +
        "TrustServerCertificate=False;Connection Timeout=30;";

        static public List<ProductCategory> ReturnCategories()
        {
            List<ProductCategory> ProductCategories = new List<ProductCategory>();

            using (SqlConnection boothtest = new SqlConnection(DatabaseConnection))
            {
                using (SqlCommand CategoriesCMD = new SqlCommand("ReturnCategories", boothtest))
                {
                    CategoriesCMD.CommandType = CommandType.StoredProcedure;

                    boothtest.Open();

                    SqlDataReader myReader = CategoriesCMD.ExecuteReader();

                    ProductCategories = new List<ProductCategory>();

                    while (myReader.Read())
                    {
                        ProductCategory productCategory = new ProductCategory();

                        productCategory.ProductCategoryID = int.Parse(myReader["ProductCategoryID"].ToString());
                        productCategory.ProductCategoryName = myReader["ProductCategoryName"].ToString();

                        ProductCategories.Add(productCategory);
                    }

                    myReader.Close(); // Close Command

                    boothtest.Close(); // Close Database Connection
                }
            }

            return ProductCategories;
        }

        static public List<Product> ReturnProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection boothtest = new SqlConnection(DatabaseConnection))
            {
                using (SqlCommand ProductsCMD = new SqlCommand("ReturnProducts", boothtest))
                {
                    ProductsCMD.CommandType = CommandType.StoredProcedure;

                    boothtest.Open();

                    SqlDataReader myReader = ProductsCMD.ExecuteReader();

                    products = new List<Product>();

                    while (myReader.Read())
                    {
                        Product product = new Product();

                        product.ProductID = int.Parse(myReader["ProductID"].ToString());
                        product.ProductName = myReader["ProductName"].ToString();
                        product.ProductDesc = myReader["ProductDesc"].ToString();
                        product.ProductPrice = double.Parse(myReader["ProductPrice"].ToString());
                        product.ProductStock = int.Parse(myReader["ProductStock"].ToString());
                        product.ProductURL = myReader["ProductURL"].ToString();
                        product.ProductCategoryID = int.Parse(myReader["ProductCategoryID"].ToString());

                        products.Add(product);
                    }

                    myReader.Close(); // Close Command

                    boothtest.Close(); // Close Database Connection
                }
            }

            return products;
        }

        static public int InsertProduct(string name, string desc, double price, int stock, string url, int categoryid)
        {
            int result = 0;

            using (SqlConnection boothtest = new SqlConnection(DatabaseConnection))
            {
                using (SqlCommand ProductCMD = new SqlCommand("InsertProduct", boothtest))
                {
                    ProductCMD.CommandType = CommandType.StoredProcedure;

                    SqlParameter ProductName = ProductCMD.Parameters.Add("@ProductName", SqlDbType.NChar, 15);
                    ProductName.Direction = ParameterDirection.Input;
                    ProductName.Value = name;

                    SqlParameter ProductDesc = ProductCMD.Parameters.Add("@ProductDesc", SqlDbType.NVarChar, 100);
                    ProductDesc.Direction = ParameterDirection.Input;
                    ProductDesc.Value = desc;

                    SqlParameter ProductPrice = ProductCMD.Parameters.Add("@ProductPrice", SqlDbType.SmallMoney);
                    ProductPrice.Direction = ParameterDirection.Input;
                    ProductPrice.Value = price;

                    SqlParameter ProductStock = ProductCMD.Parameters.Add("@ProductStock", SqlDbType.SmallInt);
                    ProductStock.Direction = ParameterDirection.Input;
                    ProductStock.Value = stock;

                    SqlParameter ProductURL = ProductCMD.Parameters.Add("@ProductURL", SqlDbType.NVarChar, 400);
                    ProductURL.Direction = ParameterDirection.Input;
                    ProductURL.Value = url;

                    SqlParameter ProductCategoryID = ProductCMD.Parameters.Add("@ProductCategoryID", SqlDbType.Int);
                    ProductCategoryID.Direction = ParameterDirection.Input;
                    ProductCategoryID.Value = categoryid;

                    SqlParameter Result = ProductCMD.Parameters.Add("Result", SqlDbType.Bit);
                    Result.Direction = ParameterDirection.ReturnValue;

                    boothtest.Open();

                    SqlDataReader myReader = ProductCMD.ExecuteReader();

                    result = Convert.ToInt32(Result.Value);

                    myReader.Close(); // Close Command

                    boothtest.Close(); // Close Database Connection
                }
            }

            return result;
        }

        static public int InsertCategory(string name)
        {
            int result = 0;

            using (SqlConnection boothtest = new SqlConnection(DatabaseConnection))
            {
                using (SqlCommand CategoryCMD = new SqlCommand("InsertProductCategory", boothtest))
                {
                    CategoryCMD.CommandType = CommandType.StoredProcedure;

                    SqlParameter CategoryName = CategoryCMD.Parameters.Add("@ProductCategoryName", SqlDbType.NVarChar, 20);
                    CategoryName.Direction = ParameterDirection.Input;
                    CategoryName.Value = name;

                    SqlParameter Result = CategoryCMD.Parameters.Add("Result", SqlDbType.Bit);
                    Result.Direction = ParameterDirection.ReturnValue;

                    boothtest.Open();

                    SqlDataReader myReader = CategoryCMD.ExecuteReader();

                    result = Convert.ToInt32(Result.Value);

                    myReader.Close(); // Close Command

                    boothtest.Close(); // Close Database Connection

                }
            }

            return result;
        }

        static public User LogIn(string email, string password)
        {
            User user = new User();

            using (SqlConnection boothtest = new SqlConnection(DatabaseConnection))
            {
                using (SqlCommand LogInCMD = new SqlCommand("LogIn", boothtest)) // Create New Command calling 'LogIn' stored procedure in boothtest database
                {
                    LogInCMD.CommandType = CommandType.StoredProcedure;

                    // Input Variables
                    SqlParameter inputEmail = LogInCMD.Parameters.Add("@ExUserEmail", SqlDbType.NVarChar, 30);
                    inputEmail.Direction = ParameterDirection.Input;

                    SqlParameter inputPassword = LogInCMD.Parameters.Add("@ExuserPassword", SqlDbType.NVarChar, 30);
                    inputPassword.Direction = ParameterDirection.Input;

                    inputEmail.Value = email;
                    inputPassword.Value = password;

                    boothtest.Open(); // Open Database Connection

                    SqlDataReader myReader = LogInCMD.ExecuteReader(); // Execute Command

                    while (myReader.Read())
                    {
                        user.UserID = int.Parse(myReader["UserID"].ToString());
                        user.UserFirstName = myReader["UserFirstName"].ToString();
                        user.UserSurname = myReader["UserSurname"].ToString();
                        user.UserMobileNumber = myReader["UserMobileNumber"].ToString();
                        user.UserEmail = myReader["UserEmail"].ToString();
                        user.UserType = myReader["UserType"].ToString();
                        user.UserURL = myReader["UserURL"].ToString();
                    }

                    myReader.Close(); // Close Command

                    boothtest.Close(); // Close Database Connection
                }
            }

            return user;
        }

        static public string Register(string firstname, string surname, string mobilenumber, string email, string password)
        {
            string emailExists = null;

            using (SqlConnection boothtest = new SqlConnection(DatabaseConnection))
            {
                using (SqlCommand RegisterCMD = new SqlCommand("Register", boothtest))
                {
                    RegisterCMD.CommandType = CommandType.StoredProcedure;

                    // Input Variables
                    SqlParameter inputFirstName = RegisterCMD.Parameters.Add("@UserFirstName", SqlDbType.NVarChar, 20);
                    inputFirstName.Direction = ParameterDirection.Input;

                    SqlParameter inputSurname = RegisterCMD.Parameters.Add("@UserSurname", SqlDbType.NVarChar, 30);
                    inputSurname.Direction = ParameterDirection.Input;

                    SqlParameter inputMobileNumber = RegisterCMD.Parameters.Add("@UserMobileNumber", SqlDbType.NVarChar, 10);
                    inputMobileNumber.Direction = ParameterDirection.Input;

                    SqlParameter inputEmail = RegisterCMD.Parameters.Add("@UserEmail", SqlDbType.NVarChar, 50);
                    inputEmail.Direction = ParameterDirection.Input;

                    SqlParameter inputPassword = RegisterCMD.Parameters.Add("@UserPassword", SqlDbType.VarChar, 50);
                    inputPassword.Direction = ParameterDirection.Input;

                    SqlParameter returnEmailExists = RegisterCMD.Parameters.Add("Result", SqlDbType.Int);
                    returnEmailExists.Direction = ParameterDirection.ReturnValue;

                    inputFirstName.Value = firstname;
                    inputSurname.Value = surname;
                    inputMobileNumber.Value = mobilenumber;
                    inputEmail.Value = email;
                    inputPassword.Value = password;

                    boothtest.Open(); // Open Database Connection

                    SqlDataReader myReader = RegisterCMD.ExecuteReader(); // Execute Command

                    emailExists = Convert.ToString(returnEmailExists.Value);

                    myReader.Close(); // Close Command

                    boothtest.Close(); // Close Database Connection
                }
            }

            return emailExists;
        }

        static public List<User> ReturnStylists()
        {
            List<User> stylists = new List<User>();

            using (SqlConnection boothtest = new SqlConnection(DatabaseConnection))
            {
                using (SqlCommand StylistsCMD = new SqlCommand("ReturnStylists", boothtest))
                {
                    StylistsCMD.CommandType = CommandType.StoredProcedure;

                    boothtest.Open();

                    SqlDataReader myReader = StylistsCMD.ExecuteReader();

                    stylists = new List<User>();

                    while (myReader.Read())
                    {
                        User stylist = new User();

                        stylist.UserID = int.Parse(myReader["UserID"].ToString());
                        stylist.UserFirstName = myReader["UserFirstName"].ToString();
                        stylist.UserSurname = myReader["UserSurname"].ToString();
                        stylist.UserMobileNumber = myReader["UserMobileNumber"].ToString();
                        stylist.UserEmail = myReader["UserEmail"].ToString();
                        stylist.UserType = myReader["UserType"].ToString();
                        stylist.UserURL = myReader["UserURL"].ToString();

                        stylists.Add(stylist);
                    }

                    myReader.Close(); // Close Command

                    boothtest.Close(); // Close Database Connection
                }
            }

            return stylists;
        }

        static public string InsertStylist(string firstname, string surname, string mobilenumber, string email, string password, string url)
        {
            string emailExists = null;

            using (SqlConnection boothtest = new SqlConnection(DatabaseConnection))
            {
                using (SqlCommand StylistCMD = new SqlCommand("InsertStylist", boothtest))
                {
                    StylistCMD.CommandType = CommandType.StoredProcedure;

                    // Input Variables
                    SqlParameter inputFirstName = StylistCMD.Parameters.Add("@UserFirstName", SqlDbType.NVarChar, 20);
                    inputFirstName.Direction = ParameterDirection.Input;

                    SqlParameter inputSurname = StylistCMD.Parameters.Add("@UserSurname", SqlDbType.NVarChar, 30);
                    inputSurname.Direction = ParameterDirection.Input;

                    SqlParameter inputMobileNumber = StylistCMD.Parameters.Add("@UserMobileNumber", SqlDbType.NVarChar, 10);
                    inputMobileNumber.Direction = ParameterDirection.Input;

                    SqlParameter inputEmail = StylistCMD.Parameters.Add("@UserEmail", SqlDbType.NVarChar, 50);
                    inputEmail.Direction = ParameterDirection.Input;

                    SqlParameter inputPassword = StylistCMD.Parameters.Add("@UserPassword", SqlDbType.VarChar, 50);
                    inputPassword.Direction = ParameterDirection.Input;

                    SqlParameter inputURL = StylistCMD.Parameters.Add("@UserURL", SqlDbType.VarChar, 400);
                    inputURL.Direction = ParameterDirection.Input;

                    SqlParameter returnEmailExists = StylistCMD.Parameters.Add("Result", SqlDbType.Int);
                    returnEmailExists.Direction = ParameterDirection.ReturnValue;

                    inputFirstName.Value = firstname;
                    inputSurname.Value = surname;
                    inputMobileNumber.Value = mobilenumber;
                    inputEmail.Value = email;
                    inputPassword.Value = password;
                    inputURL.Value = url;

                    boothtest.Open(); // Open Database Connection

                    SqlDataReader myReader = StylistCMD.ExecuteReader(); // Execute Command

                    emailExists = Convert.ToString(returnEmailExists.Value);

                    myReader.Close(); // Close Command

                    boothtest.Close(); // Close Database Connection
                }
            }

            return emailExists;
        }
    }
}