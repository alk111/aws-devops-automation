using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.Cart.RequestAndresponse
{
    public class RemovefromCartRequest
    {
          public string user_Id { get; set; }
         public string product_id { get; set; }
    }
}