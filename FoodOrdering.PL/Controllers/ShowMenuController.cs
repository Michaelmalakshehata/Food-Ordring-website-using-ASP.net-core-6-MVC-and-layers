using FoodOrdering.Application.Common.Pagination;
using FoodOrdering.Application.Features.CartFeatures.CartCreate;
using FoodOrdering.Application.Features.OrderFeatures.OrderSearch;
using FoodOrdering.Application.Repositories;
using FoodOredering.Domain.Entities;
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
        public async Task<IActionResult> Index(int pg=1)
        {

            var listmenus = await serviceshowmenu.GetlastMenus();
            var data = Pagination<CartViewModel>.GetPaginationData(pg, listmenus);
            this.ViewBag.Pager = data.Item2;
            if (data.Item1 is not null)
            {
                ViewBag.list = data.Item1;
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
        public async Task<IActionResult> ShowCategoryMenu(int Id,int pg=1)
        {
            var menulist = await serviceshowmenu.GetMenuByCategory(Id);
            var data = Pagination<CartViewModel>.GetPaginationData(pg, menulist);
            this.ViewBag.pager = data.Item2;
            if (data.Item1 is not null)
            {
                ViewBag.list = data.Item1;
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
        public async Task<IActionResult> SearchResult(SearchViewModel searchViewModel,int pg=1)
        {
            var listmenu = await serviceshowmenu.SearchMenu(searchViewModel);
            var data = Pagination<CartViewModel>.GetPaginationData(pg, listmenu);
            this.ViewBag.page = data.Item2;
            ViewBag.listmenu = data.Item1;
            return View();
        }
        #endregion
    }
}
