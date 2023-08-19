using FoodOredering.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOredering.Domain.Entities
{
    //Soft delete
    public class Menus : BaseModel
    {
        [Required]
        public double Price { get; set; }
        [Required]
        public string? imgpath { get; set; }
        public int CategoryId { get; set; }
        public virtual Categories? Categories { get; set; }
    }
}
