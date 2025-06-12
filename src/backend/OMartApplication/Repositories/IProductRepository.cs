using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMartApplication.Services.Wrapper;
using OMartDomain.Models.Products.RequestAndResponce;

namespace OMartApplication.Repositories
{
    public interface IProductRepository
    {
        Task<SearchResponse> SearchProductByName(string search);

         Task<GetDeliveryTimeSlotsByEntityIDResponse> GetDeliveryTimeSlotsByEntityID(string EnityID);
        Task<ProductCategoriesResponce> GetAllCategories();
        Task<GetProductDetailsResponse> getProductDeatils(string product_id);
        Task<ProductListByCategoryResponce> GetProductOfCategory(string category);
        Task<ProductListBySearchFilterResponce> GetProductByFilters(string categories, string searchText);
        Task<string> AddMultipleProductImagesAsync(AddProductImage request);
        Task<GetProductImagesResponse> GetImagesByProductIdAsync(GetProductImagesRequest request);
    }
}