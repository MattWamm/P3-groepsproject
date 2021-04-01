using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace EigenMaaltijd.Pages
{
    public class IndexModel : PageModel
    {
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
                if (ViewData["keepLogin"] != null)
                {
                    return new UserRepository().getUserFromID((int)ViewData["keepLogin"]);
                }
                return null;

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

        public void OnGet()
        {
            ViewData["keepLogin"] = HttpContext.Session.GetInt32("keepLogin");
        }
        public void OnPostSearch()
        {
            new MealRepository().Search(SearchTerm);
        }
    }

    public class IndexMeal
    {
        public Meal meal { get; set; }
        public User user { get; set; }
        public String img64Url { get; set; }
    }
}
