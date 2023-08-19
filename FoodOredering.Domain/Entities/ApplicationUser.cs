using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOredering.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string? Address { set; get; }

        public virtual ICollection<Orders>? orders { get; set; }
        public virtual ICollection<Cart>? Carts { get; set; }


    }
}
