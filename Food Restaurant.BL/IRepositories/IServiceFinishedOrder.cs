using Food_Restaurant.BL.ViewModels;
using Food_Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Restaurant.BL.IRepositories
{
    public interface IServiceFinishedOrder
    {
        Task<List<FinishedOrders>> GetAllFinishedOrder();
        Task<List<FinishedOrders>?> GetAllFinishedOrderByName(string name);

        Task AddFinishedOrder(int id);    
    }
}
