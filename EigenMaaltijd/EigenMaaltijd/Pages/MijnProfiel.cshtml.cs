using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                return new UserRepository().getUserFromID(Convert.ToInt32(Request.Cookies["keepLogin"]));
            } 
        }
        public LoginUser LoginData 
        {
            get 
            {
                return new UserRepository().getLoginUserFromID(Convert.ToInt32(Request.Cookies["keepLogin"]));
            }
        }

        public IActionResult OnGet()
        {
            string cookie = Request.Cookies["keepLogin"];
            if (cookie != null)
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
            Response.Cookies.Delete("keepLogin");
            return RedirectToPage("Index");
        }
    }
}
