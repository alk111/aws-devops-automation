using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.EnDelTimeSlot.EnDelTimeSlotRequestAndResponse
{
    public class UpdateEnDelTimeSlotRequest
    {
        public int EnDelTimeSlotID{get; set;}
        public string EnityID{get; set; }
        public string DeliveryTimeStart{get; set; }
        public string DeliveryTimeEND {get; set; }
    }
}