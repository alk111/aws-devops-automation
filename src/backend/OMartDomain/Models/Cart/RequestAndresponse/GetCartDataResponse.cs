using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.Cart.RequestAndresponse
{
    public class GetCartDataResponse
    {
          public  List<UserCarts> carts { get; set; }
    }
}