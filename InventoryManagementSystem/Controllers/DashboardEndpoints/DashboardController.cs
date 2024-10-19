using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers.DashboardEndpoints
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
