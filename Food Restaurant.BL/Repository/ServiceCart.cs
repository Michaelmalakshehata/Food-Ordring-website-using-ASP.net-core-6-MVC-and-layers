using Food_Restaurant.BL.IRepositories;
using Food_Restaurant.BL.Repositories;
using Food_Restaurant.BL.ViewModels;
using Food_Restaurant.DAL;
using Food_Restaurant.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Restaurant.BL.Repository
{
    public class ServiceCart:IServiceCart
    {
        private readonly EntityContext context;
        public ServiceCart( EntityContext context)
        {
            this.context = context;
        }

        public async Task<string?> AddToCart(CartViewModel cartViewModel,string Name)
        {
            if(cartViewModel != null)
            {
                string? userid= await context.Users.Where(u => u.UserName == Name).Select(u => u.Id).FirstOrDefaultAsync();
                Cart cart = new Cart()
                {
                    Ordername=cartViewModel.Ordername,
                    Price=cartViewModel.Price,
                    imgpath=cartViewModel.imgpath,
                    Quantity=cartViewModel.Quantity,
                    UserId=userid  
                };
               var result= await context.AddAsync(cart);
                await context.SaveChangesAsync();
                if (result!=null)
                {
                    return "added";
                }
            }
            return null;
        }

        public async Task<string?> DeleteAllCartItems(string name)
        {
            if(name!=null)
            {
                var id = context.Users.Where(o => o.UserName == name).Select(o => o.Id).FirstOrDefault();
                if(id!=null)
                {
                    var listcart = context.Carts.Where(o => o.UserId == id).ToList();
                    if(listcart!=null)
                    {
                        context.RemoveRange(listcart);
                        await context.SaveChangesAsync();
                        return "deletedAll";
                    }
                }
            }
            return null;
        }

        public async Task<string?> DeleteCartItem(int id)
        {
            if(id!=0)
            {
                var result =  context.Carts.Where(o=>o.Id==id).FirstOrDefault();
                if(result!=null)
                {
                    context.Remove(result);
                    await context.SaveChangesAsync();
                    return "deleted";
                }
            }
            return null;
        }

        public async Task<List<CartUpdateViewModel>?> GetAllUserCart(string Name)
        {
            if(Name != null)
            {
                string? userid = await context.Users.Where(u => u.UserName == Name).Select(u => u.Id).FirstOrDefaultAsync();
                if(userid != null)
                {
                    List<Cart> carts=await context.Carts.Where(o=>o.UserId==userid).ToListAsync();
                    if (carts != null)
                    {
                        List<CartUpdateViewModel> cart = new List<CartUpdateViewModel>();
                       
                        foreach (var item in carts)
                        {
                            cart.Add(new CartUpdateViewModel
                            {
                                Id=item.Id,
                                UserId=userid,
                                Quantity=item.Quantity,
                                imgpath = item.imgpath,
                                Price = item.Price,
                                Ordername = item.Ordername,
                                SubTotalPrice=item.TotalPrice
                            });
                        }
                        return cart;
                    }
                }
            }
            return null;
        }

        public async Task<double> totalprice(string name)
        {
            if (name != null)
            {
                string? userid = await context.Users.Where(u => u.UserName == name).Select(u => u.Id).FirstOrDefaultAsync();
                if (userid != null)
                {
                    List<Cart> carts = await context.Carts.Where(o => o.UserId == userid).ToListAsync();
                    if (carts != null)
                    {
                        double totalprice = 0;
                        foreach (var c in carts)
                        {
                            totalprice += c.TotalPrice;
                        }
                        return totalprice;
                    }
                }
            }
            return 0;
        }

        public async Task<string?> UpdateCartItem(CartUpdateViewModel cartUpdateViewModel)
        {
            if(cartUpdateViewModel != null)
            {
                Cart cart = new Cart()
                {
                    Id=cartUpdateViewModel.Id,
                    Ordername=cartUpdateViewModel.Ordername,
                    imgpath=cartUpdateViewModel.imgpath,
                    Price=cartUpdateViewModel.Price,
                    Quantity=cartUpdateViewModel.Quantity,
                    UserId=cartUpdateViewModel.UserId
                };
                var result= context.Update(cart);
                await context.SaveChangesAsync();
                if (result != null)
                {
                    return "updated";
                }
            }
            return null;
        }
    }
}
