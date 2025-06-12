using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.Order
{
    public class Orders
    {
     public Guid order_id { get; set; } = Guid.NewGuid();
    public string user_id { get; set; } = string.Empty;
    public string product_id { get; set; } = string.Empty;
    public string pickup_address { get; set; } = string.Empty;
    public string shipping_address { get; set; } = string.Empty;
    public DateTime added_on { get; set; } = DateTime.UtcNow;
    public int quantity { get; set; } = 0;
    public decimal total_price { get; set; } = 0.0m;
    public string mode_of_payment { get; set; } = string.Empty;
    public DateTime? alloted_delivery_date { get; set; } = null;
    public bool is_order_delivered { get; set; } = false;
    public DateTime? order_delivery_time { get; set; } = null;
    public bool is_order_canceled { get; set; } = false;
    public DateTime? order_cancellation_time { get; set; } = null;
    public string order_cancellation_reason { get; set; } = string.Empty;
    public bool is_return_requested { get; set; } = false;
    public string order_return_reason { get; set; } = string.Empty;
    public DateTime? retun_pickup_time { get; set; } = null;
    public bool is_replacement_requested { get; set; } = false;
    public DateTime? replacement_time { get; set; } = null;
    public string order_replacement_reason { get; set; } = string.Empty;
    public List<OrderList> orderLists{ get; set; }= new List<OrderList>();
    
    }
}