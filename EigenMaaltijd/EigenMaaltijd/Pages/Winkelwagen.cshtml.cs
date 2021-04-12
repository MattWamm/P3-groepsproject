using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using EigenMaaltijd.Pages.repository_folder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace EigenMaaltijd.Pages
{
    public class WinkelwagenModel : PageModel
    {
        [BindProperty]

        public Mail sendmail { get; set; }

        public async Task OnPost()
        {
            string to = sendmail.To;
            string subject = sendmail.Subject;
            string body = sendmail.Body;
            MailMessage mm = new MailMessage();
            mm.To.Add(to);
            mm.Subject = "Bestelling Eigenmaaltijd";
            mm.Body = body;
            mm.IsBodyHtml = false;
            mm.From = new MailAddress("eigenmaaltijd@gmail.com");
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("eigenmaaltijd@gmail.com", "JurZijnZusIsHot");
            await smtp.SendMailAsync(mm);
            ViewData["Message"] = "The Mail Has Been Sent To " + sendmail.To;
        }

        public List<IndexMeal> IMeals
        {
            get
            {
                List<IndexMeal> returnList = new List<IndexMeal>();
                foreach (Meal meal in new MealRepository().Search(SearchTerm))
                {
                    IndexMeal IMeal = new IndexMeal();
                    IMeal.meal = meal;
                    IMeal.user = new UserRepository().getUserFromID(meal.UserID);
                    returnList.Add(IMeal);

                    String img64 = Convert.ToBase64String(meal.Img);
                    String img64Url = string.Format("data:image/" + "jpg" + ";base64,{0}", img64);
                    IMeal.img64Url = img64Url;

                }
                return returnList;
            }

        }
        public User LogUser
        {
            get
            {

                ViewData["keepLogin"] = HttpContext.Session.GetInt32("keepLogin");
                if (ViewData["keepLogin"] != null)
                {
                    return new UserRepository().getUserFromID((int)ViewData["keepLogin"]);
                }
                return null;

            }
        }


        public List<OrderItem>ListOrders
            { 
            get 
            
            {
                List<OrderItem> Order = new List<OrderItem>();
                foreach (Shoppinglist Bestelling in new UserRepository().GetShoppinglists(LogUser.UserID)) 
                {
                    

                    string prijs = new MealRepository().GetMealFromMealID(Bestelling.MealID).Prijs;
                    string naam = new MealRepository().GetMealFromMealID(Bestelling.MealID).Name;
                    OrderItem Meal = new OrderItem()
                    {
                        Prijs = prijs,
                        Naam = naam
                    };
                
                    Order.Add(Meal);

                }
                return Order;

            } 
        }


        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public string ButtonTextLogin
        {
            get
            {
                if (LogUser != null)
                {
                    return "Afmelden";
                }
                return "Aanmelden";
            }
        }
    }
    public class OrderItem
    {
        public string Prijs { get; set; }
        public string Naam { get; set; }

    }

}
