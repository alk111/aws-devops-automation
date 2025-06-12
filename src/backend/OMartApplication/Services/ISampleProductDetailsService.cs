using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMartDomain.Models.SampleProductDetails.SampleProductRequestAndResponse;

namespace OMartApplication.Services
{
    public interface ISampleProductDetailsService
    {
         Task<InsertSampleProductResponse> insertSampleProduct(InsertSampleProductRequest request);

        Task<UpdateSampleProductResponse> updateSampleProductDetails(UpdateSampleProductRequest request);

        Task<GetSampleProductDetailsResponse> GetSampleProductBySampleProdID(int SampleProdID);
         Task<UpdateSampleProductResponse> deleteSampleProductDetails(int SampleProdID);
    }
}