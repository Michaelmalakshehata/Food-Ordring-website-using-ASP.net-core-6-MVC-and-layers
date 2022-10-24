using Food_Restaurant.BL.ViewModels;
using Food_Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Restaurant.BL.IRepositories
{
    public interface IServiceCart
    {
        Task<List<CartUpdateViewModel>?> GetAllUserCart(string Name);
        Task<string?> AddToCart(CartViewModel cartViewModel,string name);
        Task<double> totalprice(string name);
        Task<string?> UpdateCartItem(CartUpdateViewModel cartUpdateViewModel);
        Task<string?> DeleteCartItem(int id);
        Task<string?> DeleteAllCartItems(string name);
    }
}
