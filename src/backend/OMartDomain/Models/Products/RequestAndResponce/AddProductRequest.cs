using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.Products.RequestAndResponce
{
    public class AddProductRequest
    {
    public string entity_id { get; set; }
    public string product_id { get; set; }
    public int iterations { get; set; }
    public string category { get; set; }
    public string details { get; set; }
    public decimal price { get; set; }
    public int stock_quantity { get; set; }
    public string currency_mode { get; set; }
    public int estimated_delivery_time { get; set; }
    public bool can_be_returned { get; set; }
    public int estimated_return_pickup_time { get; set; }
    public bool can_be_replaced { get; set; }
    public int estimated_replacement_time { get; set; }
    public DateTime added_on { get; set; }
    public DateTime updated_on { get; set; }
    public bool is_deleted { get; set; }
    public string productName{ get; set; }
     public string Subscribeable {get; set;}
    }
}