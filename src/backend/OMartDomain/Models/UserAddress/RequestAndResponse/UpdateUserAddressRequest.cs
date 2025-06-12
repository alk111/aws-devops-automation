using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.UserAddress.RequestAndResponse
{
    public class UpdateUserAddressRequest
    {
            public int AddressID { get; set; }
            public string UserID { get; set; }
            public string AptStreet { get; set; }
            public string Area { get; set; }
            public string Landmark { get; set; }
            public int PinCode { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Country { get; set; }
    }
}