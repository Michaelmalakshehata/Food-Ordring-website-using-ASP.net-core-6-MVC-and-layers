using Food_Restaurant.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Restaurant.BL.ValidationAttributes
{
    public class UniqueCategoryName : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            EntityContext context = (EntityContext)validationContext.GetService(typeof(EntityContext));
            if (value == null)
                return null;
            string newname = value.ToString();
            if (context.Categories.FirstOrDefault(s => s.Name == newname)!=null)
            {
                return new ValidationResult("Name Already Exist in Categories or restore it from deleted");
            }
            return ValidationResult.Success;
        }
    }
}
