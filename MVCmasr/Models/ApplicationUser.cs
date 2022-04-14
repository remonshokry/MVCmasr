using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MVCmasr.Models
{
    public class ApplicationUser : IdentityUser
    {
        
        [Range(0,50)]
        public string Address { get; set; }

        public string CreditCardNumber { get; set; }

    }
}