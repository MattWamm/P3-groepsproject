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
using Microsoft.JSInterop;
using Newtonsoft.Json;

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



        public void OnPostList()
        {
            {
                MemoryStream stream = new MemoryStream();

                Request.Body.CopyTo(stream);
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream))
                {
                    string requestBody = reader.ReadToEnd();
                    if (requestBody.Length > 0)
                    {
                        var obj = JsonConvert.DeserializeObject<PostData>(requestBody);
                        if (obj != null)
                        {
                            HttpContext.Session.SetInt32("ClickedMeal", Convert.ToInt32(obj.Item1));
                        }
                    }
                }
            }
        }
        public Meal ClickedMeal 
        {
            get
            {
                ViewData["ClickedMeal"] = HttpContext.Session.GetInt32("ClickedMeal");

                if (ViewData["ClickedMeal"] != null)
                {
                    int mealID = (int)ViewData["ClickedMeal"];
                    if (mealID != 0)
                    {
                        return new MealRepository().GetMealFromMealID(mealID);
                    }
                    else
                    {
                        Meal meal = new Meal();

                        return meal;
                    }

                }
                else 
                {
                    Meal meal = new Meal();

                    return meal;
                }
            }
        
        }

    }





    public class PostData
    {
        public string Item1 { get; set; }
        public string Item2 { get; set; }
        public string Item3 { get; set; }
    }

    public class IndexMeal
    {
        public Meal meal { get; set; }
        public User user { get; set; }
        public String img64Url { get; set; }
    }
}
