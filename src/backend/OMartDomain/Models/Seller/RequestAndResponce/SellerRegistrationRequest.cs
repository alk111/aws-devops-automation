using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.Seller.RequestAndResponce
{
    public class SellerRegistrationRequest
    {
        [Required]
        public string user_id { get; set; }

        //public string CompanyNumber { get; set; } = string.Empty; // Default value: empty string
        public string contactPhoneNumber { get; set; } = string.Empty; // Default value: empty string
        public string CompanyType { get; set; } = string.Empty; // Default value: empty string
        public string CompanyName { get; set; } = string.Empty; // Default value: empty string
        public string address { get; set; } = string.Empty;
    }
}