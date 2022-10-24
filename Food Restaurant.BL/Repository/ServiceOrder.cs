using Food_Restaurant.BL.IRepositories;
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
    public class ServiceOrder : IServiceOrder
    {
        private readonly IServiceCart serviceCart;
        private readonly EntityContext context;
        public ServiceOrder(IServiceCart serviceCart,EntityContext context)
        {
            this.serviceCart = serviceCart;
            this.context = context;
        }

        public async Task AddOrder(OrderViewModel order)
        {
            if(order!=null)
            {
                string orderdetails="";
                int i = 1;
                List<string> cartitem = await OrderDetailes(order.Username);
                if (cartitem != null)
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

        public async Task<List<Orders>?> AllOrders()
        {
          return   await context.Orders.ToListAsync();

        }

        public async Task<OrderViewModel?> CheckoutOrder(string Name)
        {
            if (Name != null)
            {
                string? userid = await context.Users.Where(u => u.UserName == Name).Select(u => u.Id).FirstOrDefaultAsync();
                if (userid != null)
                {
                    List<string>? cartitem = await OrderDetailes(Name);
                    if (cartitem != null)
                    {
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
                }
            }
            return null;
        }

        public async Task DeleteOrder(int id)
        {
          var orders=  await context.Orders.Where(o => o.Id == id).FirstOrDefaultAsync();
            if(orders != null)
            {
                 context.Remove(orders);
                await context.SaveChangesAsync();
            }

        }

        public async Task<Orders?> Order(string Name)
        {
            if (Name != null)
            {
                Orders? orders = await context.Orders.Where(o => o.Username == Name).FirstOrDefaultAsync();
                return orders;
            }
            return null;
        }

        public async Task<List<string>?> OrderDetailes(string Name)
        {
            if (Name != null)
            {
                string? userid = await context.Users.Where(u => u.UserName == Name).Select(u => u.Id).FirstOrDefaultAsync();
                if (userid != null)
                {
                    List<Cart> carts = await context.Carts.Where(o => o.UserId == userid).ToListAsync();
                    if (carts != null)
                    {
                        List<string> cartitem = new List<string>();

                        foreach (var item in carts)
                        {
                            cartitem.Add(item.Quantity + " * " + item.Ordername + " :=  $" + item.TotalPrice);
                        }
                        return cartitem;
                    }
                }
            }
            return null;
        }

        public Task<double> totalprice(string name)
        {
            return serviceCart.totalprice(name);
        }
    }
}
