using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EigenMaaltijd.Pages
{
    public class Index2Model : PageModel
    {
        [BindProperty]
        public User LogUser 
        { get 
            {
                return new UserRepository().getUserFromID((int)ViewData["keepLogin"]);
            } 
        }
        public LoginUser LoginData 
        {
            get 
            {
                return new UserRepository().getLoginUserFromID((int)ViewData["keepLogin"]);
            }
        }

        public IActionResult OnGet()
        {
            ViewData["keepLogin"] = HttpContext.Session.GetInt32("keepLogin");
            if (ViewData["keepLogin"] != null)
            {
                return new PageResult();
            }
            else
            {
                return RedirectToPage("Inloggen");
            }
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("keepLogin");
            return RedirectToPage("Index");
        }
    }
}
