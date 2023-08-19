using FoodOredering.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOredering.Domain.Entities
{
    //Soft delete
    public class Categories : BaseModel
    {
        public virtual ICollection<Menus>? Menus { get; set; }
    }
}
