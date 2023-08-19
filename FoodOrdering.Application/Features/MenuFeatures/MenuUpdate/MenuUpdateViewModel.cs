using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Application.Features.MenuFeatures.MenuUpdate
{
    public class MenuUpdateViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Name length between 5 and 25 Letters")]
        public string? Name { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        public string? imgpath { get; set; }
        public IFormFile? File { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
