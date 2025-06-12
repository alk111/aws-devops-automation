using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.User.RequestAndResponse
{
    public class VerifyOtpWithEmailRequest
    {
        [Required]
        public int otp { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
    }
}