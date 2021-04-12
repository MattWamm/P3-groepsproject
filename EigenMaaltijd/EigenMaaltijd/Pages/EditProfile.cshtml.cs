using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EigenMaaltijd.Pages
{
    public class EditProfileModel : PageModel
    {
        [BindProperty]
        public User UserData
        {
            get
            {
                string cookie = Request.Cookies["keepLogin"];
                return new UserRepository().getUserFromID(Convert.ToInt32(cookie));
            }
        }
        [BindProperty]
        public LoginUser LoginData
        {
            get
            {
                string cookie = Request.Cookies["keepLogin"];
                return new UserRepository().getLoginUserFromID(Convert.ToInt32(cookie));
            }
        }

        [BindProperty]
        public RegisterLUser RegisterData { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnGetLogout()
        {
            Response.Cookies.Delete("keepLogin");
            return RedirectToPage("Index");
        }
    }
}
