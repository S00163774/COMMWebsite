using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Stripe;

namespace HairSalon_Website
{
    public class Product
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public double ProductStock { get; set; }
        public string ProductURL { get; set; }

        public override string ToString()
        {
            return this.ProductID + "," + this.ProductName + "," + this.ProductPrice + "," + this.ProductStock;
        }
    }

    public class User
    {
        public string UserID { get; set; }
        public string UserFirstName { get; set; }
        public string UserSurname { get; set; }
        public string UserMobileNumber { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserType { get; set; }
    }

    public class TimetableDetails
    {
        public string Stylist { get; set; }
        public string User { get; set; }
        public string Treatment { get; set; }

        public int BeginSlot { get; set; }
        public int EndSlot { get; set; }

        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public int Hour { get; set; }
        public int Minutes { get; set; }

        public int EndHour { get; set; }
        public int EndMinutes { get; set; }

        public override string ToString()
        {
            return this.Stylist + "," + this.User + " - " + this.Treatment + "," + this.Day.ToString() + "," + this.Month.ToString() + "," + this.Year.ToString() + "," + this.Hour.ToString() + "," + this.Minutes.ToString() + "," + this.EndHour.ToString() + "," + this.EndMinutes.ToString();
        }
    }

    public class StoreOrderDetails
    {
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public string ProductName { get; set; }
        public string Quantity { get; set; }
        public string UserName { get; set; }
    }

    public class Checkout
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Postcode { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Email { get; set; }

        public static void PaymentMethod(int amount, string description)
        {
            // var amount = 100;
            //var payment = Stripe.StripeCharge.(amount);
            try
            {
                //use nu-get Stripe
                //set TLS 1.2 in androuid settings

                StripeConfiguration.SetApiKey("sk_test_BEPrGyKARA5fbK1rcLbAixdd");

                var chargeOptions = new StripeChargeCreateOptions()
                {
                    Amount = amount,
                    Currency = "usd",
                    Description = description,
                    SourceTokenOrExistingSourceId = "tok_visa",
                    Metadata = new Dictionary<String, String>()
                    {
                        { "OrderId", "6735" }
                    }
                };

                var chargeService = new StripeChargeService();
                StripeCharge charge = chargeService.Create(chargeOptions);
            }
            // Use Stripe's library to make request

            catch (StripeException ex)
            {
                switch (ex.StripeError.ErrorType)
                {
                    case "card_error":
                        System.Diagnostics.Debug.WriteLine("   Code: " + ex.StripeError.Code);
                        System.Diagnostics.Debug.WriteLine("Message: " + ex.StripeError.Message);
                        break;
                    case "api_connection_error":
                        System.Diagnostics.Debug.WriteLine(" apic  Code: " + ex.StripeError.Code);
                        System.Diagnostics.Debug.WriteLine("apic Message: " + ex.StripeError.Message);
                        break;
                    case "api_error":
                        System.Diagnostics.Debug.WriteLine("api   Code: " + ex.StripeError.Code);
                        System.Diagnostics.Debug.WriteLine("api Message: " + ex.StripeError.Message);
                        break;
                    case "authentication_error":
                        System.Diagnostics.Debug.WriteLine(" auth  Code: " + ex.StripeError.Code);
                        System.Diagnostics.Debug.WriteLine("auth Message: " + ex.StripeError.Message);
                        break;
                    case "invalid_request_error":
                        System.Diagnostics.Debug.WriteLine(" invreq  Code: " + ex.StripeError.Code);
                        System.Diagnostics.Debug.WriteLine("invreq Message: " + ex.StripeError.Message);
                        break;
                    case "rate_limit_error":
                        System.Diagnostics.Debug.WriteLine("  rl Code: " + ex.StripeError.Code);
                        System.Diagnostics.Debug.WriteLine("rl Message: " + ex.StripeError.Message);
                        break;
                    case "validation_error":
                        System.Diagnostics.Debug.WriteLine(" val  Code: " + ex.StripeError.Code);
                        System.Diagnostics.Debug.WriteLine("val Message: " + ex.StripeError.Message);
                        break;
                    default:
                        // Unknown Error Type
                        break;
                }
            }
        }

        public static void PaymentMethod(int amount)
        {
            // var amount = 100;
            //var payment = Stripe.StripeCharge.(amount);
            try
            {
                //use nu-get Stripe
                //set TLS 1.2 in androuid settings

                StripeConfiguration.SetApiKey("sk_test_BEPrGyKARA5fbK1rcLbAixdd");

                var chargeOptions = new StripeChargeCreateOptions()
                {
                    Amount = amount,
                    Currency = "eur",
                    SourceTokenOrExistingSourceId = "tok_visa",
                    Metadata = new Dictionary<String, String>()
                    {
                        { "OrderId", "6735" }
                    }
                };

                var chargeService = new StripeChargeService();
                StripeCharge charge = chargeService.Create(chargeOptions);
            }
            // Use Stripe's library to make request

            catch (StripeException ex)
            {
                switch (ex.StripeError.ErrorType)
                {
                    case "card_error":
                        System.Diagnostics.Debug.WriteLine("   Code: " + ex.StripeError.Code);
                        System.Diagnostics.Debug.WriteLine("Message: " + ex.StripeError.Message);
                        break;
                    case "api_connection_error":
                        System.Diagnostics.Debug.WriteLine(" apic  Code: " + ex.StripeError.Code);
                        System.Diagnostics.Debug.WriteLine("apic Message: " + ex.StripeError.Message);
                        break;
                    case "api_error":
                        System.Diagnostics.Debug.WriteLine("api   Code: " + ex.StripeError.Code);
                        System.Diagnostics.Debug.WriteLine("api Message: " + ex.StripeError.Message);
                        break;
                    case "authentication_error":
                        System.Diagnostics.Debug.WriteLine(" auth  Code: " + ex.StripeError.Code);
                        System.Diagnostics.Debug.WriteLine("auth Message: " + ex.StripeError.Message);
                        break;
                    case "invalid_request_error":
                        System.Diagnostics.Debug.WriteLine(" invreq  Code: " + ex.StripeError.Code);
                        System.Diagnostics.Debug.WriteLine("invreq Message: " + ex.StripeError.Message);
                        break;
                    case "rate_limit_error":
                        System.Diagnostics.Debug.WriteLine("  rl Code: " + ex.StripeError.Code);
                        System.Diagnostics.Debug.WriteLine("rl Message: " + ex.StripeError.Message);
                        break;
                    case "validation_error":
                        System.Diagnostics.Debug.WriteLine(" val  Code: " + ex.StripeError.Code);
                        System.Diagnostics.Debug.WriteLine("val Message: " + ex.StripeError.Message);
                        break;
                    default:
                        // Unknown Error Type
                        break;
                }
            }
        }

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

    public class Category
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
    }

    public class Booking
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Procedure { get; set; }
        public DateTime Date { get; set; }
        public int Slot { get; set; }

        public static int GetLength(string category)
        {
            // Variable used for returning value
            int length;
            // Set value for length of treatment
            switch (category)
            {
                case "Perm":
                    length = 4;
                    break;

                case "Colour":
                    length = 2;
                    break;

                case "Baliage":
                    length = 6;
                    break;
                default:
                    length = 0;
                    break;
            }
            // Return value
            return length;
        }
    }
}