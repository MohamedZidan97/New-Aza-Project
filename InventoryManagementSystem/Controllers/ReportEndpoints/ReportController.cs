using IMS.Application.Interfaces.IEntitiesRepo;
using IMS.Domain.Entites;
using IMS.Persistance.Repositories.EntitiesRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static IMS.Application.Features.Product.ProductManagementModel;
using static IMS.Application.Features.Report.ReportManagementModel;

namespace InventoryManagementSystem.Controllers.ReportEndpoints
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly ISupplierRepo supplierRepo;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IStockLevelRepo stockLevelRepo;
        private readonly IReportDashboardRepo repo;
        private readonly ISubCategoryRepo categoryRepo;

        public ReportController(ISupplierRepo supplierRepo,UserManager<ApplicationUser> userManager,IStockLevelRepo stockLevelRepo,IReportDashboardRepo repo,ISubCategoryRepo categoryRepo)
        {
            this.supplierRepo = supplierRepo;
            this.userManager = userManager;
            this.stockLevelRepo = stockLevelRepo;
            this.repo = repo;
            this.categoryRepo = categoryRepo;
        }
        private async Task<string> CheckAuth()
        {
            ApplicationUser user = await userManager.GetUserAsync(User);

            return user.Id;

        }
        [HttpGet]
        public async Task<IActionResult> FilterReportView1()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FilterReportView1(FilterReports filter)
        {
            return RedirectToAction(nameof(GetReportHasProducts), filter);
        }

        [HttpGet]
        public async Task<IActionResult> GetReportHasProducts(FilterReports filter)
        {
            ViewBag.SubCategories = new SelectList(await categoryRepo.GetSubCategoriesRepo(), "SubCategoryId", "SubCategoryName");

            var res = await repo.GetReportProductAsync(filter);

            return View(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetStockLevelReport(int id)
        {
            var stockLevels = await stockLevelRepo.GetStockLevelsByProductIdAsync(id);
            ViewBag.CurrentStock = await stockLevelRepo.GetCurrentStockAsync(id);
            //var pro = await productRepo.GetProductByIdAsync(id);
            //ViewBag.Id = id;

            return View(stockLevels);
        }


        public async Task<IActionResult> GetAllSuppliersReport()
        {
            var suppliers = await supplierRepo.GetSuppliersRepo(await CheckAuth());
            return View(suppliers);
        }
        public class ReportViewModel
        {
            public IEnumerable<GetProductsResponse> Products { get; set; }
            public IEnumerable<GetCategoriesToAllPRoductsResponse>? Categories { get; set; }
        }
        public async Task<IActionResult> GetSupplierHasProductsReport(int id)
        {
            var products = await repo.GetProductsBySupplierIdAsync(id);
         
            return View(products);
        }

        public async Task<IActionResult> GetProductsByCategoryId(int id)
        {
            var products = await repo.GetProductsByCategoryIdAsync(id);

            return View(products);
        }
       

    }
}
