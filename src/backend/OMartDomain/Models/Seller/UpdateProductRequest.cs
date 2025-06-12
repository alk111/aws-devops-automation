using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMartDomain.Models.Seller
{
    public class UpdateProductRequest
    {
        public string entity_id { get; set; }
        [MaxLength(36)]
        public string product_id { get; set; }
        public int? iterations { get; set; }
        public string? category { get; set; }
        public string? details { get; set; }
        public string? brand { get; set; }
        public decimal price { get; set; }
        public int stock_quantity { get; set; }
        [MaxLength(3)]
        public string currency_mode { get; set; }
        public int? estimated_delivery_time { get; set; }
        public bool? can_be_returned { get; set; }
        public int? estimated_return_pickup_time { get; set; }
        public bool? can_be_replaced { get; set; }
        public int? estimated_replacement_time { get; set; }
      //  public DateTime? added_on { get; set; }
      //  public DateTime? updated_on { get; set; }
        public bool? isSubscribable { get; set; } = false;

        public string? productName { get; set; }
  
        public string? UnitofMeasure { get; set; }
        public string? UnitofQuantity { get; set; }
    }
}
