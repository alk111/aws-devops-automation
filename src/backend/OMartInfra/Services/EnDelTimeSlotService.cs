using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMartApplication.Repositories;
using OMartApplication.Services;
using OMartDomain.Models.EnDelTimeSlot.EnDelTimeSlotRequestAndResponse;

namespace OMartInfra.Services
{
    public class EnDelTimeSlotService : IEnDelTimeSlotService
    {
        private readonly  IEnDelTimeSlotRepository enDelTimeSlotRepository;

        public EnDelTimeSlotService(IEnDelTimeSlotRepository enDelTimeSlotRepository)
        {
            this.enDelTimeSlotRepository=enDelTimeSlotRepository;
        }

        public async   Task<EnDelTimeSlotResponse> AddDeliveryTimeSlot(EnDelTimeSlotRequest request)
        {
           return await enDelTimeSlotRepository.AddDeliveryTimeSlot(request);
        }
       

        public async Task<EnDelTimeSlotResponse> DeleteEnDeliveryTimeSlot(int EnDelTimeSlotID)
        {
            return await enDelTimeSlotRepository.DeleteEnDeliveryTimeSlot(EnDelTimeSlotID);
        }
        public async Task<UpdateEnDelTimeSlotResponse> UpdateEnDelTimeSlot(UpdateEnDelTimeSlotRequest request)
        {
            return await enDelTimeSlotRepository.UpdateEnDelTimeSlot(request);
        }
    }
}