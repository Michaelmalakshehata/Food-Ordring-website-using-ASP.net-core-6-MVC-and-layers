using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Restaurant.DAL.Models
{
    //Soft delete
    public class Categories:BaseModel
    {
        public virtual ICollection<Menus>? Menus { get; set; }
    }
}
