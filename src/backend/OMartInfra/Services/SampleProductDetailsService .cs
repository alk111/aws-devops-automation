using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMartApplication.Repositories;
using OMartApplication.Services;
using OMartDomain.Models.SampleProductDetails.SampleProductRequestAndResponse;

namespace OMartInfra.Services
{
    public class SampleProductDetailsService : ISampleProductDetailsService 
    {
          private readonly ISampleProductRepository sampleProductRepository;

        public SampleProductDetailsService( ISampleProductRepository sampleProductRepository)
        {
            this.sampleProductRepository = sampleProductRepository;
        }

        public async Task<InsertSampleProductResponse> insertSampleProduct(InsertSampleProductRequest request)
        {
           return await sampleProductRepository.insertSampleProduct(request);
        }

       public async Task<UpdateSampleProductResponse> updateSampleProductDetails(UpdateSampleProductRequest request)
       {
          return await sampleProductRepository.updateSampleProductDetails(request);
       }

        public async Task<GetSampleProductDetailsResponse> GetSampleProductBySampleProdID(int SampleProdID)
        {
            return await sampleProductRepository.GetSampleProductBySampleProdID(SampleProdID);
        }

        public async  Task<UpdateSampleProductResponse> deleteSampleProductDetails(int SampleProdID)
        {
            return await sampleProductRepository.deleteSampleProductDetails(SampleProdID);  
        }
    }
}