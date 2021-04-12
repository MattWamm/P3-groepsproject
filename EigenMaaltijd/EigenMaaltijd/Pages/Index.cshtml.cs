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

        public ActionResult OnPostList()
        {
            string returnString = "";
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
                            HttpContext.Session.SetInt32("postData", Convert.ToInt32(new MealRepository().GetMealFromMealID(Convert.ToInt32(obj.Item1)).UserID));
                            returnString = obj.Item1;
                        }
                    }
                }
            }
            List<string> lstString = new List<string>
            {
            returnString
            };

                return new JsonResult(lstString);
        }
        
        public ClickedMeal ClickedMeal 
        {
            get
            {
                ViewData["ClickedMeal"] = HttpContext.Session.GetInt32("ClickedMeal");

                if (ViewData["ClickedMeal"] != null)
                {
                    int mealID = (int)ViewData["ClickedMeal"];
                    if (mealID != 0)
                    {
                        Meal meal = new MealRepository().GetMealFromMealID(mealID);

                        string[] splits = meal.Ingredients.Split(',');
                        ClickedMeal CMeal = new ClickedMeal()
                        {
                            MealID = meal.MealID,
                            UserID = meal.UserID,
                            Name = meal.Name,
                            Ingredients = splits.ToList<string>(),
                            Portions = meal.Portions,
                            PortionSize = meal.PortionSize,
                            Rating = meal.Rating,
                            Img = meal.Img,
                            Ingevroren = meal.Ingevroren,
                            Betalingsmethode = meal.Betalingsmethode,
                            Prijs = meal.Prijs
                        };
                        return CMeal;
                    }
                    else
                    {
                        return new ClickedMeal();
                    }

                }
                else 
                {
                    return new ClickedMeal();
                }
            }
        
        }

        public IActionResult OnPostClose()
        {
            return Page();
        }

        public PartialViewResult OnGetMealPartial()
        {
            return Partial("_PartialPopup", ClickedMeal);
        }

        
        public IActionResult OnPostChange(int quantity)
        {
            Meal meal = new MealRepository().GetMealFromMealID(ClickedMeal.MealID);

            meal.Portions -= quantity;

            new MealRepository().ChangeMeal(meal);

            return new PageResult();
        }
    }

    public class ClickedMeal
    {
        public int MealID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public List<string> Ingredients { get; set; }
        public int Portions { get; set; }
        public string PortionSize { get; set; }
        public float Rating { get; set; }
        public byte[] Img { get; set; }
        public bool Ingevroren { get; set; }
        public string Betalingsmethode { get; set; }
        public string Prijs { get; set; }
    }




    public class PostData
    {
        public string Item1 { get; set; }
    }

    public class IndexMeal
    {
        public Meal meal { get; set; }
        public User user { get; set; }
        public String img64Url { get; set; }
    }
}
