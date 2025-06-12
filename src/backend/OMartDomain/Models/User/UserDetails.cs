using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.User
{
    public class UserDetails
    {

        public Guid user_id { get; set; } = Guid.NewGuid(); // Default value
        public int iterations { get; set; } = 0; // Default value
        public string first_name { get; set; } = string.Empty; // Default value
        public string middle_name { get; set; } = string.Empty; // Default value
        public string last_name { get; set; } = string.Empty; // Default value
        public DateTime? date_of_birth { get; set; } = null; // Default value
        public string country { get; set; } = string.Empty; // Default value
        public string residential_address { get; set; } = string.Empty; // Default value
        public string permanent_address { get; set; } = string.Empty; // Default value
        public string contact_phone_number { get; set; } = string.Empty; // Default value
        public string contact_email { get; set; } = string.Empty; // Default value
        public bool TwoFA_verified { get; set; } = false; // Default value
        public DateTime registered_on { get; set; } = DateTime.UtcNow; // Default value
        public DateTime? updated_on { get; set; } = null; // Default value
        public bool is_active { get; set; } = true; // Default value
        public bool is_deleted { get; set; } = false; // Default value
    }
}