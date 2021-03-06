﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text;

namespace HairSalon_Website
{
    public class DatabaseControl
    {
        const string DatabaseConnection = "Server=tcp:boothserver.database.windows.net,1433;" +
        "Initial Catalog=boothtest;Persist Security Info=False;" +
        "User ID=S00163774;Password=BOOTHserver%;" +
        "MultipleActiveResultSets=False;Encrypt=True;" +
        "TrustServerCertificate=False;Connection Timeout=30;";

        const string CommDatabaseConnection = "Server=tcp:commtest1996.database.windows.net,1433;" +
        "Initial Catalog=commtest1996;Persist Security Info=False;" +
        "User ID=S00151977;Password=Sligoit17;" +
        "MultipleActiveResultSets=False;Encrypt=True;" +
        "TrustServerCertificate=False;Connection Timeout=30;";

        static string Db = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

        static public List<Product> ReturnProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection commtest1996 = new SqlConnection(CommDatabaseConnection))
            {
                using (SqlCommand ProductsCMD = new SqlCommand("ReturnProducts", commtest1996))
                {
                    ProductsCMD.CommandType = CommandType.StoredProcedure;

                    commtest1996.Open();

                    SqlDataReader myReader = ProductsCMD.ExecuteReader();

                    products = new List<Product>();

                    while (myReader.Read())
                    {
                        Product product = new Product();

                        product.ProductID = myReader["ID"].ToString();
                        product.ProductName = myReader["ProductName"].ToString();
                        product.ProductPrice = double.Parse(myReader["Price"].ToString());
                        product.ProductStock = double.Parse(myReader["Quantity"].ToString());
                        product.ProductURL = myReader["imageURL"].ToString();

                        products.Add(product);
                    }

                    myReader.Close(); // Close Command

                    commtest1996.Close(); // Close Database Connection
                }
            }

