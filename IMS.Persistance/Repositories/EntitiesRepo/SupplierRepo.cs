using AutoMapper;
using Humanizer;
using IMS.Application.Interfaces;
using IMS.Application.Interfaces.IEntitiesRepo;
using IMS.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IMS.Application.Features.Product.ProductManagementModel;
using static IMS.Application.Features.Report.ReportManagementModel;
using static IMS.Application.Features.Supplier.SupplierManagementModel;

namespace IMS.Persistance.Repositories.EntitiesRepo
{
    public class SupplierRepo : ISupplierRepo
    {
        private readonly IBaseRepo<userSupplier> userSupRepo;
        private readonly ApplicationDbContext context;
        private readonly IBaseRepo<Supplier> baseRepo;
        private readonly IMapper mapper;

        public SupplierRepo(IBaseRepo<userSupplier> userSupRepo,ApplicationDbContext context,IBaseRepo<Supplier> baseRepo, IMapper mapper)
        {
            this.userSupRepo = userSupRepo;
            this.context = context;
            this.baseRepo = baseRepo;
            this.mapper = mapper;
        }
        public async Task<GeneralResponse> AddSupplierAsync(AddSupplierRequest request, string UserId)
        {

            var t1 = new Supplier
            {
                SupplierName = request.SupplierName,
                ContactInfo = request.ContactInfo
            };
            await context.suppliers.AddAsync(t1);
            await context.SaveChangesAsync();

            var t2 = new userSupplier {  SupplierId = t1.SupplierId, ApplicationUserId = UserId };
            var res = await userSupRepo.AddAsync(t2);

            return res;

        }
        public async Task<GeneralResponse> DeleteSupplierAsync(int supplierId)
       => await baseRepo.DeleteAsync(supplierId);
        public async Task<GetSupplierByIdResponse> GetSupplierByIdAsync(int supplierId)
        => mapper.Map<GetSupplierByIdResponse>(await baseRepo.GetByIdAsync(supplierId));
        public async Task<IEnumerable<GetSuppliersResponse>> GetSuppliersRepo(string UserId)
        {

            return await (from sup in context.suppliers
                          join pro in context.userSuppliers
                          on sup.SupplierId equals pro.SupplierId
                          where pro.ApplicationUserId == UserId
                          group pro by new
                          {
                              sup.SupplierId,
                              sup.SupplierName

                          } into g
                          select new GetSuppliersResponse
                          {
                              SupplierId = g.Key.SupplierId,
                              SupplierName = g.Key.SupplierName
                          }).ToListAsync();


        }
        public async Task<GeneralResponse> UpdateSupplierAsync(UpdateSupplierRequest request)
         => await baseRepo.UpdateAsync(mapper.Map<Supplier>(request));
    }
}
