using FoodOrdering.Application.Repositories;
using FoodOrdering.Persistence.Context;
using FoodOredering.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Persistence.Repositories
{
    public class GenericService<T> : IGenericRepository<T> where T : BaseModel
    {
        protected EntityContext _Context;
        protected DbSet<T> _Entities;
        public GenericService(EntityContext context)
        {
            _Context = context;
            _Entities = _Context.Set<T>();
        }
        async Task<List<T>> IGenericRepository<T>.Getall()
        {

            try
            {
                return await _Entities.Where(w => w.IsDeleted == false).ToListAsync();

            }
            catch
            {
                return new List<T>();
            }

        }


        async Task<T> IGenericRepository<T> .GetById(int id)
        {
            if (id == 0 || await _Entities.FindAsync(id) is null)
            {
                return default(T);
            }

            else
            {
                return await _Entities.FindAsync(id);
            }
            
        }


        async Task<T> IGenericRepository<T>.add(T data)
        {
            if (data is not null)
            {
                await _Context.AddAsync(data);
                await _Context.SaveChangesAsync();
                return data;
            }
            return default(T);

        }

        async Task IGenericRepository<T> .update(T data)
        {
            if (data is not null)
            {
                _Context.Update(data);

                await _Context.SaveChangesAsync();
            }           
        }


        async Task<int> IGenericRepository<T>.delete(int id)
        {
            if (id == 0 || await _Entities.FindAsync(id) is null)
            {
               return 0;
            }
            T obj = await _Entities.FindAsync(id);
            obj.IsDeleted = true;
            _Context.Update(obj);
            await _Context.SaveChangesAsync();
            return id;
        }

        public async Task<List<T>> Find(Expression<Func<T, bool>> match)
        {
            if (match is null)
            {
                return new List<T>();
            }
            return await _Entities.Where(match).Where(b => b.IsDeleted == false).ToListAsync();
        }

        public async Task<List<T>> GetallSoftDeleted()
        {
            try
            {
                return await _Entities.Where(w => w.IsDeleted == true).ToListAsync();

            }
            catch
            {
                return new List<T>();
            }
        }

        public async Task<int> Restordeleted(int id)
        {
            if (id == 0 || await _Entities.FindAsync(id) is null)
            {
                return 0;
            }
            T obj = await _Entities.FindAsync(id);
            obj.IsDeleted = false;
            _Context.Update(obj);
            await _Context.SaveChangesAsync();
            return id;
        }
    }
}
