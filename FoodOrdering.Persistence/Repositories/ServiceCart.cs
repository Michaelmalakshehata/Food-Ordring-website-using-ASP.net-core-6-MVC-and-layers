using FoodOrdering.Application.Features.CartFeatures.CartCreate;
using FoodOrdering.Application.Features.CartFeatures.CartUpdate;
using FoodOrdering.Application.Repositories;
using FoodOrdering.Persistence.Context;
using FoodOredering.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Persistence.Repositories
{
    public class ServiceCart : IServiceCart
    {
        private readonly EntityContext context;
        public ServiceCart(EntityContext context)
        {
            this.context = context;
        }

        public async Task<string?> AddToCart(CartViewModel cartViewModel, string Name)
        {
            try
            {
                if (cartViewModel is null)
                {
                   return string.Empty;
                }
                Cart? existincart = await context.Carts.Where(o => o.Ordername == cartViewModel.Ordername).FirstOrDefaultAsync();
                if (existincart is not null)
                {
                    existincart.Quantity = cartViewModel.Quantity + existincart.Quantity;
                    context.Update(existincart);
                    await context.SaveChangesAsync();
                    return "added";
                }
                else
                {
                    string? userid = await context.Users.Where(u => u.UserName == Name).Select(u => u.Id).FirstOrDefaultAsync();
                    Cart cart = new Cart()
                    {
                        Ordername = cartViewModel.Ordername,
                        Price = cartViewModel.Price,
                        imgpath = cartViewModel.imgpath,
                        Quantity = cartViewModel.Quantity,
                        UserId = userid
                    };
                    var result = await context.AddAsync(cart);
                    await context.SaveChangesAsync();
                    return "added";
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<string?> DeleteAllCartItems(string name)
        {
            try
            {
                if (name is null)
                {
                    return string.Empty;
                }
                var id = context.Users.Where(o => o.UserName == name).Select(o => o.Id).FirstOrDefault();
                if (id is null)
                {
                    return string.Empty;
                }
                var listcart = context.Carts.Where(o => o.UserId == id).ToList();
                if (listcart is null)
                {
                    return string.Empty;
                }
                context.RemoveRange(listcart);
                await context.SaveChangesAsync();
                return "deletedAll";
            }
            catch
            {
                throw;
            }
        }

        public async Task<string?> DeleteCartItem(int id)
        {
            try
            {
                if (id == 0)
                {
                    return string.Empty;
                }
                var result = context.Carts.Where(o => o.Id == id).FirstOrDefault();
                if (result is null)
                {
                    return string.Empty;
                }
                context.Remove(result);
                await context.SaveChangesAsync();
                return "deleted";
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<CartUpdateViewModel>?> GetAllUserCart(string Name)
        {
            try
            {
                if (Name is null)
                {
                    return new List<CartUpdateViewModel>();
                }
                string? userid = await context.Users.Where(u => u.UserName == Name).Select(u => u.Id).FirstOrDefaultAsync();
                if (userid is null)
                {
                    return new List<CartUpdateViewModel>();
                }
                List<Cart> carts = await context.Carts.Where(o => o.UserId == userid).ToListAsync();
                if (carts is null)
                {
                    return new List<CartUpdateViewModel>();
                }
                List<CartUpdateViewModel> cart = new List<CartUpdateViewModel>();
                foreach (var item in carts)
                {
                    if (await context.Menus.Where(o => o.IsDeleted == false).Where(o => o.Name == item.Ordername).FirstOrDefaultAsync() == null)
                    {
                        await DeleteCartItem(item.Id);
                        continue;
                    }
                    else
                    {
                        cart.Add(new CartUpdateViewModel
                        {
                            Id = item.Id,
                            UserId = userid,
                            Quantity = item.Quantity,
                            imgpath = item.imgpath,
                            Price = item.Price,
                            Ordername = item.Ordername,
                            SubTotalPrice = item.TotalPrice
                        });
                    }

                }
                return cart;
            }
            catch
            {
                throw;
            }
        }

        public async Task<double> totalprice(string name)
        {
            try
            {
                if (name is null)
                {
                    return 0;
                }
                string? userid = await context.Users.Where(u => u.UserName == name).Select(u => u.Id).FirstOrDefaultAsync();
                if (userid is null)
                {
                    return 0;
                }
                List<Cart> carts = await context.Carts.Where(o => o.UserId == userid).ToListAsync();
                if (carts is null)
                {
                    return 0;
                }
                double totalprice = 0;
                foreach (var c in carts)
                {
                    totalprice += c.TotalPrice;
                }
                return totalprice;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string?> UpdateCartItem(CartUpdateViewModel cartUpdateViewModel)
        {
            try
            {
                if (cartUpdateViewModel is null)
                {
                    return string.Empty;
                }
                Cart cart = new Cart()
                {
                    Id = cartUpdateViewModel.Id,
                    Ordername = cartUpdateViewModel.Ordername,
                    imgpath = cartUpdateViewModel.imgpath,
                    Price = cartUpdateViewModel.Price,
                    Quantity = cartUpdateViewModel.Quantity,
                    UserId = cartUpdateViewModel.UserId
                };
                var result = context.Update(cart);
                await context.SaveChangesAsync();
                if (result is null)
                {
                    return string.Empty;
                }
                return "updated";
            }
            catch
            {
                throw;
            }
        }
    }
}
