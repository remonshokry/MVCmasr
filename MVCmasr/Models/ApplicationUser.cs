using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MVCmasr.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } 

        [StringLength(100)]
        public string Address { get; set; }

        [Required, Range(3, 100)]
        public int Age { get; set; }

        public string CreditCardNumber { get; set; }

        [Required]
        public byte[] Picture { get; set; }

    }
}