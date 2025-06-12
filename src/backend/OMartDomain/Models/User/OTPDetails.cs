using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.User
{
    public class OTPDetails
    {
        public int id;
        public string email;
        public int otp;
        public DateTime created_on;
        public DateTime otp_generated_on;
        public DateTime otp_expires_on;
        public bool is_verified;
        public bool is_expired;
    }
}