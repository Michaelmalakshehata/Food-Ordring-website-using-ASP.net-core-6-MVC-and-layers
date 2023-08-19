using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Application.Features.CategoryFeatures.CategoryUpdate
{
    public class CategoryUpdateViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Name length between 5 and 25 Letters")]
        public string? Name { get; set; }
    }
}
