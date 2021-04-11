using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.WebPages.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EigenMaaltijd.Pages
{
    public class PlaatsGerechtModel : PageModel
    {

        [BindProperty]
        public Meal meal { get; set; }

        [BindProperty]
        public IFormFile test { get; set; }


        [BindProperty]
        public User LogUser
        {
            get
            {
                ViewData["keepLogin"] = HttpContext.Session.GetInt32("keepLogin");
                return new UserRepository().getUserFromID((int)ViewData["keepLogin"]);
            }
        }

        public IActionResult OnGet()
        {
            ViewData["keepLogin"] = HttpContext.Session.GetInt32("keepLogin");

            if (ViewData["keepLogin"] == null)
            {
                return RedirectToPage("Inloggen");
            }
            return new PageResult();
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

        public IActionResult OnPostPlaats()
        {
            meal.UserID = LogUser.UserID;
         
            using (MemoryStream ms = new MemoryStream())
            {
                test.CopyTo(ms);
                byte[] fileBytes = ms.ToArray();
                meal.Img = fileBytes;
            }
            new MealRepository().AddMeal(meal);

            return RedirectToPage("Index");
        }

       
    }

}