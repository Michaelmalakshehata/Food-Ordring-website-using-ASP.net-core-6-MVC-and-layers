using Food_Restaurant.BL.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Restaurant.BL.ViewModels
{
    public class ProfileViewModel
    {
        [Required]
        [UniqueUsername]
        [MinLength(3, ErrorMessage = "Length more than 2 Lettres")]
        public string? Username { get; set; }
        [Required]
        [UniqueEmail]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter valid email")]
        public string? Email { get; set; }
        [Required]
        [MinLength(4, ErrorMessage = "Length more than 3 Lettres")]
        public string? Address { get; set; }
    }
}
