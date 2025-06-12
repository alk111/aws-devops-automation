using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.Seller.RequestAndResponce
{
    public class VerifySellerOtpWithEmailRequest
    {
        [Required]
        public int otp { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
    }
}