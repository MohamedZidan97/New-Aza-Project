using IMS.Application.Features.Report;
using IMS.Application.Interfaces.IEntitiesRepo;
using IMS.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IMS.Application.Features.Report.ReportManagementModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace IMS.Persistance.Repositories.EntitiesRepo
{
    public class ReportDashboardRepo : IReportDashboardRepo
    {
        private readonly ApplicationDbContext context;

        public ReportDashboardRepo(ApplicationDbContext context)
        {
            this.context = context;
        } 
        public async Task<IEnumerable<GetReportProductResponse>> GetReportProductAsync(FilterReports model)
        {
            var reports = new List<GetReportProductResponse>();

            reports = await (from pro in context.products
                             join sto in context.stockLevels
                             on pro.ProductId equals sto.ProductId
                             // Ensure correct property name
                             where (model.SubCategoryId == null || pro.SubCategoryId == model.SubCategoryId)
                             && (model.From == null || sto.DateUpdated >= model.From)
                             && (model.To == null || sto.DateUpdated <= model.To)
                             group sto by new
                             {
                                 pro.ProductId,
                                 pro.ProductName,
                                 pro.Price,
                                 pro.SupplierId
                             } into g
                             select new GetReportProductResponse
                             {
                                 ProductId = g.Key.ProductId,
                                 ProductName = g.Key.ProductName,
                                 Price = g.Key.Price,
                                 SupplierId = g.Key.SupplierId
                             }).ToListAsync();


            return reports;
        }
    
        public async Task<IEnumerable<GetProductsBySupplierIdResponse>> GetProductsBySupplierIdAsync(int SupplierId)
        {
            var products =await context.products.Where(e=>e.SupplierId == SupplierId)
                .Select(e=> new GetProductsBySupplierIdResponse { Price =e.Price, ProductId =e.ProductId, ProductName =e.ProductName})
                .ToListAsync();

            return products;
        }
        public async Task<IEnumerable<GetProductsBySupplierIdResponse>> GetProductsByCategoryIdAsync(int CategoryId)
        {
            var products = await context.products.Where(e => e.SubCategoryId == CategoryId)
                .Select(e => new GetProductsBySupplierIdResponse { Price = e.Price, ProductId = e.ProductId, ProductName = e.ProductName })
                .ToListAsync();

            return products;
        }
        public async Task<IEnumerable<GetCategoriesToAllPRoductsResponse>> GetCategoriesToAllPRoductsAsync(string UserId)
        {
            var reports = from cat in context.subCategories
                          join pro in context.products
                          on cat.SubCategoryId equals pro.SubCategoryId
                          where pro.ApplicationUserId == UserId
                          group cat by new
                          {
                             cat.Name,
                             cat.SubCategoryId
                          } into g
                          select new GetCategoriesToAllPRoductsResponse
                          {
                               CategoryId = g.Key.SubCategoryId,
                                CategoryName =g.Key.Name
                          };


            return await reports.ToListAsync();
        }
    }
}
