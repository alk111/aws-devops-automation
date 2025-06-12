using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMartApplication.Repositories;
using OMartApplication.Services;
using OMartDomain.Models.SampleProdFields.ProductFieldsRequestAndResponse;

namespace OMartInfra.Services
{
    public class SampleProdFieldsService : ISampleProdFieldsService
    {
        private readonly ISampleProdFieldsRepository sampleProdFieldsRepository;
        public SampleProdFieldsService(ISampleProdFieldsRepository sampleProdFieldsRepository)
        {
            this.sampleProdFieldsRepository = sampleProdFieldsRepository;
        }

        public async Task<InsertSampleProdFieldsResponse> InsertSampleProductField(InsertSampleProdFieldsRequest request)
        {
            return await sampleProdFieldsRepository.InsertSampleProductField(request);
        }

        public async Task<UpdateSampleProdFieldsResponse> UpdateSampleProductField(UpdateSampleProdFieldsRequest request)
        {
            return await sampleProdFieldsRepository.UpdateSampleProductField(request);
        }

        public async Task<GetBySampleProdFieldsResponse> GetBySampleProdOptionalFieldID(int SampleProdOptionalFieldID)
        {
            return await sampleProdFieldsRepository.GetBySampleProdOptionalFieldID(SampleProdOptionalFieldID);
        }

        public async Task<UpdateSampleProdFieldsResponse> DeleteBySampleProdOptionalFieldID(int SampleProdOptionalFieldID)
        {
            return await sampleProdFieldsRepository.DeleteBySampleProdOptionalFieldID(SampleProdOptionalFieldID);
        }
    }
}