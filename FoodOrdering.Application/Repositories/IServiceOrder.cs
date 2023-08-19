using FoodOrdering.Application.Features.OrderFeatures.OrderCreate;
using FoodOredering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Application.Repositories
{
    public interface IServiceOrder
    {
        Task<OrderViewModel?> CheckoutOrder(string name);
        Task<double> totalprice(string name);
        Task AddOrder(OrderViewModel order);
        Task<Orders?> Order(string name);
        Task<List<Orders>?> AllOrders();
        Task<List<string>?> OrderDetailes(string name);
        Task DeleteOrder(int id);
    }
}
