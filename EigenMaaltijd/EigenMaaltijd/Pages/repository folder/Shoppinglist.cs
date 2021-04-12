using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EigenMaaltijd.Pages
{
    public class Shoppinglist
    {
        public int MealID { get; set; }

        public int Hoeveelheid { get; set; }

        public DateTime Date { get; set; }

        public int UserID { get; set; }
    }


}
