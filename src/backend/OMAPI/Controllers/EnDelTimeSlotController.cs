using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OMartApplication.Services;
using OMartDomain.Models.EnDelTimeSlot.EnDelTimeSlotRequestAndResponse;

namespace OMAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnDelTimeSlotController : ControllerBase
    {
        private readonly IEnDelTimeSlotService  enDelTimeSlotService;
        public EnDelTimeSlotController(IEnDelTimeSlotService  enDelTimeSlotService)
        {
            this.enDelTimeSlotService=enDelTimeSlotService;
        }

        // [HttpPost("AddDeliveryTimeSlot")]
        // public   async Task<EnDelTimeSlotResponse> AddDeliveryTimeSlot(EnDelTimeSlotRequest request)
        // {
        //    return await enDelTimeSlotService.AddDeliveryTimeSlot(request);
        // }
        
       

        // [HttpDelete("DeleteEnDeliveryTimeSlot/{EnDelTimeSlotID}")]
        //public async Task<EnDelTimeSlotResponse> DeleteEnDeliveryTimeSlot(int EnDelTimeSlotID)
        // {
        //    return await enDelTimeSlotService.DeleteEnDeliveryTimeSlot(EnDelTimeSlotID);
        // }
         
        // [HttpPut("UpdateEnDelTimeSlot")]
        // public async Task<UpdateEnDelTimeSlotResponse> UpdateEnDelTimeSlot(UpdateEnDelTimeSlotRequest request)
        // {
        //    return await enDelTimeSlotService.UpdateEnDelTimeSlot(request);
        // }
    }
}