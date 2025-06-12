using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.UserAddress.RequestAndResponse
{
    public class InsertUserAddressRequest
    {
            public string userID{ get; set; }
            public string aptStreet{ get; set; }
            public string Area{ get; set; }
            public string landmark{ get; set; }
            public int pinCode{ get; set; }
            public string city{ get; set; }
            public string state{ get; set; }
            public string country{ get; set; }
    }
}