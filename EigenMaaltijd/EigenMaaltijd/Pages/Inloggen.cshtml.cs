using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EigenMaaltijd.Pages
{
    public class InloggenModel : PageModel
    {

        [BindProperty]
        public User UserData { get; set; }

        [BindProperty]
        public LoginUser LoginData { get; set; }

        [BindProperty]
        public RegisterLUser RegisterData { get; set; }
        

        public void OnGet()
        {
        }

        public IActionResult OnPostRegister()
        {
            int whatever = new UserRepository().Register(UserData, RegisterData);

            User user = new UserRepository().getUserFromID(whatever);
            if (User != null)
            {
                CookieOptions cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(1),
                    IsEssential = true
                };
                Response.Cookies.Append("keepLogin",user.UserID.ToString(), cookieOptions);
            }
            return RedirectToPage("MijnProfiel");
        }

        public IActionResult OnPostLogin()
        {

            User user = new UserRepository().Login(LoginData.Email, LoginData.Password);

            if (user != null)
            {
                CookieOptions cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(1),
                    IsEssential = true
                };
                Response.Cookies.Append("keepLogin", user.UserID.ToString(), cookieOptions);
            }
            return RedirectToPage("MijnProfiel");
        }


    }
    public class tempData : Controller
    {
    }
}
