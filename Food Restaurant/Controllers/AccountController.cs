using Food_Restaurant.BL.ViewModels;
using Food_Restaurant.DAL.Constant;
using Food_Restaurant.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Food_Restaurant.Controllers
{
   
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        #region Registration (Sign Up) 
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegisterViewModel modelvm)
        {

            try
            {

                if (ModelState.IsValid)
                {

                    var user = new ApplicationUser()
                    {

                        UserName = modelvm.Username,
                        Email = modelvm.Email,
                        Address=modelvm.Address
                    };

                    var result = await userManager.CreateAsync(user, modelvm.Password);

                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        await userManager.AddToRoleAsync(user, Roles.userrole);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }


                }

                return View(modelvm);

            }
            catch (Exception)
            {
                return View(modelvm);
            }
        }
        #endregion
        
        
        
        #region Login (Sign In)

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel modelvm)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByNameAsync(modelvm.Username);
                   if(user !=null)
                    {
                        bool found=await userManager.CheckPasswordAsync(user, modelvm.Password);
                        if(found)
                        {
                            await signInManager.SignInAsync(user, modelvm.RememberMe);
                            return RedirectToAction("Index", "Home");

                        }
                    }
                  ModelState.AddModelError("", "Invalid Email or Password");

                }

                return View(modelvm);

            }
            catch (Exception)
            {
                return View(modelvm);
            }
        }

        #endregion


        #region LogOff (Sign Out)

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }


        #endregion

    }
}
