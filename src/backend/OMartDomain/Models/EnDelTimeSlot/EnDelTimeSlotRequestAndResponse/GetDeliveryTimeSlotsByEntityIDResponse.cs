using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartDomain.Models.EnDelTimeSlot.EnDelTimeSlotRequestAndResponse
{
    public class GetDeliveryTimeSlotsByEntityIDResponse
    {
        public string message { get; set; }
        public List<EnDelTimeSlot> enDelTimeSlots { get; set; }
    }
}