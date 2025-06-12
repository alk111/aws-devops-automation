using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMartDomain.Models.User;

namespace OMartDomain.Models.Order.RequestAndResponse
{
    public class GetBuyerUserDetailsResponse
    {
           public UserDetails userDetails{ get; set; }
    }
}