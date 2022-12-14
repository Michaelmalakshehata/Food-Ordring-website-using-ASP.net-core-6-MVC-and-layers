using Food_Restaurant.BL.IRepositories;
using Food_Restaurant.DAL.Constant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Food_Restaurant.Controllers
{
    [Authorize(Roles = Roles.adminrole)]
    public class RequestOrderController : Controller
    {
        private readonly IServiceOrder serviceOrder;

        public RequestOrderController(IServiceOrder serviceOrder)
        {
            this.serviceOrder = serviceOrder;
        }
        public async Task< IActionResult> Index()
        {
            var listorders = await serviceOrder.AllOrders();
            return View(listorders);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteOrder(int Id)
        {
            await serviceOrder.DeleteOrder(Id);
            return RedirectToAction("Index");
        }
    }
}
