using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMartDomain.Models.ProductModels.RequestAndResponse;

namespace OMartApplication.Services
{
    public interface IProductModelsService
    {
         Task<AddProductModelResponse> AddProductModel(AddProductModelRequest request);
          Task<AddProductModelResponse> UpdateProductModel(UpdateProductModelsRequest request);
          Task<GetProductModelsByProductIDResponse> GetProductModelsByProductID(string ProductID);
          Task<AddProductModelResponse> DeleteProductModel(int ProductModelsID);
    }
}