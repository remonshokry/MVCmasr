using MVCmasr.Context;
using MVCmasr.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MVCmasr.Validations
{
    public class UniquePhoneAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var phone = value as string;
            var UserToCheck = validationContext.ObjectInstance as ApplicationUser;

            var context = (ApplicationDbContext)validationContext
                         .GetService(typeof(ApplicationDbContext));

            if (value != null)
            {
                var usersPhones = context.Users.Select(user => user.PhoneNumber);
                var isExist = usersPhones.Any(userPhone => userPhone == phone);

                if (isExist)
                {
                    return new ValidationResult("Phone Exists");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
