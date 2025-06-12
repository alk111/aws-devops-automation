using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.Order.RequestAndResponse
{
    public class GetOrderListByOrderIdRequest
    {
        public string OrderID{ get; set; }
    }
}