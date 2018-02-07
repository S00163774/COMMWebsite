using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace HairSalon_Website
{
    public class ProductCategory
    {
        public int ProductCategoryID { get; set; }
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
        public string UserURL { get; set; }
    }

    public class Checkout
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Postcode { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Email { get; set; }

        public static void SendEmail(string emailUser)
        {
            MailMessage Msg = new MailMessage();
            Msg.From = new MailAddress("owenexampletest@gmail.com");
            Msg.To.Add(emailUser);
            Msg.Subject = "This is the Test Subject";
            Msg.Body = "This is the Test Body for the Project 300 email confirmation.";
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential("project300email@gmail.com", "Project300");
            smtp.EnableSsl = true;
            smtp.Send(Msg);
            //Console.WriteLine("Email Sent!");
        }

        public static void SendTextMessage(string number)
        {
            if (number.Length == 10)
            {
                number = number.Remove(0, 1);
            }

            // Your Account SID from twilio.com/console
            var accountSid = "AC0870a5e30dece345258735402d9f8e33";
            // Your Auth Token from twilio.com/console
            var authToken = "3a211e065304d39b9d5f699449fe7bcd";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                to: new PhoneNumber("+353" + number),
                from: new PhoneNumber("+353861802018"),
                body: "Mo n fi message se testing fun project 300 mi. L'agbara Olorun o ma sise.");

            Console.WriteLine(message.Sid);
            Console.Write("Press any key to continue.");
            Console.ReadKey();
        }
    }
}