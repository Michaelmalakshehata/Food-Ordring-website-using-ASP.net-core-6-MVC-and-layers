using FoodOrdering.Application.Common;
using FoodOrdering.Application.Features.CategoryFeatures.CategoryCreate;
using FoodOrdering.Application.Features.CategoryFeatures.CategoryUpdate;
using FoodOrdering.Application.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.PL.Controllers
{
    [Authorize(Roles = Roles.adminrole)]
    public class CategoryController : Controller
    {
        private readonly IServiceCategory serviceCategory;
        public CategoryController(IServiceCategory serviceCategory)
        {
            this.serviceCategory = serviceCategory;
        }
        #region all categories
        public async Task<IActionResult> Index()
        {
            var Categorylist = await serviceCategory.GetallCategories();
            if (Categorylist != null)
            {
                return View(Categorylist);
            }
            return View();
        }

        #endregion


        #region all deleted categories
        [HttpGet]
        public async Task<IActionResult> DeletedCategory()
        {
            var Categorylistdeleted = await serviceCategory.GetalldeletedCategories();
            return View(Categorylistdeleted);
        }

        #endregion

        #region all create categories
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(CategoryViewModel categoryViewModel)
        {

            if (ModelState.IsValid)
            {

                var result = await serviceCategory.add(categoryViewModel);
                return RedirectToAction("Index");
            }

            return View(categoryViewModel);
        }

        #endregion

        #region all updated categories

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int Id)
        {
            var result = await serviceCategory.GetByid(Id);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateViewModel categoryUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                await serviceCategory.Update(categoryUpdateViewModel);
                return RedirectToAction("Index");
            }
            return View(categoryUpdateViewModel);
        }

        #endregion

        #region  deleted categories

        [HttpGet]
        public async Task<IActionResult> DeleteCategory(int Id)
        {
            await serviceCategory.Delete(Id);
            return RedirectToAction("Index");
        }

        #endregion


        #region restore categories

        [HttpGet]
        public async Task<IActionResult> RestoreCategory(int Id)
        {
            await serviceCategory.RestoreCategory(Id);
            return RedirectToAction("DeletedCategory");
        }

        #endregion
    }
}
