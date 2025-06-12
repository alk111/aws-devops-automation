using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMartApplication.Services.Wrapper;
using OMartDomain.Models.Products;
using OMartDomain.Models.Products.RequestAndResponce;

namespace OMartApplication.Services
{
    public interface IProductService
    {
        Task<IResult<SearchResponse>> SearchProductByName(SearchRequest searchRequest);
        Task<IResult<ProductCategoriesResponce>> GetAllCategories();

         Task<GetDeliveryTimeSlotsByEntityIDResponse> GetDeliveryTimeSlotsByEntityID(string EnityID);
        Task<IResult<GetProductDetailsResponse>> getProductDeatils(string product_id);
        Task<IResult<ProductListByCategoryResponce>> GetProductOfCategory(string category);
        Task<IResult<ProductListBySearchFilterResponce>> GetProductByFilters(ProductListBySearchFilterRequest productListBySearchFilterRequest);
        Task<string> AddProductImagesAsync(AddProductImage request);
        Task<IResult<GetProductImagesResponse>> GetImagesByProductIdAsync(GetProductImagesRequest request);
    }
}