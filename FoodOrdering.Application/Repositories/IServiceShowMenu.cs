using FoodOrdering.Application.Features.CartFeatures.CartCreate;
using FoodOrdering.Application.Features.OrderFeatures.OrderSearch;
using FoodOredering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Application.Repositories
{
    public interface IServiceShowMenu
    {
        Task<List<CartViewModel>?> GetlastMenus();
        Task<CartViewModel?> GetByid(int id);
        Task<List<CartViewModel>?> GetMenuByCategory(int id);
        Task<List<Menus>?> Takefristthreemenu();
        Task<List<CartViewModel>?> SearchMenu(SearchViewModel searchViewModel);
    }
}
