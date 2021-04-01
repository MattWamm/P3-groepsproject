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
            int userID = new UserRepository().Register(UserData, RegisterData);
            User user = new UserRepository().getUserFromID(userID);
            if (User != null)
            {
                HttpContext.Session.SetInt32("keepLogin", user.UserID);
            }
            return RedirectToPage("MijnProfiel");
        }

        public IActionResult OnPostLogin()
        {
            User user = new UserRepository().Login(LoginData.Email, LoginData.Password);

            if (user != null)
            {
                //session test
                HttpContext.Session.SetInt32("keepLogin", user.UserID);
                ViewData["keepLogin"] = HttpContext.Session.GetInt32("keepLogin");
            }
            return RedirectToPage("MijnProfiel");
        }
    }
    public class tempData : Controller
    {
    }
}
