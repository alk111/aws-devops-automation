using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMartDomain.Models.User.RequestAndResponse
{
    public  class UpdateUserDetailsRequest
    {
        public string user_id { get; set; } // Default value
        public string? first_name { get; set; } = string.Empty; // Default value
        public string? middle_name { get; set; } = string.Empty; // Default value
        public string? last_name { get; set; } = string.Empty; // Default value
        //public DateTime? date_of_birth { get; set; } = null; // Default value
        public string? country { get; set; } = string.Empty; // Default value
        public string? residential_address { get; set; } = string.Empty; // Default value
        public string? permanent_address { get; set; } = string.Empty; // Default value
    }
}
