using FoodOrdering.Application.Features.OrderFeatures.OrderSearch;
using FoodOrdering.Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.PL.Controllers
{
    public class ShowMenuController : Controller
    {

        private readonly IServiceShowMenu serviceshowmenu;
        private readonly IServiceCategory servicecategory;
        public ShowMenuController(IServiceShowMenu _serviceshowmenu, IServiceCategory servicecategory)
        {
            this.serviceshowmenu = _serviceshowmenu;
            this.servicecategory = servicecategory;
        }

        #region show all menu with category name
        public async Task<IActionResult> Index()
        {

            var listmenus = await serviceshowmenu.GetlastMenus();
            if (listmenus != null)
            {
                ViewBag.list = listmenus;
            }
            return View();
        }

        #endregion

        #region show menu in detailes

        [HttpGet]
        public async Task<IActionResult> ShowDetailes(int Id)
        {
            var menu = await serviceshowmenu.GetByid(Id);

            return View(menu);
        }
        #endregion

        #region show all menu in his category 

        [HttpGet]
        public async Task<IActionResult> ShowCategoryMenu(int Id)
        {
            var menulist = await serviceshowmenu.GetMenuByCategory(Id);
            if (menulist != null)
            {
                ViewBag.list = menulist;
            }
            return View();
        }
        #endregion

        #region Search view
        [HttpGet]
        public IActionResult SearchMenu()
        {
            return View();
        }

        #endregion

        #region Search Result
        [HttpGet]
        public async Task<IActionResult> SearchResult(SearchViewModel searchViewModel)
        {
            ViewBag.listmenu = await serviceshowmenu.SearchMenu(searchViewModel);
            return View();
        }
        #endregion
    }
}
