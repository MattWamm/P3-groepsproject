using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EigenMaaltijd.Pages
{
    public class ZoekMaaltijdModel : PageModel
    {
        public List<Meal> Meals
        {
            get
            {
                return new MealRepository().Search(SearchTerm);
            }
        }


        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }


        public void OnGet()
        {


        }

        public void OnPostSearch()
        {
            new MealRepository().Search(SearchTerm);

        }
    }
}
