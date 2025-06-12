using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMartApplication.Services;
using OMartDomain.Models.ProductModels.RequestAndResponse;

namespace OMAPI.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductModelsController : ControllerBase
    {
        private readonly IProductModelsService productModelsService;
        public ProductModelsController(IProductModelsService productModelsService)
        {
            this.productModelsService=productModelsService;
        }

        [HttpPost("AddProductModel")]
        public async Task<AddProductModelResponse> AddProductModel(AddProductModelRequest request)
        {
           return await productModelsService.AddProductModel(request);
        }

        [HttpPut("UpdateProductModel")]
        public async  Task<AddProductModelResponse> UpdateProductModel(UpdateProductModelsRequest request)
        {
            return await productModelsService.UpdateProductModel(request);
        }

        [HttpGet("GetProductModelsByProductID/{ProductID}")] 
        public async  Task<GetProductModelsByProductIDResponse> GetProductModelsByProductID(string ProductID)
        {
            return await productModelsService.GetProductModelsByProductID(ProductID);
        }

        [HttpDelete("DeleteProductModel")]
        public async   Task<AddProductModelResponse> DeleteProductModel(int ProductModelsID)
        {
          return await productModelsService.DeleteProductModel(ProductModelsID);
        }
    }
}