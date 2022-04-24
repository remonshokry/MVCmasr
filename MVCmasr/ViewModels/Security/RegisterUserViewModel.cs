using MVCmasr.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace MVCmasr.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required]
        public string UserName { get; set; }    

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [PhoneRegex, Display(Name = "Phone Number")]
        [UniquePhone]
        public string Phone { get; set; }

        [Required,Range(3,100)]
        public int Age { get; set; }

        [Required,DataType(DataType.Password)]
        public string Password { get; set; }

        [Required,Display(Name ="Confirm Password"), DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password and Confirm Not Matched")]
        public string ConfirmPassword { get; set; }
        
        [StringLength(100)]
        public string Address { get; set; }

        public string Role { get; set; }
    }
}
