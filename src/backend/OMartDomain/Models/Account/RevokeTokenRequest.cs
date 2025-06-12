using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMartDomain.Models.Account
{
    public class RevokeTokenRequest
    {
        public string Token { get; set; }
        public string? RevokedByIp { get; set; }
        public string? Reason { get; set; }
    }
}
