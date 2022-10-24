using Food_Restaurant.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Food_Restaurant.BL.ValidationAttributes
{
    public class UniqueUsername : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            EntityContext context = (EntityContext)validationContext.GetService(typeof(EntityContext));
            if (value == null)
                return null;
            string newname = value.ToString();
            var username = context.Users.FirstOrDefault(s=>s.UserName==newname);
            if (username != null)
            {
                return new ValidationResult("Username Already Exist");
            }
            return ValidationResult.Success;
        }
    }
}
