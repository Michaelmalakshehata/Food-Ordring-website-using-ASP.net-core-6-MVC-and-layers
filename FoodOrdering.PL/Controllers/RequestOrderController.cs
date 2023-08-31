using FoodOrdering.Application.Common;
using FoodOrdering.Application.Common.Pagination;
using FoodOrdering.Application.Repositories;
using FoodOredering.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.PL.Controllers
{
    [Authorize(Roles = Roles.adminrole)]
    public class RequestOrderController : Controller
    {
        private readonly IServiceOrder serviceOrder;

        public RequestOrderController(IServiceOrder serviceOrder)
        {
            this.serviceOrder = serviceOrder;
        }
        public async Task<IActionResult> Index(int pg=1)
        {
            var listorders = await serviceOrder.AllOrders();
            var data = Pagination<Orders>.GetPaginationData(pg, listorders);
            this.ViewBag.Pager = data.Item2;
            return View(data.Item1);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteOrder(int Id)
        {
            await serviceOrder.DeleteOrder(Id);
            return RedirectToAction("Index");
        }
    }
}
