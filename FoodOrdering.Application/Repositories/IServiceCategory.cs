using FoodOrdering.Application.Features.CategoryFeatures.CategoryCreate;
using FoodOrdering.Application.Features.CategoryFeatures.CategoryUpdate;
using FoodOredering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Application.Repositories
{
    public interface IServiceCategory
    {
        Task<List<Categories>?> GetallCategories();
        Task<List<Categories>?> GetalldeletedCategories();
        Task<Categories?> add(CategoryViewModel categoryViewModel);
        Task<int> Delete(int id);
        Task<int> RestoreCategory(int id);
        Task<CategoryUpdateViewModel?> GetByid(int id);
        Task Update(CategoryUpdateViewModel categoryUpdateViewModel);
    }
}
