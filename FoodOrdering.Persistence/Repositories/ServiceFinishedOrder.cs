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
    public class ServiceFinishedOrder : IServiceFinishedOrder
    {
        private readonly IServiceMenu serviceMenu;
        private readonly EntityContext context;
        public ServiceFinishedOrder(IServiceMenu serviceMenu, EntityContext context)
        {
            this.context = context;
            this.serviceMenu = serviceMenu;
        }
        public async Task AddFinishedOrder(int id)
        {
            try
            {
                if (id != 0)
                {
                    Orders? orders = await context.Orders.FindAsync(id);
                    if (orders is not null)
                    {
                        FinishedOrders finishedOrders = new FinishedOrders()
                        {
                            Username = orders.Username,
                            AddressDetailes = orders.AddressDetailes,
                            Email = orders.Email,
                            Phonenumber = orders.Phonenumber,
                            TotalPrice = orders.TotalPrice,
                            OrderDetails = orders.OrderDetails
                        };
                        context.FinishedOrders.Add(finishedOrders);
                        context.Orders.Remove(orders);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<FinishedOrders>> GetAllFinishedOrder()
        {
            try
            {
                List<FinishedOrders> orders = await context.FinishedOrders.ToListAsync();
                return orders;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<FinishedOrders>?> GetAllFinishedOrderByName(string name)
        {
            try
            {
                if (name is null)
                {
                    return new List<FinishedOrders>();
                }
                List<FinishedOrders> orders = await context.FinishedOrders.Where(o => o.Username == name).ToListAsync();
                return orders;
            }
            catch
            {
                throw;
            }
        }
    }
}
