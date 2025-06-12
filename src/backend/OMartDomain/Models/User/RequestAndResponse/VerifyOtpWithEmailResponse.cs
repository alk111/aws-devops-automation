using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.User.RequestAndResponse
{
    public class VerifyOtpWithEmailResponse
    {
        public bool is_expired { get; set; }
        public bool is_verified { get; set; }
        public string verificationMessage { get; set; }
    }
}