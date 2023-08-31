using FoodOrdering.Application.Common.Pagination;
using FoodOrdering.Application.Features.CartFeatures.CartCreate;
using FoodOrdering.Application.Features.CartFeatures.CartUpdate;
using FoodOrdering.Application.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.PL.Controllers
{
    public class CartController : Controller
    {
        private readonly IServiceCart serviceCart;
        public CartController(IServiceCart serviceCart)
        {
            this.serviceCart = serviceCart;
        }
        public async Task<IActionResult> Index(int pg=1)
        {
            string name = User.Identity.Name;
            var carts = await serviceCart.GetAllUserCart(name);
            var data = Pagination<CartUpdateViewModel>.GetPaginationData(pg,carts);
            this.ViewBag.Pager = data.Item2;
            double totalprice = await serviceCart.totalprice(name);
            if (carts is not null && totalprice != 0 && data.Item1 is not null)
            {
                ViewBag.price = totalprice;
                ViewBag.list = data.Item1;
                return View();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddToCart(CartViewModel cartViewModel)
        {
            if (ModelState.IsValid)
            {
                string name = User.Identity.Name;

                if (name != null && cartViewModel != null)
                {
                    string result = await serviceCart.AddToCart(cartViewModel, name);
                    if (result != null)
                    {
                        return RedirectToAction("Index", "Cart");
                    }
                }
            }

            return RedirectToAction("Index", "ShowMenu");
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCart(CartUpdateViewModel cartUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                await serviceCart.UpdateCartItem(cartUpdateViewModel);
                return RedirectToAction("Index", "Cart");

            }
            return RedirectToAction("Index", "Cart");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteCartItem(int Id)
        {
            if (Id != 0)
            {
                await serviceCart.DeleteCartItem(Id);
                return RedirectToAction("Index", "Cart");

            }
            return RedirectToAction("Index", "Cart");
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteAllCartItems()
        {
            string name = User.Identity.Name;
            if (name != null)
            {
                await serviceCart.DeleteAllCartItems(name);
                return RedirectToAction("Index", "Cart");

            }
            return RedirectToAction("Index", "Cart");
        }
    }
}
