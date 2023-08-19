using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Application.Features.OrderFeatures.OrderSearch
{
    public class SearchViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name length between 2 and 30 Letters")]
        public string? name { get; set; }
    }
}
