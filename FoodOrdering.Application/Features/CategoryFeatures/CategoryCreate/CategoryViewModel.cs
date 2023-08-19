using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Application.Features.CategoryFeatures.CategoryCreate
{
    public class CategoryViewModel
    {
        [Required]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Name length between 5 and 25 Letters")]
        public string? Name { get; set; }

    }
}
