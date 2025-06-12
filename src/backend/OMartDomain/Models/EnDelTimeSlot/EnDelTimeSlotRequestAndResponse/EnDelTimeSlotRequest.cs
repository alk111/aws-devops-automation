using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.EnDelTimeSlot.EnDelTimeSlotRequestAndResponse
{
    public class EnDelTimeSlotRequest
    {
        public int EnDelTimeSlotID{ get; set; }
         public string EnityID{ get; set; }
         public DateTime DeliveryTimeStart{get; set;}
         public DateTime DeliveryTimeEND{get; set;} 
    }
}