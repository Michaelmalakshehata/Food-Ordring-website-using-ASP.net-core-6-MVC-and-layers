using Food_Restaurant.BL.ViewModels;
using Food_Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Restaurant.BL.IRepositories
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
