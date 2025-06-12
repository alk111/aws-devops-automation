using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.Cart
{
    public class UserCarts
    {
        public int CartID { get; set; }
        public string user_id { get; set; }
        public string product_id { get; set; }
        public DateTime added_on { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public string entity_id { get; set; }
        public string establishment_name { get; set; }
        public string productName { get; set; }
        public string ImageName { get; set; }


    }
}