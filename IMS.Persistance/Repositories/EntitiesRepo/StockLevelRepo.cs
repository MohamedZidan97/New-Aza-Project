using _VC.Application.Contents.MailServicesIntr;
using AutoMapper;
using IMS.Application.Features.StockLevel;
using IMS.Application.Interfaces;
using IMS.Application.Interfaces.IEntitiesRepo;
using IMS.Domain.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IMS.Application.Features.Product.ProductManagementModel;
using static IMS.Application.Features.StockLevel.StockLevelManagementModel;

namespace IMS.Persistance.Repositories.EntitiesRepo
{
    [Authorize]
    public class StockLevelRepo : IStockLevelRepo
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailServices emailServices;
        private readonly IProductRepo productRepo;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IBaseRepo<StockLevel> baseRepo;

        public StockLevelRepo(UserManager<ApplicationUser> userManager,IEmailServices emailServices,IProductRepo productRepo,ApplicationDbContext context ,IMapper mapper, IBaseRepo<StockLevel> baseRepo )
        {
            this.userManager = userManager;
            this.emailServices = emailServices;
            this.productRepo = productRepo;
            this.context = context;
            this.mapper = mapper;
            this.baseRepo = baseRepo;
        }
    
        public async Task<int> GetCurrentStockAsync(int productId)
        {
            // Fetch all data asynchronously
            var sto = await baseRepo.GetAllAsync();

            // Calculate the sum synchronously, since the data is already in memory
            var buy = sto.Where(sl => sl.ProductId == productId && !sl.Sell).Sum(sl => sl.Quantity);
            var sell = sto.Where(sl => sl.ProductId == productId && sl.Sell).Sum(sl => sl.Quantity);

            return buy-sell;
        }

        public async Task<IEnumerable<GetStockLevelsByProductIdResponse>> GetStockLevelsByProductIdAsync(int productId)
        {

            // First, fetch all data asynchronously
           // var sto = await baseRepo.GetAllAsync();

            // Filter and project the data synchronously since it's already in memory
            var stockLevels =await context.stockLevels.Where(r => r.ProductId == productId)
                .Select(e => new GetStockLevelsByProductIdResponse
                {
                    StockLevelId = e.StockLevelId,
                    ProductId = e.ProductId,
                    DateUpdated = e.DateUpdated,
                    ProductName = e.Product.ProductName,
                    Quantity = e.Quantity,
                    Sell = e.Sell
                })
                .ToListAsync();
            // Use ToList since the data is already in memory

            return stockLevels;
        }

        public async Task<GeneralResponse> AddStockLevelAsync(AddStockLevelRequest request,string UserId)
        {
           
            
           var response =  await baseRepo.AddAsync(mapper.Map<StockLevel>(request));

           var stockCurrent = await GetCurrentStockAsync(request.ProductId);

           var product = await productRepo.GetProductByIdAsync(request.ProductId);
            var user = await userManager.Users.Where(e => e.Id == UserId).FirstOrDefaultAsync();
            if(product.LowStock >= stockCurrent)
            {
                await emailServices.SendEmailAsync(user.Email, "Stock", $"{product.ProductName} - Low Stock:{product.LowStock} - Stock Current:{stockCurrent}");
            }

            return response;
        } 
        public async Task<IEnumerable<GetProductsResponse>> GetProductsAreLowStockAsync(string UserId)
        {

            // Fetch products for the specified user
            var products = await context.products
                .Where(e => e.ApplicationUserId == UserId)
                .Select(r => new GetProductsResponse
                {
                    LowStock = r.LowStock,
                    ImageUrl = r.Image,
                    ProductId = r.ProductId,
                    Price = r.Price,
                    ProductName = r.ProductName
                })
                .ToListAsync();

            // Create a list to store the products that are low on stock
            var lowStockProducts = new List<GetProductsResponse>();

            // Loop through each product and check its current stock level
            foreach (var item in products)
            {
                var currentStock = await GetCurrentStockAsync(item.ProductId);
                if (item.LowStock >= currentStock)
                {
                    lowStockProducts.Add(item); // Add the product to the list if it's low on stock
                }
            }

            // Return the list of low-stock products
            return lowStockProducts;
        }

    }
}
