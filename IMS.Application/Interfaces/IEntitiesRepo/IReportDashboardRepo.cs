using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IMS.Application.Features.Report.ReportManagementModel;

namespace IMS.Application.Interfaces.IEntitiesRepo
{
    public interface IReportDashboardRepo
    {
        Task<IEnumerable<GetReportProductResponse>> GetReportProductAsync(FilterReports model);

        Task<IEnumerable<GetProductsBySupplierIdResponse>> GetProductsBySupplierIdAsync(int SupplierId);
        Task<IEnumerable<GetProductsBySupplierIdResponse>> GetProductsByCategoryIdAsync(int CategoryId);


        Task<IEnumerable<GetCategoriesToAllPRoductsResponse>> GetCategoriesToAllPRoductsAsync(string UserId);

    }
}
