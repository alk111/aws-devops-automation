using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.Order
{
    public class EntityAddress
    {
         public string country { get; set; } = string.Empty; // Default value
        public string residential_address { get; set; } = string.Empty; // Default value
        public string permanent_address { get; set; } = string.Empty; // Default value 
    }
}