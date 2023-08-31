using FoodOrdering.Application.Features.CartFeatures.CartCreate;
using FoodOrdering.Application.Features.CartFeatures.CartUpdate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Application.Repositories
{
    public interface IServiceCart
    {
        Task<List<CartUpdateViewModel>?> GetAllUserCart(string Name);
        Task<string?> AddToCart(CartViewModel cartViewModel, string name);
        Task<double> totalprice(string name);
        Task<string?> UpdateCartItem(CartUpdateViewModel cartUpdateViewModel);
        Task<string?> DeleteCartItem(int id);
        Task<string?> DeleteAllCartItems(string name);

        Task<int> GetCartCount(string name);
        
    }
}
