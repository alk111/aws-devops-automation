using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.Products
{
    public class EnDelTimeSlot
    {
         public string EnityID{ get; set; }
        public DateTime DeliveryTimeStart{get; set;}
        public DateTime DeliveryTimeEND{get; set;} 
    }
}