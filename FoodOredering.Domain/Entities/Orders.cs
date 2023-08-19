using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOredering.Domain.Entities
{
    //Hard delete
    public class Orders
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? OrderDetails { get; set; }
        [Required]
        public double TotalPrice { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        [Required]
        public string? AddressDetailes { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        public string? Phonenumber { get; set; }
        public string? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }


    }
}
