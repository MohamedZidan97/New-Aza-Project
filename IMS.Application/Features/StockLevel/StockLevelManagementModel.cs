using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Features.StockLevel
{
    public class StockLevelManagementModel
    {
        public class GetStockLevelsByProductIdResponse
        {
            public int StockLevelId { get; set; }
            public int ProductId { get; set; }
            public string? ProductName { get; set; }
            public int Quantity { get; set; }
            public DateTime DateUpdated { get; set; }
            public bool Sell { get; set; }
        }

        public class AddStockLevelRequest
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public DateTime DateUpdated { get; set; }
            public bool Sell { get; set; }

        }
    }
}
