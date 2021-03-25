using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

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


        public void OnGet()
        {

        }
    }
}
