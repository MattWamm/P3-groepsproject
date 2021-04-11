using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
<<<<<<< Updated upstream
=======
using Microsoft.JSInterop;
using Newtonsoft.Json;
using EigenMaaltijd.Pages.repository_folder;
>>>>>>> Stashed changes

namespace EigenMaaltijd.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
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

        public IActionResult OnGetLogout()
        {
            Response.Cookies.Delete("keepLogin");
            return RedirectToPage("Index");
        }


<<<<<<< Updated upstream
        public void OnGet()
=======

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
>>>>>>> Stashed changes
        {

<<<<<<< Updated upstream
        }
=======
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

>>>>>>> Stashed changes
    }
}
