using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.Cart
{
    public class Addcarts
    {
          public string user_id{ get; set; }
          public bool total_price{ get; set; }
          public int quantity{ get; set; }
          public string product_id{ get; set; }
          public DateTime added_on{ get; set; }
    }
}