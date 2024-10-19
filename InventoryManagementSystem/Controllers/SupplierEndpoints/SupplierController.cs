using IMS.Application.Interfaces.IEntitiesRepo;
using IMS.Domain.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static IMS.Application.Features.Supplier.SupplierManagementModel;

namespace InventoryManagementSystem.Controllers.SupplierEndpoints
{
    [Authorize]
    public class SupplierController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ISupplierRepo repo;

        public SupplierController(UserManager<ApplicationUser> userManager,ISupplierRepo repo)
        {
            this.userManager = userManager;
            this.repo = repo;
        }

        private async Task<string> CheckAuth()
        {
            ApplicationUser user = await userManager.GetUserAsync(User);

            return user.Id;

        }
        public async Task<IActionResult> GetAllSuppliers()
        {
            var suppliers = await repo.GetSuppliersRepo(await CheckAuth());
            return View(suppliers);
        }

        public IActionResult AddSupplier()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSupplier(AddSupplierRequest supplier)
        {
            if (ModelState.IsValid)
            {
                await repo.AddSupplierAsync(supplier, await CheckAuth());
                return RedirectToAction(nameof(GetAllSuppliers));
            }
            return View(supplier);
        }

        public async Task<IActionResult> UpdateSupplier(int id)
        {
            var supplier = await repo.GetSupplierByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            var sup = new UpdateSupplierRequest
            {
                SupplierId = supplier.SupplierId,
                SupplierName = supplier.SupplierName,
                ContactInfo = supplier.ContactInfo
            };
            return View(sup);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSupplier(UpdateSupplierRequest supplier)
        {
            if (ModelState.IsValid)
            {
                await repo.UpdateSupplierAsync(supplier);
                return RedirectToAction(nameof(GetAllSuppliers));
            }
            return View(supplier);
        }


        public async Task<IActionResult> DetailsOfSupplier(int id)
        {
            var supplier = await repo.GetSupplierByIdAsync(id);
            if (supplier == null)
            {
                return RedirectToAction(nameof(GetAllSuppliers));
            }

            return View(supplier);
        }

        public async Task<IActionResult> DeleteSupplier(int id)
        {
            await repo.DeleteSupplierAsync(id);
            return RedirectToAction(nameof(GetAllSuppliers));
        }

    }
}
