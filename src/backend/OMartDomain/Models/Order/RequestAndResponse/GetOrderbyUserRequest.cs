using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMartDomain.Models.Order.RequestAndResponse
{
    public class GetOrderbyUserRequest
    {
        [Required]
        public string user_id { get; set; }

        public bool is_delivered { get; set; } = false;

        public bool is_cancelled { get; set; } = false;

    }
}
