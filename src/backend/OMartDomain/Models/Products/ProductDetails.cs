using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMartDomain.Models.Products
{
    public class ProductDetails
    {
        public string entity_id { get; set; }
        public string product_id { get; set; }
        //public int Iterations { get; set; }
        public string productName { get; set; }
        public string Category { get; set; }
        public string Details { get; set; }
        public decimal Price { get; set; }
        public int stock_quantity { get; set; }
        public string currency_mode { get; set; }
        public string brand { get; set; }
        //public string location { get; set; }
        public string ImageName { get; set; }
        public string establishment_name { get; set; }
        public bool can_be_replaced { get; set; }
        public int estimated_replacement_time { get; set; }
        public DateTime added_on { get; set; }
        public DateTime updated_on { get; set; }
        public bool is_deleted { get; set; }
        public string? UnitofMeasure { get; set; }
        public string? UnitofQuantity { get; set; }

        //public string? category { get; set; }
        public int? iterations { get; set; }
    }
}
