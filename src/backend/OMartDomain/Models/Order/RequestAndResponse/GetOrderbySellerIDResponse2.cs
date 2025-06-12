using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.Order.RequestAndResponse
{
    public class GetOrderbySellerIDResponse2
    {
        public string order_id { get; set; } = string.Empty;
        public string user_id { get; set; } = string.Empty;

        public string pickup_address { get; set; } = string.Empty;
        public string shipping_address { get; set; } = string.Empty;

        public decimal total_price { get; set; } = 0.0m;
        public string mode_of_payment { get; set; } = string.Empty;
        public DateTime? alloted_delivery_date { get; set; } = null;
        public DateTime added_on { get; set; }
    }
}