using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FoodOrdering.Application.Features.OrderFeatures.OrderCreate
{
    public class PatternPhone : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            Regex reg = new Regex("^(01)(0|1|2|5)[0-9]{8}$");

            if (value == null)
                return null;
            string phone = value.ToString();
            if (reg.IsMatch(phone) == false)
            {
                return new ValidationResult("Invalid Phone Number");
            }
            return ValidationResult.Success;
        }
    }
}
