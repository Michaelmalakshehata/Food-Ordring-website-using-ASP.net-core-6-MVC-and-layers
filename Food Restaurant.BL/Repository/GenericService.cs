using Food_Restaurant.BL.Repositories;
using Food_Restaurant.DAL;
using Food_Restaurant.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Food_Restaurant.BL.Repository
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
                return await _Entities.Where(w=>w.IsDeleted==false).ToListAsync();

            }
            catch
            {
                throw new ArgumentNullException();
            }

        }


        async Task<T> IGenericRepository<T>.GetById(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException();
            }

            if (await _Entities.FindAsync(id) == null)
            {
                throw new ArgumentNullException();

            }
            return await _Entities.FindAsync(id);
        }


        async Task<T> IGenericRepository<T>.add<T>(T data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            await _Context.AddAsync(data);
            await _Context.SaveChangesAsync();
            return data;

        }

        async Task IGenericRepository<T>.update<T>(T data)
        {
            if (data== null)
            {
                throw new ArgumentNullException();

            }
           
            _Context.Update(data);

             _Context.SaveChanges();
        }


        async Task<int> IGenericRepository<T>.delete(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException();

            }
            if (await _Entities.FindAsync(id) == null)
            {
                throw new ArgumentNullException();

            }
            T obj = await _Entities.FindAsync(id);
            obj.IsDeleted = true;
            _Context.Update(obj);

            await _Context.SaveChangesAsync();
            return id;
        }

        public async Task<List<T>> Find(Expression<Func<T, bool>> match)
        {
            if (match == null)
            {
                throw new ArgumentNullException();
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
                throw new ArgumentNullException();
            }
        }

        public async Task<int> Restordeleted(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException();

            }
            if (await _Entities.FindAsync(id) == null)
            {
                throw new ArgumentNullException();

            }
            T obj = await _Entities.FindAsync(id);
            obj.IsDeleted = false;
            _Context.Update(obj);

            await _Context.SaveChangesAsync();
            return id;
        }
    }
}
