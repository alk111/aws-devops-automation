using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.Order.RequestAndResponse
{
    public class InserOrderListRequest
    {
        public string OrderID{ get; set; }
        public string ProductID{ get; set; }
        public double Quantity{ get; set; }
        public double Price{ get; set; }
        public double SalePrice{ get; set; }
        public double TaxAmount{ get; set; }
        public double DeliveryAmount{ get; set; }
        public double Amount{ get; set; }
        public string TaxID{ get; set; }
    }
}