using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Restaurant.DAL.Models
{
    public abstract class BaseModel
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        [Required]
        public string? Name { get; set; }
        public DateTime Date { get; set; }=DateTime.Now;
    }
}
