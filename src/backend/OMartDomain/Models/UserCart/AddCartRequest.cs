using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMartDomain.Models.UserCart
{
    public class AddCartRequest
    {
        public  string user_id { get; set; }
        public string product_id { get; set; }
        public int quantity { get; set; }
        public string? entity_id{ get; set; }
         
    }
}
