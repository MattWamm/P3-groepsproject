using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EigenMaaltijd.Pages
{
    public class ProfielPaginaModel : PageModel
    {
        [BindProperty]
        public User UserData
        {
            get
            {
                ViewData["postData"] = HttpContext.Session.GetInt32("postData");
                return new UserRepository().getUserFromID((int)ViewData["postData"]);
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
        public IActionResult OnPostGetData()
        {
            ViewData["postData"] = HttpContext.Session.GetInt32("postData");
            if (ViewData["postData"] != null)
            {
                return new PageResult();
            }
            else
            {
                return RedirectToPage("Index");
            }
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("keepLogin");
            return RedirectToPage("Index");
        }
    }
}
