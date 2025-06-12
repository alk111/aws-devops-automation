using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMartDomain.Models.EnDelTimeSlot;

namespace OMartDomain.Models.Products.RequestAndResponce
{
    public class GetDeliveryTimeSlotsByEntityIDResponse
    {
        public string message { get; set; }
        public List<EnDelTimeSlot> enDelTimeSlots { get; set; }
    }
}