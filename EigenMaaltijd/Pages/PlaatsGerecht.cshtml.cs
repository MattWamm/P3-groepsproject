using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EigenMaaltijd.Pages
{
    public class PlaatsGerechtModel : PageModel
    {

        [BindProperty]
        public Meal meal { get; set; }

        public void OnGet()
        {

        }

        public void Ingredients()
        {
            Meal meel = meal;
            //string what = meal.Ingredients;
            //string[] whatList = what.Split(',');
            //    string ever = "";
            //foreach (string yes in whatList)
            //{
            //    ever = ever = "," + yes + ",";
            //}
            //meel.Ingredients = ever;

            new MealRepository().AddMeal(meel);
        }

        [BindProperty]
        public User LogUser
        {
            get
            {
                string cookie = Request.Cookies["keepLogin"];
                return new UserRepository().getUserFromID(Convert.ToInt32(cookie));
            }
        }

        public IActionResult OnPostPlaats()
        {
            meal.UserID = LogUser.UserID;
            new MealRepository().AddMeal(meal);

            return RedirectToPage("Index");
        }
    }

}