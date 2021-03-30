using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EigenMaaltijd.Pages
{
    public class Meal
    {

        public int MealID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public int Portions { get; set; }
        public string PortionSize { get; set; }
        public float Rating { get; set; } 
    }
}
