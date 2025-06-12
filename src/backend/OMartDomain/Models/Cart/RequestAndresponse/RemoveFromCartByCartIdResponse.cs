using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.Cart.RequestAndresponse
{
    public class RemoveFromCartByCartIdResponse
    {
        
        public string message{ get; set; }
        public List<UserCarts> carts { get; set; }
    }
}