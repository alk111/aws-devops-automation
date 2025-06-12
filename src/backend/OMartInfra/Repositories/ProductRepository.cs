using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OMartApplication.Repositories;
using OMartApplication.Services.Wrapper;
using OMartDomain.Common;
using OMartDomain.Models.Categorie;
using OMartDomain.Models.Products;
using OMartDomain.Models.Products.RequestAndResponce;
using OMartDomain.Models.Wrapper;

namespace OMartInfra.Repositories
{
    public class ProductRepository : CentralRepository, IProductRepository
    {

        private readonly string? _connectionString;

        public ProductRepository(IConfiguration configuration) : base(configuration, "OMartDevDbConnection")
        {
            _connectionString = configuration.GetConnectionString(name: "OMartDevDbConnection");

        }

        public async Task<SearchResponse> SearchProductByName(string search)
        {
            try
            {

                var parameters = new
                {
                    input_product_name = search
                };

                List<ProductDetails> searchResult = await ExecuteQueryListAsync<ProductDetails>(SPConstant.SearchProductsByName, parameters);

                return new SearchResponse() { products = searchResult };

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while Searching product: {ex.Message}");
            }
        }

        public async Task<ProductCategoriesResponce> GetAllCategories()
        {
            try
            {
                List<Category> allCategories = await ExecuteQueryListAsync<Category>(SPConstant.GetAllProductCategories, null);

                return new ProductCategoriesResponce() { categories = allCategories };
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while getting all categories of products: {ex.Message}");
            }
        }

 public async  Task<GetDeliveryTimeSlotsByEntityIDResponse> GetDeliveryTimeSlotsByEntityID(string EnityID)
                { 
                    try{
                    var parameters = new
                    { 
                      p_EnityID=EnityID
                    };

                    var result = await ExecuteQueryListAsync<EnDelTimeSlot>(SPConstant.GetDeliveryTimeSlotsByEntityID,parameters);
                    if (result==null)
                    {
                        return new GetDeliveryTimeSlotsByEntityIDResponse{ message = "EntityId is not present in db",enDelTimeSlots=null };
                    }
                    return new GetDeliveryTimeSlotsByEntityIDResponse{message = "Successfully Get Data",enDelTimeSlots=result};
                    }
                    catch (Exception ex)
                    {
                      throw new Exception($"Exception while connecting to the database: {ex.Message}", ex);
                    }
                }


        public async Task<GetProductDetailsResponse> getProductDeatils(string productId)
        {
            try
            {
                var parameters = new
                {
                    input_product_id = productId
                };

                var productDetails = await ExecuteQueryAsync<ProductDetails>(SPConstant.GetProductDetailsById, parameters);

                return new GetProductDetailsResponse { productDetails = productDetails };
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while getting details of a product in product repository: {ex.Message}");
            }
        }

        public async Task<ProductListByCategoryResponce> GetProductOfCategory(string category)
        {
            try
            {
                var parameters = new
                {
                    input_category = category
                };

                var result = await ExecuteQueryListAsync<ProductDetails>(SPConstant.GetProductsByCategory, parameters);

                return new ProductListByCategoryResponce() { products = result };
            }
            catch (Exception ex)
            {
                throw new Exception($"An Error occur while getting all the product of a certain category in product repository: {ex.Message} ");
            }
        }

        public async Task<ProductListBySearchFilterResponce> GetProductByFilters(string categories, string searchFilters)
        {
            try
            {
                var parameters = new
                {
                    p_category_type = categories,
                    p_search_text = searchFilters
                };

                var searchResult = await ExecuteQueryListAsync<ProductDetails>(SPConstant.SearchProductsFilter, parameters);

                return new ProductListBySearchFilterResponce() { productDetails = searchResult };

            }
            catch (Exception ex)
            {
                throw new Exception($"An Error occur while getting all the product by search filter in product repository: {ex.Message} ");
            }
        }

        ///
        public async Task<string> AddMultipleProductImagesAsync(AddProductImage request)
        {
            // Convert the list of image names to JSON format
            var imageDataJson = JsonSerializer.Serialize(request.ImageNames);

            try
            {
                var parameters = new
                {
                    p_ProductID = request.ProductID,
                    p_ImageData = imageDataJson
                };

                var Result = await ExecuteQueryAsync<string>(SPConstant.AddMultipleProductImages, parameters);

                return  Result;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message} ");
            }
          
        }

        public async Task<GetProductImagesResponse> GetImagesByProductIdAsync(GetProductImagesRequest request)
        {
            try
            {
                var parameters = new
                {
                    p_ProductID = request.ProductId
                };

                var result = await ExecuteQueryListAsync<Image>(SPConstant.GetProductImages, parameters);

                GetProductImagesResponse getProductImagesResponse = new GetProductImagesResponse { ProductImages = result};
                return getProductImagesResponse ;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while getting details of a product in product repository: {ex.Message}");
            }
        }
    }
}