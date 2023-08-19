using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Application.Features.UserFeatures.UserProfile
{
    public class ProfileViewModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "Length more than 2 Lettres")]
        public string? Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter valid email")]
        public string? Email { get; set; }
        [Required]
        [MinLength(4, ErrorMessage = "Length more than 3 Lettres")]
        public string? Address { get; set; }
    }
}
