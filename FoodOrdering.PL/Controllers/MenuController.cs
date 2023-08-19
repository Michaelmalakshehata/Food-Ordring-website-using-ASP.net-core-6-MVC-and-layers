using FoodOrdering.Application.Common;
using FoodOrdering.Application.Features.MenuFeatures.MenuCreate;
using FoodOrdering.Application.Features.MenuFeatures.MenuUpdate;
using FoodOrdering.Application.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.PL.Controllers
{
    [Authorize(Roles = Roles.adminrole)]
    public class MenuController : Controller
    {
        private readonly IServiceMenu servicemenu;
        private readonly IServiceCategory servicecategory;
        public MenuController(IServiceMenu servicemenu, IServiceCategory servicecategory)
        {
            this.servicemenu = servicemenu;
            this.servicecategory = servicecategory;
        }
        #region all Menus
        public async Task<IActionResult> Index()
        {

            var menulist = await servicemenu.GetallMenus();
            if (menulist != null)
            {
                return View(menulist);
            }
            return View();
        }

        #endregion


        #region all deleted menus
        [HttpGet]
        public async Task<IActionResult> Deletedmenu()
        {
            var menulistdeleted = await servicemenu.GetalldeletedMenus();
            return View(menulistdeleted);
        }

        #endregion

        #region all create Menus
        [HttpGet]
        public async Task<IActionResult> CreateMenus()
        {
            var list = await servicecategory.GetallCategories();
            ViewBag.list = list;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMenus(MenuViewModel menuViewModel)
        {

            if (ModelState.IsValid)
            {

                var result = await servicemenu.add(menuViewModel);
                return RedirectToAction("Index");
            }
            var list = await servicecategory.GetallCategories();
            ViewBag.list = list;
            return View(menuViewModel);
        }

        #endregion

        #region all updated Menu

        [HttpGet]
        public async Task<IActionResult> UpdateMenu(int Id)
        {
            var result = await servicemenu.GetByid(Id);
            var list = await servicecategory.GetallCategories();
            ViewBag.list = list;
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateMenu(MenuUpdateViewModel menuUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await servicemenu.Update(menuUpdateViewModel);
                    return RedirectToAction("Index");
                }
                catch (ArgumentNullException)
                {
                    ModelState.AddModelError("Name", "Updated Name Already exist in Menus or Restor it from Deleted Menus");
                }

            }
            var list = await servicecategory.GetallCategories();
            ViewBag.list = list;
            return View(menuUpdateViewModel);
        }

        #endregion

        #region  deleted Menu

        [HttpGet]
        public async Task<IActionResult> DeleteMenu(int Id)
        {
            await servicemenu.Delete(Id);
            return RedirectToAction("Index");
        }

        #endregion


        #region restore Menu

        [HttpGet]
        public async Task<IActionResult> RestoreMenu(int Id)
        {
            await servicemenu.RestoreMenu(Id);
            return RedirectToAction("Deletedmenu");
        }

        #endregion
    }
}
