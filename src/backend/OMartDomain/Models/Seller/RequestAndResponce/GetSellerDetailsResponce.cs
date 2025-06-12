using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OMartDomain.Models.Seller.RequestAndResponce
{
    public class GetSellerDetailsResponce
    {
        public string contact_email_1 { get; set; } = string.Empty;
        public string contact_email_2 { get; set; } = string.Empty;
        public string contact_no_1 { get; set; } = string.Empty; 
        public string contact_no_2 { get; set; } = string.Empty; 
        public string address { get; set; } = string.Empty; 
        public string establishment_name { get; set; } = string.Empty; 
        public string establishment_type { get; set; } = string.Empty;
        public string gmaps_location { get; set; } = string.Empty;
        

    }
}