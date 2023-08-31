using FoodOrdering.Application.Features.CategoryFeatures.CategoryCreate;
using FoodOrdering.Application.Features.CategoryFeatures.CategoryUpdate;
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
            try
            {
                if (categoryViewModel is null)
                {
                    return new Categories();
                }
                Categories categories = new Categories()
                {
                    Name = categoryViewModel.Name
                };
                var obj = await genericRepository.add(categories);
                return obj;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> Delete(int id)
        {
            try
            {
                var findProductsOfCategory = await context.Menus.Where(o => o.IsDeleted == false && o.CategoryId == id).AnyAsync();
                if (id > 0 && findProductsOfCategory ==false)
                {
                    int result = await genericRepository.delete(id);
                    return result;
                }
                return 0;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Categories>?> GetallCategories()
        {
            try
            {
                List<Categories> list = await genericRepository.Getall();
                if (list is null)
                {
                    return new List<Categories>();
                }
                else
                {
                    return list;
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Categories>?> GetalldeletedCategories()
        {
            try
            {
                List<Categories> list = await genericRepository.GetallSoftDeleted();
                if (list is null)
                {
                    return new List<Categories>();
                }
                else
                {
                    return list;
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<CategoryUpdateViewModel?> GetByid(int id)
        {
            try
            {
                if (id == 0)
                {
                    return new CategoryUpdateViewModel();
                }
                var result = await genericRepository.GetById(id);
                if (result is null)
                {
                    return new CategoryUpdateViewModel();
                }
                CategoryUpdateViewModel categoryUpdateViewModel = new CategoryUpdateViewModel()
                {
                    Name = result.Name,
                    Id = result.Id
                };
                return categoryUpdateViewModel;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> RestoreCategory(int id)
        {
            try
            {
                if (id > 0)
                {
                    int result = await genericRepository.Restordeleted(id);
                    return result;
                }
                return 0;
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(CategoryUpdateViewModel categoryUpdateViewModel)
        {
            try
            {
                if (categoryUpdateViewModel is not null)
                {
                    Categories categories = new Categories()
                    {
                        Id = categoryUpdateViewModel.Id,
                        Name = categoryUpdateViewModel.Name
                    };
                    await genericRepository.update(categories);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
