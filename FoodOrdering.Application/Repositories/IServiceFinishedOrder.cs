using FoodOredering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Application.Repositories
{
    public interface IServiceFinishedOrder
    {
        Task<List<FinishedOrders>> GetAllFinishedOrder();
        Task<List<FinishedOrders>?> GetAllFinishedOrderByName(string name);

        Task AddFinishedOrder(int id);
    }
}
