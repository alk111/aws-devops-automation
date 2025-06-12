using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMartApplication.Repositories;
using OMartApplication.Services;
using OMartDomain.Models.ProductModels.RequestAndResponse;

namespace OMartInfra.Services
{
    public class ProductModelsService :IProductModelsService
    {
        private readonly IAddProductModelRepository addProductModelRepository;
        public ProductModelsService(IAddProductModelRepository addProductModelRepository)
        {
            this.addProductModelRepository = addProductModelRepository;
        }
        public async Task<AddProductModelResponse> AddProductModel(AddProductModelRequest request)
        {
            return await addProductModelRepository.AddProductModel(request);
        }

        public async  Task<AddProductModelResponse> UpdateProductModel(UpdateProductModelsRequest request)
        {
            return await addProductModelRepository.UpdateProductModel(request);
        }

        public async   Task<GetProductModelsByProductIDResponse> GetProductModelsByProductID(string ProductID)
        {
            return await addProductModelRepository.GetProductModelsByProductID(ProductID);
        }

        public async Task<AddProductModelResponse> DeleteProductModel(int ProductModelsID)
        {
            return await addProductModelRepository.DeleteProductModel(ProductModelsID);
        }
    }
}