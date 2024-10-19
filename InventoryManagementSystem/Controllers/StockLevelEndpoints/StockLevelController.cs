using IMS.Application.Interfaces.IEntitiesRepo;
using IMS.Domain.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using static IMS.Application.Features.Product.ProductManagementModel;
using static IMS.Application.Features.Report.ReportManagementModel;
using static IMS.Application.Features.StockLevel.StockLevelManagementModel;

namespace InventoryManagementSystem.Controllers.StockLevelEndpoints
{
    [Authorize]
    public class StockLevelController : Controller
    {
        private readonly IReportDashboardRepo dashboardRepo;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IStockLevelRepo repo;
        private readonly IProductRepo productRepo;

        public StockLevelController(IReportDashboardRepo dashboardRepo ,UserManager<ApplicationUser> userManager,IStockLevelRepo repo,IProductRepo productRepo)
        {
            this.dashboardRepo = dashboardRepo;
            this.userManager = userManager;
            this.repo = repo;
            this.productRepo = productRepo;
        }
        // View stock levels for a specific product
        private async Task<string> CheckAuth()
        {
            ApplicationUser user = await userManager.GetUserAsync(User);

            return user.Id;

        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var stockLevels = await repo.GetStockLevelsByProductIdAsync(id);
            ViewBag.CurrentStock = await repo.GetCurrentStockAsync(id);
            var pro = await productRepo.GetProductByIdAsync(id);
            ViewBag.Id = id;
           
            return View(stockLevels);
        }

        // Update stock for a product
        [HttpGet]
        public async Task<IActionResult> AddOperationOnStockOfProduct(int id,bool sell)
        {
            var pro = await productRepo.GetProductByIdAsync (id);
            ViewBag.CurrentStock = await repo.GetCurrentStockAsync(id);

            var sto = new AddStockLevelRequest
            {
                DateUpdated = DateTime.UtcNow,
                ProductId = id,
                Quantity = 0,
                Sell = sell
            };
           


            return View(sto);
        }
        [HttpPost]
        public async Task<IActionResult> AddOperationOnStockOfProduct(AddStockLevelRequest model)
        {
            ViewBag.CurrentStock = await repo.GetCurrentStockAsync(model.ProductId);
            if (ModelState.IsValid)
            {
                var CurrentStock = await repo.GetCurrentStockAsync(model.ProductId);
                if(model.Sell && model.Quantity > CurrentStock)
                {
                    ModelState.AddModelError("", "Quantity Large from CurrentStock");
                    return View(model);
                }
                await repo.AddStockLevelAsync(model,await CheckAuth());
                return RedirectToAction(nameof(Details),new { id = model.ProductId });
            }
            return View(model);
        }

        public class ReportViewModel
        {
            public IEnumerable<GetProductsResponse> Products { get; set; }
            public IEnumerable<GetCategoriesToAllPRoductsResponse> Categories { get; set; }
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsAreLowStock()
        {
            var products = await repo.GetProductsAreLowStockAsync(await CheckAuth());
            var cats = await dashboardRepo.GetCategoriesToAllPRoductsAsync(await CheckAuth());

            var models = new ReportViewModel
            {
                Products = products,
                Categories = cats
            };
            return View(models);

        }
        //[HttpGet]
        //public async Task<IActionResult> GetCategoriesToAllPRoducts()
        //{
        //    var cats = await dashboardRepo.GetCategoriesToAllPRoductsAsync(await CheckAuth());

        //    return PartialView("~/Views/Report/GetCategoriesToAllPRoducts.cshtml", cats);
        //}


    }
}

