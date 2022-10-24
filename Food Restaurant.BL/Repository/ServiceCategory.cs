using Food_Restaurant.BL.IRepositories;
using Food_Restaurant.BL.Repositories;
using Food_Restaurant.BL.ViewModels;
using Food_Restaurant.DAL;
using Food_Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Restaurant.BL.Repository
{ 
    public class ServiceCategory : IServiceCategory
    {
        private readonly EntityContext context;
        private readonly IGenericRepository<Categories> genericRepository;
        public ServiceCategory(IGenericRepository<Categories> genericRepository, EntityContext context)
        {
            this.genericRepository = genericRepository;
            this.context = context;
        }

        public async Task<Categories?> add(CategoryViewModel categoryViewModel)
        {
            if(categoryViewModel !=null)
            {
                Categories categories=new Categories()
                {
                    Name=categoryViewModel.Name
                }; 
                var obj=await genericRepository.add(categories);
                return obj;
            }
            return null;
        }

        public async Task<int> Delete(int id)
        {
            if(id > 0)
            {
               int result=await genericRepository.delete(id);
                return result;
            }
            return 0;
        }

        public async Task<List<Categories>?> GetallCategories()
        {
            List<Categories> list =await genericRepository.Getall();
            if(list != null)
            {
                return list;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Categories>?> GetalldeletedCategories()
        {
            List<Categories> list = await genericRepository.GetallSoftDeleted();
            if (list != null)
            {
                return list;
            }
            else
            {
                return null;
            }
        }

        public async Task<CategoryUpdateViewModel?> GetByid(int id)
        {
            if (id > 0)
            {
                var result = await genericRepository.GetById(id);
                if (result != null)
                {
                    CategoryUpdateViewModel categoryUpdateViewModel = new CategoryUpdateViewModel()
                    {
                        Name = result.Name,
                        Id = result.Id
                    };
                    return categoryUpdateViewModel;
                }

            }
            return null;
        }

        public async Task<int> RestoreCategory(int id)
        {
            if (id > 0)
            {
                int result = await genericRepository.Restordeleted(id);
                return result;
            }
            return 0;
        }

        public async Task Update(CategoryUpdateViewModel categoryUpdateViewModel)
        {
            if(categoryUpdateViewModel !=null)
            {

                Categories categories = new Categories()
                {
                    Id = categoryUpdateViewModel.Id,
                    Name = categoryUpdateViewModel.Name

                };
              await genericRepository.update(categories);
            }
        }
    }
}
