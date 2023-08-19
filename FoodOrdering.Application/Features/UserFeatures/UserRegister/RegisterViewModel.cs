using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Application.Features.UserFeatures.UserRegister
{
    public class RegisterViewModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "Length more than 2 Lettres")]
        public string? Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter valid email")]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm Password Not match")]
        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword { get; set; }
        [Required]
        [MinLength(4, ErrorMessage = "Length more than 3 Lettres")]
        public string? Address { get; set; }
    }
}
