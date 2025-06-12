using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.Order.RequestAndResponse
{
    public class GetOrderbySellerIDResponse
    {
        public string message{ get; set;}
        public List<GetOrderbySellerIDResponse2> orders{get; set;} 
    }
}