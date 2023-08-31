using FoodOrdering.Application.Common;
using FoodOrdering.Application.Common.Pagination;
using FoodOrdering.Application.Repositories;
using FoodOredering.Domain.Entities;
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

        public async Task<IActionResult> Index(int pg=1)
        {
            var listorders = await serviceFinishedOrder.GetAllFinishedOrder();
            var data = Pagination<FinishedOrders>.GetPaginationData(pg, listorders);
            this.ViewBag.Pager = data.Item2;
            return View(data.Item1);
        }
        [HttpGet]
        public async Task<IActionResult> AddFinishedOrder(int Id)
        {
            await serviceFinishedOrder.AddFinishedOrder(Id);
            return RedirectToAction("Index", "RequestOrder");
        }
    }
}
