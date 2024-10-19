using AutoMapper;
using IMS.Domain.Entites;
using Microsoft.AspNetCore.SignalR.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IMS.Application.Features.Product.ProductManagementModel;
using static IMS.Application.Features.StockLevel.StockLevelManagementModel;
using static IMS.Application.Features.Supplier.SupplierManagementModel;

namespace IMS.Application.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Product
            CreateMap<Product, GetProductByIdResponse>().ReverseMap();
            CreateMap<Product, AddProductRequest>().ReverseMap();
            CreateMap<Product, UpdateProductRequest>().ReverseMap();

            // supplier
            CreateMap<Supplier, GetSupplierByIdResponse>().ReverseMap();

            CreateMap<Supplier, GetSuppliersResponse>().ReverseMap();

            CreateMap<Supplier, UpdateSupplierRequest>().ReverseMap();

            CreateMap<Supplier, AddSupplierRequest>().ReverseMap();


            // Stock
            //
            CreateMap<StockLevel, AddStockLevelRequest>().ReverseMap();


            #region Account 
            // Register
            //  CreateMap<AccountGeneralResponse, AccountRegisterRequest>().ReverseMap();
            #endregion
        }
    }
}
