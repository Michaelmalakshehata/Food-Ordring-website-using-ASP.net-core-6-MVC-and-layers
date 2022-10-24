using Food_Restaurant.BL.ViewModels;
using Food_Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Restaurant.BL.IRepositories
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
