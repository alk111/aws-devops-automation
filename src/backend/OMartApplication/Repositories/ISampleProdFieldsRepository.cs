using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMartDomain.Models.BookMark.requestAndResponse;
using OMartDomain.Models.SampleProdFields.ProductFieldsRequestAndResponse;

namespace OMartApplication.Repositories
{
    public interface ISampleProdFieldsRepository
    {
        Task<InsertSampleProdFieldsResponse> InsertSampleProductField(InsertSampleProdFieldsRequest request);
        Task<UpdateSampleProdFieldsResponse> UpdateSampleProductField(UpdateSampleProdFieldsRequest request);
        Task<GetBySampleProdFieldsResponse> GetBySampleProdOptionalFieldID(int SampleProdOptionalFieldID);
        Task<UpdateSampleProdFieldsResponse> DeleteBySampleProdOptionalFieldID(int SampleProdOptionalFieldID);
    }
}