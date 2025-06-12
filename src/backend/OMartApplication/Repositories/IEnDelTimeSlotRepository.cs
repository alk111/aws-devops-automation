using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMartDomain.Models.EnDelTimeSlot.EnDelTimeSlotRequestAndResponse;

namespace OMartApplication.Repositories
{
    public interface IEnDelTimeSlotRepository
    {
        Task<EnDelTimeSlotResponse> AddDeliveryTimeSlot(EnDelTimeSlotRequest request);
       
        Task<EnDelTimeSlotResponse> DeleteEnDeliveryTimeSlot(int EnDelTimeSlotID);
        Task<UpdateEnDelTimeSlotResponse> UpdateEnDelTimeSlot(UpdateEnDelTimeSlotRequest request);

    }
}