using Food_Restaurant.BL.ViewModels;
using Food_Restaurant.DAL.Constant;
using Food_Restaurant.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Food_Restaurant.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        public ProfileController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            string name = User.Identity.Name;
            ApplicationUser user = await userManager.FindByNameAsync(name);
            if(user != null)
            {
                return View(user);
            }

            return View();
        }
    }
}
