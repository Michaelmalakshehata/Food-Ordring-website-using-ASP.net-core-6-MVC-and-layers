using Food_Restaurant.BL.IRepositories;
using Food_Restaurant.DAL.Constant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Food_Restaurant.Controllers
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

        public async Task<IActionResult> Index()
        {
           var listcategory=await serviceCategory.GetallCategories();
            ViewBag.list= listcategory;
           var listmenus =await serviceshowmenu.Takefristthreemenu();
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
    }
}