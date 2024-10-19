using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Features.Report
{
    public class ReportManagementModel
    {

        public class GetReportProductResponse
        {
            public int ProductId { get; set;}
            public string ProductName { get; set;}
            public decimal Price { get; set;}
            public int SupplierId { get; set;}
        }
        public class GetProductsBySupplierIdResponse
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public decimal Price { get; set; }
           
        }

        public class GetCategoriesToAllPRoductsResponse
        {
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }

        }
        public class FilterReports
        {
            public int? SubCategoryId { get; set; }
            public DateTime? From { get; set;}
            public DateTime? To { get; set;}
        }
    }
}
