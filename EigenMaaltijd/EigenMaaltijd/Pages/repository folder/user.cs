using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace EigenMaaltijd.Pages
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public DateTime GeboorteDatum { get; set; }
        public string Telefoon { get; set; }
        public string Adres { get; set; }
        public string Postcode { get; set; }
    }

    public class LoginUser
    { 
        public int UserID { get; set; }

        [EmailAddress, Required]
        public string Email { get; set; }

        [MinLength(8), Required]
        public string Password { get; set; }
    }

}
