using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMartDomain.Models.Order.RequestAndResponse
{
    public class GetOrderbyUserResponse
    {
        public string Message { get; set; }
        public List<Orders> Orders { get; set; }
    }
}
