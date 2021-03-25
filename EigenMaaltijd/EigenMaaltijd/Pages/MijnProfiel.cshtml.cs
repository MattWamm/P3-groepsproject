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
        public User LogUser { get; set; }



        public void OnGet()
        {
            string cookie = Request.Cookies["keepLogin"];
            if (cookie != null)
            {
                LogUser = new UserRepository().getUserFromID(Convert.ToInt32(cookie));
            }
        }
    }
}
