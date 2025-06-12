using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OMartApplication.Services;
using OMartDomain.Models.SampleProductDetails.SampleProductRequestAndResponse;

namespace OMAPI.Controllers
{
    public class SampleProductDetailsController: ControllerBase 
    {
         private readonly ISampleProductDetailsService sampleProductDetailsService;
           public SampleProductDetailsController(ISampleProductDetailsService sampleProductDetailsService)
            {
                this.sampleProductDetailsService = sampleProductDetailsService;
            }

            [HttpPost("insertSampleProduct")]
            public async Task<InsertSampleProductResponse> insertSampleProduct(InsertSampleProductRequest request)
                {
                  return await  sampleProductDetailsService.insertSampleProduct(request);
                }


           [HttpPut("updateSampleProductDetails")]
            public async Task<UpdateSampleProductResponse> updateSampleProductDetails(UpdateSampleProductRequest request)
            {
                return await sampleProductDetailsService.updateSampleProductDetails(request);
            }


            [HttpGet("GetSampleProductBySampleProdID/{SampleProdID}")]
             public async Task<GetSampleProductDetailsResponse> GetSampleProductBySampleProdID(int SampleProdID)
             {
                Console.WriteLine(SampleProdID);
                return await sampleProductDetailsService.GetSampleProductBySampleProdID(SampleProdID); 
             }

             [HttpDelete("deleteSampleProductDetails/{SampleProdID}")]
               public async  Task<UpdateSampleProductResponse> deleteSampleProductDetails(int SampleProdID)
               {
                 return await sampleProductDetailsService.deleteSampleProductDetails(SampleProdID);
               }
    }
}