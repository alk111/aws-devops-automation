using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMartDomain.Models.Order.RequestAndResponse
{
    public class GetOrderbySellerRequest
    {
        public string seller_id { get; set; }
        public bool is_delivered { get; set; } = false;

        public bool is_cancelled { get; set; } = false;
    }
}
