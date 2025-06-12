using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OMartApplication.Services;
using OMartDomain.Models.SampleProdFields.ProductFieldsRequestAndResponse;
using OMartInfra.Services;

namespace OMAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SampleProdFieldsController : ControllerBase
    {
        private readonly ISampleProdFieldsService sampleProdFieldsService;

        public SampleProdFieldsController(ISampleProdFieldsService sampleProdFieldsService)
        {
            this.sampleProdFieldsService = sampleProdFieldsService;
        }
        
        [HttpPost("InsertSampleProductField")]
        public async Task<InsertSampleProdFieldsResponse> InsertSampleProductField(InsertSampleProdFieldsRequest request)
        {
            return await sampleProdFieldsService.InsertSampleProductField(request);
        }

        [HttpPut("UpdateSampleProductField")]
        public async Task<UpdateSampleProdFieldsResponse> UpdateSampleProductField(UpdateSampleProdFieldsRequest request)
        {
            return await sampleProdFieldsService.UpdateSampleProductField(request);
        }

        [HttpGet("GetBySampleProdOptionalFieldID/{SampleProdOptionalFieldID}")]
        public async Task<GetBySampleProdFieldsResponse> GetBySampleProdOptionalFieldID(int SampleProdOptionalFieldID)
        {
            return await sampleProdFieldsService.GetBySampleProdOptionalFieldID(SampleProdOptionalFieldID);
        }

        [HttpDelete("DeleteBySampleProdOptionalFieldID/{SampleProdOptionalFieldID}")]
        public async Task<UpdateSampleProdFieldsResponse> DeleteBySampleProdOptionalFieldID(int SampleProdOptionalFieldID)
        {
            return await sampleProdFieldsService.DeleteBySampleProdOptionalFieldID(SampleProdOptionalFieldID);
        }
    }
}