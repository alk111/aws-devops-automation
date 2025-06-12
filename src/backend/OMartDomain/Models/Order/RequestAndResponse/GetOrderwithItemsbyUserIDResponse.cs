using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.Order.RequestAndResponse
{
    public class GetOrderwithItemsbyUserIDResponse
    {
        public string message{get; set;}
        public List<GetOrderResponse> orders{get; set;} 
    }
}