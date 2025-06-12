using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.User.RequestAndResponse
{
    public class UserCreationRequest
    {
        
        public string? FirstName { get; set; }
        
        public string? MiddleName { get; set; }

        public string? LastName { get; set; } = "";
       
        public string? ContactPhoneNumber { get; set; }
        [Required]
        public string? ContactEmail { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}