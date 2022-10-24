using Food_Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Food_Restaurant.BL.Repositories
{
    public interface IGenericRepository<T> where T : BaseModel
    {
        Task<List<T>> Getall();
        Task<List<T>> GetallSoftDeleted();
        Task<T> GetById(int id);
        Task<T> add<T>(T data);
        Task update<T>(T data);
        Task<int> delete(int id);
        Task<int> Restordeleted(int id);
        Task<List<T>> Find(Expression<Func<T, bool>> match);
    
    }
}
