using FoodOrdering.Application.Features.OrderFeatures.OrderCreate;
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
    public class ServiceOrder : IServiceOrder
    {
        private readonly IServiceCart serviceCart;
        private readonly EntityContext context;
        public ServiceOrder(IServiceCart serviceCart, EntityContext context)
        {
            this.serviceCart = serviceCart;
            this.context = context;
        }

        public async Task AddOrder(OrderViewModel order)
        {
            try
            {
                if (order is not null)
                {
                    string orderdetails = "";
                    int i = 1;
                    List<string> cartitem = await OrderDetailes(order.Username);
                    if (cartitem is not null)
                    {
                        foreach (var item in cartitem)
                        {
                            orderdetails += "Order " + i++ + " := " + item.ToString() + " _  ";
                        }
                        double price = await totalprice(order.Username);
                        Orders orders = new Orders()
                        {
                            AddressDetailes = order.Address,
                            Email = order.Email,
                            Username = order.Username,
                            Phonenumber = order.PhoneNumber,
                            TotalPrice = price,
                            UserId = order.UserId,
                            OrderDetails = orderdetails
                        };
                        await context.AddAsync(orders);
                        await context.SaveChangesAsync();
                        await serviceCart.DeleteAllCartItems(order.Username);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Orders>?> AllOrders()
        {
            try
            {
                return await context.Orders.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<OrderViewModel?> CheckoutOrder(string Name)
        {
            try
            {
                if (Name is null)
                {
                    return new OrderViewModel();
                }
                string? userid = await context.Users.Where(u => u.UserName == Name).Select(u => u.Id).FirstOrDefaultAsync();
                if (userid is null)
                {
                    return new OrderViewModel();
                }
                List<string>? cartitem = await OrderDetailes(Name);
                if (cartitem is null)
                {
                    return new OrderViewModel();
                }
                var personalinfo = context.Users.Where(u => u.Id == userid).Select(u => new { u.Address, u.Email }).FirstOrDefault();
                OrderViewModel orderViewModel = new OrderViewModel()
                {
                    OrdersDetailes = cartitem,
                    UserId = userid,
                    Address = personalinfo.Address,
                    Email = personalinfo.Email,
                    Username = Name
                };
                return orderViewModel;
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteOrder(int id)
        {
            try
            {
                var orders = await context.Orders.Where(o => o.Id == id).FirstOrDefaultAsync();
                if (orders is not null)
                {
                    context.Remove(orders);
                    await context.SaveChangesAsync();
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<Orders?> Order(string Name)
        {
            try
            {
                if (Name is null)
                {
                    return new Orders();
                }
                Orders? orders = await context.Orders.Where(o => o.Username == Name).FirstOrDefaultAsync();
                return orders;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<string>?> OrderDetailes(string Name)
        {
            try
            {
                if (Name is null)
                {
                    return new List<string>();
                }
                string? userid = await context.Users.Where(u => u.UserName == Name).Select(u => u.Id).FirstOrDefaultAsync();
                if (userid is null)
                {
                    return new List<string>();
                }
                List<Cart> carts = await context.Carts.Where(o => o.UserId == userid).ToListAsync();
                if (carts is null)
                {
                    return new List<string>();
                }
                List<string> cartitem = new List<string>();

                foreach (var item in carts)
                {
                    cartitem.Add(item.Quantity + " * " + item.Ordername + " :=  $" + item.TotalPrice);
                }
                return cartitem;
            }
            catch
            {
                throw;
            }
        }

        public Task<double> totalprice(string name)
        {
            try
            {
                return serviceCart.totalprice(name);
            }
            catch
            {
                throw;
            }
        }
    }
}
