using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IMS.Application.Features.Product.ProductManagementModel;
using static IMS.Application.Features.StockLevel.StockLevelManagementModel;

namespace IMS.Application.Interfaces.IEntitiesRepo
{
    public interface IStockLevelRepo
    {
        Task<IEnumerable<GetStockLevelsByProductIdResponse>> GetStockLevelsByProductIdAsync(int productId);
        Task<GeneralResponse> AddStockLevelAsync(AddStockLevelRequest request, string UserId);
        Task<int> GetCurrentStockAsync(int productId);
        Task<IEnumerable<GetProductsResponse>> GetProductsAreLowStockAsync(string UserId);
    }
}
