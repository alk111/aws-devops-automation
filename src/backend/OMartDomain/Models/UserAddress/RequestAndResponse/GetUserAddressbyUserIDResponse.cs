using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.UserAddress.RequestAndResponse
{
    public class GetUserAddressbyUserIDResponse
    {
        public string message { get; set; }
        public List<UserAddress> userAddress { get; set; }
    }
}