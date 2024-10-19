using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Features.Supplier
{
    public class SupplierManagementModel
    {
        // get all
        public class GetSuppliersResponse
        {
            public int SupplierId { get; set; }
            public string SupplierName { get; set;}
        }

        // add 
        public class AddSupplierRequest
        {
            public int SupplierId { get; set; }
            public string SupplierName { get; set; }
            public string ContactInfo { get; set; }

        }
        // update 
        public class UpdateSupplierRequest
        {
            public int SupplierId { get; set; }
            public string SupplierName { get; set; }
            public string ContactInfo { get; set; }

        }
        // Get By Id
        public class GetSupplierByIdResponse
        {
            public int SupplierId { get; set; }
            public string SupplierName { get; set; }
            public string ContactInfo { get; set; }

        }
    }
}
