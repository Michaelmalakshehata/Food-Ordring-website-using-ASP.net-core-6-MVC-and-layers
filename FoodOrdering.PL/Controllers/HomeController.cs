using FoodOrdering.Application.Common.Pagination;
using FoodOrdering.Application.Repositories;
using FoodOredering.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FoodOrdering.PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceShowMenu serviceshowmenu;
        private readonly IServiceCategory serviceCategory;

        public HomeController(IServiceShowMenu serviceshowmenu, IServiceCategory _serviceCategory)
        {
            this.serviceCategory = _serviceCategory;
            this.serviceshowmenu = serviceshowmenu;
        }

        public async Task<IActionResult> Index(int pg=1)
        {
            var listcategory = await serviceCategory.GetallCategories();
            var data = Pagination<Categories>.GetPaginationData(pg, listcategory);
            this.ViewBag.Pager = data.Item2;
            ViewBag.list = data.Item1;
            var listmenus = await serviceshowmenu.Takefristthreemenu();
            return View(listmenus);
        }
        [HttpGet]
        public IActionResult About()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendMail()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }
    }
}