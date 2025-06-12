using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.Seller.RequestAndResponce
{
    public class UpdateSellerDetails
    {
        [Required]
        public string seller_Id { get; set; }
        public string? address { get; set; } = string.Empty;
        public string? contact_no_2 { get; set; } = string.Empty;
        public string? contact_email_2 { get; set; } = string.Empty;
        public string? establishment_name { get; set; } = string.Empty;
        public string? establishment_type { get; set; } = string.Empty;
        public string? gmaps_location { get; set; } = string.Empty;
       
    }
}