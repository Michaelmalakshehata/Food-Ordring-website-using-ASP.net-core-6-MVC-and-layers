using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Application.Features.CartFeatures.CartUpdate
{
    public class CartUpdateViewModel
    {
        [Key]
        public int Id { get; set; }
        public string? Ordername { get; set; }
        public string? imgpath { get; set; }

        public double Price { get; set; }
        [Required]
        public int Quantity { get; set; }

        public string? UserId { get; set; }
        public double SubTotalPrice { get; set; }

    }
}
