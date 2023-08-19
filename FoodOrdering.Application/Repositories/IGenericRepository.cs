using FoodOredering.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Application.Repositories
{
    public interface IGenericRepository<T> where T : BaseModel
    {
        Task<List<T>> Getall();
        Task<List<T>> GetallSoftDeleted();
        Task<T> GetById(int id);
        Task<T> add(T data);
        Task update(T data);
        Task<int> delete(int id);
        Task<int> Restordeleted(int id);
        Task<List<T>> Find(Expression<Func<T, bool>> match);

    }
}
