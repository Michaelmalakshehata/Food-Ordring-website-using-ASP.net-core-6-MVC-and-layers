using FoodOredering.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.PL.Controllers
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
            if (user != null)
            {
                return View(user);
            }

            return View();
        }
    }
}
