using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.Order.RequestAndResponse
{
    public class AddOrderRequest
    {
       
        public string user_id { get; set; }
        public string entity_id { get; set; }
      //  public string pickup_address { get; set; }
        public string shipping_address { get; set; }
       
        public decimal price { get; set; }
       
        //public DateTime? alloted_delivery_date { get; set; }
       
        public List<AddOrderListRequest> OrderItems { get; set; }

    }
    public class AddMultipleOrderRequest
    {
        public List<AddOrderRequest> Orders { get; set; }
    }

}

//public bool is_order_delivered { get; set; } 
//public DateTime? order_delivery_time { get; set; }
//public bool is_order_canceled { get; set; } 
//public DateTime? order_cancellation_time { get; set; } 
//public string order_cancellation_reason { get; set; }
//public bool is_return_requested { get; set; } 
//public string order_return_reason { get; set; } = string.Empty;
//public DateTime? retun_pickup_time { get; set; } = null;
//public bool is_replacement_requested { get; set; } = false;
//public DateTime? replacement_time { get; set; } = null;
//public string order_replacement_reason { get; set; } = string.Empty;