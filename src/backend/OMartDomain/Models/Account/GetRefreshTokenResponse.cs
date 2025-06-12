using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMartDomain.Models.Account
{
    public class GetRefreshTokenResponse
    {
        public int RefreshTokenId { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; }
        public string CreatedByIp { get; set; }
        public DateTime? Revoked { get; set; }
        public string RevokedByIp { get; set; }
        public string ReplacedByToken { get; set; }
        public string ReasonRevoked { get; set; }

        // User details
      
        public string contact_email { get; set; }
        public string last_name { get; set; }
        public string first_name { get; set; }
        public string user_id { get; set; }


    }
}
