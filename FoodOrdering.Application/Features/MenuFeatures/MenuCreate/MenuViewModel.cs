using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Application.Features.MenuFeatures.MenuCreate
{
    public class MenuViewModel
    {

        [Required]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Name length between 5 and 25 Letters")]
        public string? Name { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        [Required]
        public IFormFile? File { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
