using Food_Restaurant.DAL.Constant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Food_Restaurant.Controllers
{
    [Authorize(Roles = Roles.adminrole)]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
