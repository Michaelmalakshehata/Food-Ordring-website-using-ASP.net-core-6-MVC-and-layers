using FoodOrdering.Application.Common;
using FoodOrdering.Application.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.PL.Controllers
{
    [Authorize(Roles = Roles.adminrole)]
    public class FinishedOrderController : Controller
    {
        private readonly IServiceFinishedOrder serviceFinishedOrder;
        public FinishedOrderController(IServiceFinishedOrder serviceFinishedOrder)
        {
            this.serviceFinishedOrder = serviceFinishedOrder;
        }

        public async Task<IActionResult> Index()
        {
            var listorders = await serviceFinishedOrder.GetAllFinishedOrder();
            return View(listorders);
        }
        [HttpGet]
        public async Task<IActionResult> AddFinishedOrder(int Id)
        {
            await serviceFinishedOrder.AddFinishedOrder(Id);
            return RedirectToAction("Index", "RequestOrder");
        }
    }
}
