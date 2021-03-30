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

        private readonly MealRepository mealRepository;
        public IEnumerable<Meal> Meals
              {
                get
            {
                    return new MealRepository().GetAllMeals();
                }
        }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

     
        public void OnGet()
        {
           
        }

        public void OnGetSearch()
        {
          mealRepository.Search(SearchTerm);
        }
    }
}
