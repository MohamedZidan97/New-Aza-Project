using IMS.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IMS.Application.Features.Product.ProductManagementModel;
using static IMS.Application.Features.Supplier.SupplierManagementModel;

namespace IMS.Application.Interfaces.IEntitiesRepo
{
    public interface ISupplierRepo
    {
        Task<IEnumerable<GetSuppliersResponse>> GetSuppliersRepo(string UserId);
        Task<GetSupplierByIdResponse> GetSupplierByIdAsync(int supplierId);
        Task<GeneralResponse> AddSupplierAsync(AddSupplierRequest request,string UserId);
        Task<GeneralResponse> UpdateSupplierAsync(UpdateSupplierRequest request);
        Task<GeneralResponse> DeleteSupplierAsync(int supplierId);
    }
}
