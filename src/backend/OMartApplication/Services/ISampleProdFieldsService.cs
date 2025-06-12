using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMartDomain.Models.SampleProdFields.ProductFieldsRequestAndResponse;

namespace OMartApplication.Services
{
    public interface ISampleProdFieldsService
    {
        Task<InsertSampleProdFieldsResponse> InsertSampleProductField(InsertSampleProdFieldsRequest request);
        Task<UpdateSampleProdFieldsResponse> UpdateSampleProductField(UpdateSampleProdFieldsRequest request);
        Task<GetBySampleProdFieldsResponse> GetBySampleProdOptionalFieldID(int SampleProdOptionalFieldID);
        Task<UpdateSampleProdFieldsResponse> DeleteBySampleProdOptionalFieldID(int SampleProdOptionalFieldID);
    }
}