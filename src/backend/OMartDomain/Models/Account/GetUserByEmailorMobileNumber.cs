using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMartDomain.Models.Account
{
    public  class GetUserByEmailorMobileNumber
    {
        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        public string? MobileNumber { get; set; }
    }
}