            return products;
        } //COMM Ready

        static public int InsertProduct(string name, double price, int stock, string url)
        {
            int result = 0;

            using (SqlConnection commtest1996 = new SqlConnection(CommDatabaseConnection))
            {
                using (SqlCommand ProductCMD = new SqlCommand("InsertProduct", commtest1996))
                {
                    ProductCMD.CommandType = CommandType.StoredProcedure;

                    SqlParameter ProductName = ProductCMD.Parameters.Add("@ProductName", SqlDbType.NChar, 15);
                    ProductName.Direction = ParameterDirection.Input;
                    ProductName.Value = name;

                    SqlParameter ProductPrice = ProductCMD.Parameters.Add("@ProductPrice", SqlDbType.Float);
                    ProductPrice.Direction = ParameterDirection.Input;
                    ProductPrice.Value = price;

                    SqlParameter ProductStock = ProductCMD.Parameters.Add("@ProductStock", SqlDbType.Float);
                    ProductStock.Direction = ParameterDirection.Input;
                    ProductStock.Value = stock;

                    SqlParameter ProductURL = ProductCMD.Parameters.Add("@ProductURL", SqlDbType.NVarChar, 400);
                    ProductURL.Direction = ParameterDirection.Input;
                    ProductURL.Value = url;

                    SqlParameter Result = ProductCMD.Parameters.Add("Result", SqlDbType.Bit);
                    Result.Direction = ParameterDirection.ReturnValue;

                    commtest1996.Open();

                    SqlDataReader myReader = ProductCMD.ExecuteReader();

                    result = Convert.ToInt32(Result.Value);

                    myReader.Close(); // Close Command

                    commtest1996.Close(); // Close Database Connection
                }
            }

            return result;
        } //COMM Ready

        static public int EditProduct(string id, string name, double price, double stock)
        {
            int result = 0;

            using (SqlConnection commtest1996 = new SqlConnection(CommDatabaseConnection))
            {
                using (SqlCommand ProductCMD = new SqlCommand("EditProduct", commtest1996))
                {
                    ProductCMD.CommandType = CommandType.StoredProcedure;

                    SqlParameter ProductID = ProductCMD.Parameters.Add("@ProductID", SqlDbType.NVarChar, 255);
                    ProductID.Direction = ParameterDirection.Input;
                    ProductID.Value = id;

                    SqlParameter ProductName = ProductCMD.Parameters.Add("@ProductName", SqlDbType.NChar, 15);
                    ProductName.Direction = ParameterDirection.Input;
                    ProductName.Value = name;

                    SqlParameter ProductPrice = ProductCMD.Parameters.Add("@ProductPrice", SqlDbType.Float);
                    ProductPrice.Direction = ParameterDirection.Input;
                    ProductPrice.Value = price;

                    SqlParameter ProductStock = ProductCMD.Parameters.Add("@ProductStock", SqlDbType.Float);
                    ProductStock.Direction = ParameterDirection.Input;
                    ProductStock.Value = stock;

                    SqlParameter Result = ProductCMD.Parameters.Add("Result", SqlDbType.Bit);
                    Result.Direction = ParameterDirection.ReturnValue;

                    commtest1996.Open();

                    SqlDataReader myReader = ProductCMD.ExecuteReader();

                    result = Convert.ToInt32(Result.Value);

                    myReader.Close(); // Close Command

                    commtest1996.Close(); // Close Database Connection
                }
            }

            return result;
        } //COMM Ready

        static public User LogIn(string email, string password)
        {
            User user = new User();

            using (SqlConnection commtest1996 = new SqlConnection(CommDatabaseConnection))
            {
                using (SqlCommand LogInCMD = new SqlCommand("LogIn", commtest1996)) // Create New Command calling 'LogIn' stored procedure in boothtest database
                {
                    LogInCMD.CommandType = CommandType.StoredProcedure;

                    // Input Variables
                    SqlParameter inputEmail = LogInCMD.Parameters.Add("@ExUserEmail", SqlDbType.NVarChar, 30);
                    inputEmail.Direction = ParameterDirection.Input;

                    SqlParameter inputPassword = LogInCMD.Parameters.Add("@ExuserPassword", SqlDbType.NVarChar, 30);
                    inputPassword.Direction = ParameterDirection.Input;

                    inputEmail.Value = email;
                    inputPassword.Value = password;

                    commtest1996.Open(); // Open Database Connection

                    SqlDataReader myReader = LogInCMD.ExecuteReader(); // Execute Command

                    while (myReader.Read())
                    {
                        user.UserID = myReader["ID"].ToString();
                        user.UserFirstName = myReader["FirstName"].ToString();
                        user.UserSurname = myReader["Surname"].ToString();
                        user.UserMobileNumber = myReader["Phone"].ToString();
                        user.UserEmail = myReader["Email"].ToString();
                        user.UserType = myReader["UserType"].ToString();
                    }

                    myReader.Close(); // Close Command

                    commtest1996.Close(); // Close Database Connection
                }
            }

            return user;
        } //COMM Ready

        static public string Register(string firstname, string surname, string mobilenumber, string email, string password)
        {
            string emailExists = null;

            using (SqlConnection commtest1996 = new SqlConnection(CommDatabaseConnection))
            {
                using (SqlCommand RegisterCMD = new SqlCommand("Register", commtest1996))
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

                    commtest1996.Open(); // Open Database Connection

                    SqlDataReader myReader = RegisterCMD.ExecuteReader(); // Execute Command

                    emailExists = Convert.ToString(returnEmailExists.Value);

                    myReader.Close(); // Close Command

                    commtest1996.Close(); // Close Database Connection
                }
            }

            return emailExists;
        } //COMM Ready

        static public List<User> ReturnStylists()
        {
            List<User> stylists = new List<User>();

            using (SqlConnection commtest1996 = new SqlConnection(CommDatabaseConnection))
            {
                using (SqlCommand StylistsCMD = new SqlCommand("ReturnStylists", commtest1996))
                {
                    StylistsCMD.CommandType = CommandType.StoredProcedure;

                    commtest1996.Open();

                    SqlDataReader myReader = StylistsCMD.ExecuteReader();

                    stylists = new List<User>();

                    while (myReader.Read())
                    {
                        User stylist = new User();

                        stylist.UserID = myReader["ID"].ToString();
                        stylist.UserFirstName = myReader["FirstName"].ToString();
                        stylist.UserSurname = myReader["Surname"].ToString();
                        stylist.UserMobileNumber = myReader["Phone"].ToString();
                        stylist.UserEmail = myReader["Email"].ToString();
                        stylist.UserType = myReader["UserType"].ToString();

                        stylists.Add(stylist);
                    }

                    myReader.Close(); // Close Command

                    commtest1996.Close(); // Close Database Connection
                }
            }

            return stylists;
        } //COMM Ready

        static public string InsertStylist(string firstname, string surname, string mobilenumber, string email, string password)
        {
            string emailExists = null;

            using (SqlConnection commtest1996 = new SqlConnection(CommDatabaseConnection))
            {
                using (SqlCommand StylistCMD = new SqlCommand("InsertStylist", commtest1996))
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

                    SqlParameter returnEmailExists = StylistCMD.Parameters.Add("Result", SqlDbType.Int);
                    returnEmailExists.Direction = ParameterDirection.ReturnValue;

                    inputFirstName.Value = firstname;
                    inputSurname.Value = surname;
                    inputMobileNumber.Value = mobilenumber;
                    inputEmail.Value = email;
                    inputPassword.Value = password;

                    commtest1996.Open(); // Open Database Connection

                    SqlDataReader myReader = StylistCMD.ExecuteReader(); // Execute Command

                    emailExists = Convert.ToString(returnEmailExists.Value);

                    myReader.Close(); // Close Command

                    commtest1996.Close(); // Close Database Connection
                }
            }

            return emailExists;
        } //COMM Ready

        static public List<Category> ReturnCategories()
        {
            List<Category> categories = new List<Category>();

            using (SqlConnection commtest1996 = new SqlConnection(CommDatabaseConnection))
            {
                using (SqlCommand CategoriesCMD = new SqlCommand("ReturnCategoriesLength", commtest1996))
                {
                    CategoriesCMD.CommandType = CommandType.StoredProcedure;

                    commtest1996.Open();

                    SqlDataReader myReader = CategoriesCMD.ExecuteReader();

                    categories = new List<Category>();

                    while (myReader.Read())
                    {
                        Category category = new Category();

                        category.ID = myReader["id"].ToString();
                        category.Name = myReader["CategoryName"].ToString();
                        category.Length = Convert.ToDouble(myReader["Length"].ToString());

                        categories.Add(category);
                    }

                    myReader.Close(); // Close Command

                    commtest1996.Close(); // Close Database Connection
                }
            }

            return categories;
        }

        static public int InsertCategory(string name, double length)
        {
            int result = 0;

            using (SqlConnection commtest1996 = new SqlConnection(CommDatabaseConnection))
            {
                using (SqlCommand CategoryCMD = new SqlCommand("InsertCategory", commtest1996))
                {
                    CategoryCMD.CommandType = CommandType.StoredProcedure;

                    SqlParameter CategoryName = CategoryCMD.Parameters.Add("@CategoryName", SqlDbType.NVarChar, 255);
                    CategoryName.Direction = ParameterDirection.Input;
                    CategoryName.Value = name;

                    SqlParameter Length = CategoryCMD.Parameters.Add("@Length", SqlDbType.Float);
                    Length.Direction = ParameterDirection.Input;
                    Length.Value = length;

                    SqlParameter Result = CategoryCMD.Parameters.Add("Result", SqlDbType.Bit);
                    Result.Direction = ParameterDirection.ReturnValue;

                    commtest1996.Open();

                    SqlDataReader myReader = CategoryCMD.ExecuteReader();

                    result = Convert.ToInt32(Result.Value);

                    myReader.Close(); // Close Command

                    commtest1996.Close(); // Close Database Connection
                }
            }

            return result;
        }

        static public List<TimetableDetails> TimetableDetails()
        {
            List<TimetableDetails> details = new List<TimetableDetails>();

            using (SqlConnection commtest1996 = new SqlConnection(CommDatabaseConnection))
            {
                using (SqlCommand DetailsCMD = new SqlCommand("TimetableDetails", commtest1996))
                {
                    DetailsCMD.CommandType = CommandType.StoredProcedure;

                    commtest1996.Open();

                    SqlDataReader myReader = DetailsCMD.ExecuteReader();

                    details = new List<TimetableDetails>();

                    while (myReader.Read())
                    {
                        TimetableDetails detail = new TimetableDetails();

                        detail.Stylist = myReader["Stylist"].ToString();
                        detail.User = myReader["User"].ToString();
                        detail.Treatment = myReader["Treatment"].ToString();
                        detail.BeginSlot = int.Parse(myReader["Slot"].ToString());
                        detail.EndSlot = int.Parse(myReader["EndSlot"].ToString());
                        detail.Day = int.Parse(myReader["Day"].ToString());
                        detail.Month = int.Parse(myReader["Month"].ToString());
                        detail.Year = int.Parse(myReader["Year"].ToString());

                        details.Add(detail);
                    }

                    myReader.Close(); // Close Command

                    commtest1996.Close(); // Close Database Connection
                }
            }

            return details;
        } //COMM Ready

        static public void DeleteItem(string id, string item)
        {
            using (SqlConnection commtest1996 = new SqlConnection(CommDatabaseConnection))
            {
                using (SqlCommand DeleteCMD = new SqlCommand("DeleteItem", commtest1996))
                {
                    DeleteCMD.CommandType = CommandType.StoredProcedure;

                    SqlParameter ID = DeleteCMD.Parameters.Add("@ID", SqlDbType.NVarChar, 255);
                    ID.Direction = ParameterDirection.Input;
                    ID.Value = id;

                    SqlParameter Item = DeleteCMD.Parameters.Add("@Item", SqlDbType.NVarChar, 255);
                    Item.Direction = ParameterDirection.Input;
                    Item.Value = item;

                    commtest1996.Open();

                    SqlDataReader myReader = DeleteCMD.ExecuteReader();

                    myReader.Close(); // Close Command

                    commtest1996.Close(); // Close Database Connection
                }
            }
        } //COMM Ready

        static public List<StoreOrderDetails> ReturnStoreOrders()
        {
            List<StoreOrderDetails> details = new List<StoreOrderDetails>();

            using (SqlConnection commtest1996 = new SqlConnection(CommDatabaseConnection))
            {
                using (SqlCommand ProductsCMD = new SqlCommand("ReturnStoreOrders", commtest1996))
                {
                    ProductsCMD.CommandType = CommandType.StoredProcedure;

                    commtest1996.Open();

                    SqlDataReader myReader = ProductsCMD.ExecuteReader();

                    details = new List<StoreOrderDetails>();

                    while (myReader.Read())
                    {
                        StoreOrderDetails detail = new StoreOrderDetails();

                        detail.OrderNo = myReader["OrderNo"].ToString();
                        detail.OrderDate = Convert.ToDateTime(myReader["OrderDate"].ToString());
                        detail.ProductName = myReader["ProductName"].ToString();
                        detail.Quantity = myReader["Quantity"].ToString();
                        detail.UserName = myReader["UserName"].ToString();

                        details.Add(detail);
                    }

                    myReader.Close(); // Close Command

                    commtest1996.Close(); // Close Database Connection
                }
            }

            return details;
        } //COMM Ready

        // Statistic Control Methods

        static public List<ProductOrderCount> ProductOrderCount()
        {
            List<ProductOrderCount> products = new List<ProductOrderCount>();

            using (SqlConnection commtest1996 = new SqlConnection(CommDatabaseConnection))
            {
                using (SqlCommand ProductsCMD = new SqlCommand("DataProductCountOrders", commtest1996))
                {
                    ProductsCMD.CommandType = CommandType.StoredProcedure;

                    commtest1996.Open();

                    SqlDataReader myReader = ProductsCMD.ExecuteReader();

                    products = new List<ProductOrderCount>();

                    while (myReader.Read())
                    {
                        ProductOrderCount product = new ProductOrderCount();

                        product.ProductID = myReader["ID"].ToString();
                        product.ProductName = myReader["ProductName"].ToString();
                        product.NoOfOrders = myReader["NoOfOrders"].ToString();

                        products.Add(product);
                    }

                    myReader.Close(); // Close Command

                    commtest1996.Close(); // Close Database Connection
                }
            }

            return products;
        } //COMM Ready

        // Method for Getting all Categories
        public static List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();

            string db = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection boothtest = new SqlConnection(db))
            {
                //"Server=tcp:boothserver.database.windows.net,1433;" + "Initial Catalog=boothtest;Persist Security Info=False;" + "User ID=S00163774;Password=BOOTHserver%163774;" + "MultipleActiveResultSets=False;Encrypt=True;" + "TrustServerCertificate=False;Connection Timeout=30;"

                using (SqlCommand CategoryCMD = new SqlCommand("ReturnCategories", boothtest))
                {
                    CategoryCMD.CommandType = CommandType.StoredProcedure;

                    boothtest.Open();

                    SqlDataReader myReader = CategoryCMD.ExecuteReader();

                    categories = new List<Category>();

                    while (myReader.Read())
                    {
                        Category category = new Category
                        {
                            ID = myReader["ID"].ToString(),
                            Name = myReader["CategoryName"].ToString(),
                        };

                        categories.Add(category);
                    }

                    myReader.Close();
                    boothtest.Close();
                }
            }
            return categories;
        }

        // Methods for Booking and Time Slots
        public static List<string> TimeSlots(DateTime date)
        {
            List<int> takenTimes = GetTakenTimes(date);
            List<string> availableTimes = GetFreeTimes(takenTimes);
            return availableTimes;
        }

        static List<int> GetTakenTimes(DateTime date)
        {
            using (SqlConnection db = new SqlConnection(Db))
            {
                List<int> slotsTaken = new List<int>();

                using (SqlCommand DatesCMD = new SqlCommand("GetDates", db))
                {
                    DatesCMD.CommandType = CommandType.StoredProcedure;

                    SqlParameter inputDate = DatesCMD.Parameters.Add("@EDate", SqlDbType.DateTime);
                    inputDate.Direction = ParameterDirection.Input;

                    inputDate.Value = date;

                    db.Open();

                    SqlDataReader myReader = DatesCMD.ExecuteReader();

                    while (myReader.Read())
                    {
                        int slot = int.Parse(myReader["Slot"].ToString());

                        slotsTaken.Add(slot);
                    }

                    myReader.Close();
                    db.Close();

                    return slotsTaken;
                }
            }
        }

        static List<string> GetFreeTimes(List<int> list)
        {
            List<string> times = new List<string>();

            if (!list.Contains(1)) { times.Add("09:00"); }
            if (!list.Contains(2)) { times.Add("09:30"); }
            if (!list.Contains(3)) { times.Add("10:00"); }
            if (!list.Contains(4)) { times.Add("10:30"); }
            if (!list.Contains(5)) { times.Add("11:00"); }
            if (!list.Contains(6)) { times.Add("11:30"); }
            if (!list.Contains(7)) { times.Add("12:00"); }
            if (!list.Contains(8)) { times.Add("12:30"); }
            if (!list.Contains(9)) { times.Add("13:00"); }
            if (!list.Contains(10)) { times.Add("13:30"); }
            if (!list.Contains(11)) { times.Add("14:00"); }
            if (!list.Contains(12)) { times.Add("14:30"); }
            if (!list.Contains(13)) { times.Add("15:00"); }
            if (!list.Contains(14)) { times.Add("15:30"); }
            if (!list.Contains(15)) { times.Add("16:00"); }
            if (!list.Contains(16)) { times.Add("16:30"); }
            if (!list.Contains(17)) { times.Add("17:00"); }
            if (!list.Contains(18)) { times.Add("17:30"); }

            return times;
        }

        static public int InsertBooking(string Name, string Category, int Slot, string email, DateTime date, string stylist)
        {
            int result = 0;
            using (SqlConnection db = new SqlConnection(Db))
            {
                using (SqlCommand InsertBookingCMD = new SqlCommand("InsertBooking", db))
                {
                    InsertBookingCMD.CommandType = CommandType.StoredProcedure;

                    SqlParameter inputEmail = InsertBookingCMD.Parameters.Add("@EEmail", SqlDbType.NVarChar);
                    inputEmail.Direction = ParameterDirection.Input;

                    SqlParameter inputName = InsertBookingCMD.Parameters.Add("@EBookingName", SqlDbType.NVarChar, 50);
                    inputName.Direction = ParameterDirection.Input;

                    SqlParameter inputSlot = InsertBookingCMD.Parameters.Add("@ESlot", SqlDbType.Float);
                    inputSlot.Direction = ParameterDirection.Input;

                    SqlParameter inputProcedure = InsertBookingCMD.Parameters.Add("@EProcedure", SqlDbType.NVarChar, 50);
                    inputProcedure.Direction = ParameterDirection.Input;

                    SqlParameter inputStylist = InsertBookingCMD.Parameters.Add("@EStylist", SqlDbType.NVarChar, 50);
                    inputStylist.Direction = ParameterDirection.Input;

                    SqlParameter InputDate = InsertBookingCMD.Parameters.Add("@EDate", SqlDbType.DateTime);
                    InputDate.Direction = ParameterDirection.Input;

                    SqlParameter Result = InsertBookingCMD.Parameters.Add("Result", SqlDbType.Bit);
                    Result.Direction = ParameterDirection.ReturnValue;

                    inputEmail.Value = email;
                    inputName.Value = Name;
                    inputSlot.Value = Slot;
                    inputProcedure.Value = Category;
                    inputStylist.Value = stylist;
                    InputDate.Value = date;

                    db.Open();

                    InsertBookingCMD.ExecuteNonQuery();

                    result = Convert.ToInt32(Result.Value);

                    return result;
                }
            }
        }

        public static void EditBooking(string Id, int slot)
        {
            using (SqlConnection db = new SqlConnection(Db))
            {
                using (SqlCommand InsertBookingCMD = new SqlCommand("UpdateBooking", db))
                {
                    InsertBookingCMD.CommandType = CommandType.StoredProcedure;

                    SqlParameter inputID = InsertBookingCMD.Parameters.Add("@EBookingID", SqlDbType.NVarChar);
                    inputID.Direction = ParameterDirection.Input;

                    SqlParameter inputSlot = InsertBookingCMD.Parameters.Add("@ESlot", SqlDbType.Int);
                    inputSlot.Direction = ParameterDirection.Input;

                    inputID.Value = Id;
                    inputSlot.Value = slot;

                    db.Open();

                    InsertBookingCMD.ExecuteNonQuery();
                }
            }
        }

        public static int GetBookingSlots(string bookingTime)
        {
            if (bookingTime == "09:00") { return 1; }
            if (bookingTime == "09:30") { return 2; }
            if (bookingTime == "10:00") { return 3; }
            if (bookingTime == "10:30") { return 4; }
            if (bookingTime == "11:00") { return 5; }
            if (bookingTime == "11:30") { return 6; }
            if (bookingTime == "12:00") { return 7; }
            if (bookingTime == "12:30") { return 8; }
            if (bookingTime == "13:00") { return 9; }
            if (bookingTime == "13:30") { return 10; }
            if (bookingTime == "14:00") { return 11; }
            if (bookingTime == "14:30") { return 12; }
            if (bookingTime == "15:00") { return 13; }
            if (bookingTime == "15:30") { return 14; }
            if (bookingTime == "16:00") { return 15; }
            if (bookingTime == "16:30") { return 16; }
            if (bookingTime == "17:00") { return 17; }
            if (bookingTime == "17:30") { return 18; }
            else { return 0; }
        }

        public static List<Booking> GetEditBookings(string email)
        {
            List<Booking> bookings = new List<Booking>();
            using (SqlConnection db = new SqlConnection(Db))
            {
                using (SqlCommand InsertBookingCMD = new SqlCommand("ReturnBooking", db))
                {
                    InsertBookingCMD.CommandType = CommandType.StoredProcedure;

                    SqlParameter inputEmail = InsertBookingCMD.Parameters.Add("@EEmail", SqlDbType.NVarChar);
                    inputEmail.Direction = ParameterDirection.Input;

                    SqlParameter Result = InsertBookingCMD.Parameters.Add("Result", SqlDbType.Bit);
                    Result.Direction = ParameterDirection.ReturnValue;

                    inputEmail.Value = email;

                    db.Open();

                    SqlDataReader rdr = InsertBookingCMD.ExecuteReader();

                    bookings = new List<Booking>();

                    while (rdr.Read())
                    {
                        Booking booking = new Booking
                        {
                            ID = rdr["ID"].ToString(),
                            Name = rdr["BookingName"].ToString(),
                            Date = Convert.ToDateTime(rdr["Date"].ToString()),
                            Email = rdr["Email"].ToString(),
                            Slot = int.Parse(rdr["Slot"].ToString()),
                            Procedure = rdr["Treatment"].ToString()
                        };

                        bookings.Add(booking);
                    }
                }
                return bookings;
            }
        }

        public static List<User> GetStylists()
        {
            List<User> user = new List<User>();

            string db = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

            using (SqlConnection boothtest = new SqlConnection(db))
            {
                using (SqlCommand ProductsCMD = new SqlCommand("GetStylists", boothtest))
                {
                    ProductsCMD.CommandType = CommandType.StoredProcedure;

                    boothtest.Open();

                    SqlDataReader myReader = ProductsCMD.ExecuteReader();

                    user = new List<User>();

                    while (myReader.Read())
                    {
                        User product = new User
                        {
                            UserID = myReader["id"].ToString(),
                            UserFirstName = myReader["StylistName"].ToString(),
                        };
                        //product.ProductCategoryID = int.Parse(myReader["ProductCategoryID"].ToString());

                        user.Add(product);
                    }

                    myReader.Close();
                    boothtest.Close();

                    return user;
                }
            }
        }

        //public static List<User> GetStylists()
        //{

        //}

        // Code to Generate a random string from user Guffa on codereview.stackexhange
        // https://codereview.stackexchange.com/questions/5983/random-string-generation
        // https://codereview.stackexchange.com/a/5994
        public static string GenerateCoupon(int length)
        {
            Random random = new Random();
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }
    }
}