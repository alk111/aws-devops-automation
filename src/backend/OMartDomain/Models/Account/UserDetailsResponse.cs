using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMartDomain.Models.Account
{
    public class UserDetailsResponse
    {
        public string user_id { get; set; }
        public int Iterations { get; set; }
        public string First_Name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public DateTime date_of_birth { get; set; }
        public string Country { get; set; }
        public string residential_address { get; set; }
        public string Permanent_Address { get; set; }
        public string Contact_Phone_Number { get; set; }
        public string Contact_Email { get; set; }

        public DateTime registered_on { get; set; }
        public DateTime updated_on { get; set; }
        public bool is_active { get; set; }
        public bool is_deleted { get; set; }
        public bool is_seller { get; set; }
        public string Secret { get; set; }
        public string entity_id { get; set; }
    }
}
