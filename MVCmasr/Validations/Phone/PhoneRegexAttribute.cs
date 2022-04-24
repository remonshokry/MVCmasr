using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MVCmasr.Validations
{
    public class PhoneRegexAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var phone = value as string;
            string pattern = "^(010|011|012)[0-9]{8}$";
            Regex rx = new Regex(pattern);

            if (value is null)
            {
                return true;
            }

            if (rx.IsMatch(phone))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
