using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOredering.Domain.Entities
{
    //Hard Delete
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Ordername { get; set; }
        [Required]
        public string? imgpath { get; set; }

        [Required]
        public double Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        public double TotalPrice
        {
            get
            {
                return Quantity * Price;
            }
        }

        public string? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
