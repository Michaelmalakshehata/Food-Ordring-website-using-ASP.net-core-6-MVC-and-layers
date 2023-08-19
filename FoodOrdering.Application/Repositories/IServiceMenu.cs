using FoodOrdering.Application.Features.MenuFeatures.MenuCreate;
using FoodOrdering.Application.Features.MenuFeatures.MenuUpdate;
using FoodOredering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Application.Repositories
{
    public interface IServiceMenu
    {
        Task<List<Menus>?> GetallMenus();
        Task<List<Menus>?> GetalldeletedMenus();
        Task<Menus?> add(MenuViewModel menuViewModel);
        Task<int> Delete(int id);
        Task<int> RestoreMenu(int id);
        Task<MenuUpdateViewModel?> GetByid(int id);
        Task Update(MenuUpdateViewModel menuUpdateViewModel);
        Task<List<Menus>?> GetMenuByCategory(int id);
    }
}
