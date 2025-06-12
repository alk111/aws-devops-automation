using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.Seller
{
    public class SellerEmailRequest
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
    }
}