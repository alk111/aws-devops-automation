using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using MySqlX.XDevAPI.Common;
using OMartApplication.Repositories;
using OMartApplication.Services;
using OMartApplication.Services.Wrapper;
using OMartDomain.Models.Products;
using OMartDomain.Models.Products.RequestAndResponce;
using OMartDomain.Models.Wrapper;

namespace OMartInfra.Services
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository iProductRepository;

        public ProductService(IProductRepository _iProductRepository)
        {
            iProductRepository = _iProductRepository;
        }

        public async Task<IResult<SearchResponse>> SearchProductByName(SearchRequest searchRequest)
        {
            try
            {
                var searchResult = await iProductRepository.SearchProductByName(searchRequest.search);
                var url = "api/Files/download/";
                if (searchResult != null && searchResult.products.Any())
                {

                    foreach (var image in searchResult.products)
                    {
                        if (image != null)
                        {
                            image.ImageName = url + image.ImageName;
                        }
                    }
                }
                     return searchResult.products.Capacity > 0 ?
                Result<SearchResponse>.Success(searchResult, "Products with name found")
                :
                Result<SearchResponse>.Fail("Product with name did not found");
            }
            catch (Exception ex)
            {
                throw new Exception($"Some error occur while seraching the product in product service{ex.Message}");
            }
        }

        public async Task<IResult<ProductCategoriesResponce>> GetAllCategories()
        {
            try
            {
                var result = await iProductRepository.GetAllCategories();
                return result.categories.Count > 0
                ?
                Result<ProductCategoriesResponce>.Success(result, "Products all Categories")
                :
                Result<ProductCategoriesResponce>.Fail("Something went Wrong And could not get all the categories");
            }
            catch (Exception ex)
            {
                throw new Exception($"Some error occur while seraching the product category in product service{ex.Message}");
            }
        }

      public async  Task<GetDeliveryTimeSlotsByEntityIDResponse> GetDeliveryTimeSlotsByEntityID(string EnityID)
        {
            return await iProductRepository.GetDeliveryTimeSlotsByEntityID(EnityID);   
        }
        public async Task<IResult<GetProductDetailsResponse>> getProductDeatils(string product_id)
        {
            try
            {
                var productDetails = await iProductRepository.getProductDeatils(product_id);

                return productDetails != null
                ?
                Result<GetProductDetailsResponse>.Success(productDetails, "Product with the product id found")
                :
                Result<GetProductDetailsResponse>.Fail("Product with id could not be found");
            }
            catch (Exception ex)
            {
                throw new Exception($"Some error occur while seraching the product details in product service{ex.Message}");
            }
        }

        public async Task<IResult<ProductListByCategoryResponce>> GetProductOfCategory(string category)
        {
            try
            {
                var result = await iProductRepository.GetProductOfCategory(category);
                return result.products.Count > 0
                ?
                Result<ProductListByCategoryResponce>.Success(result, "Product list with the given category found")
                :
                Result<ProductListByCategoryResponce>.Fail("Product list with given category not found");
            }
            catch (Exception ex)
            {
                throw new Exception($"Some error occur while seraching the product List by the category in product service{ex.Message}");
            }
        }
        public async Task<IResult<ProductListBySearchFilterResponce>> GetProductByFilters(ProductListBySearchFilterRequest productListBySearchFilterRequest)
        {
            try
            {
                var result = await iProductRepository.GetProductByFilters(productListBySearchFilterRequest.categories, productListBySearchFilterRequest.searchText);
                return result.productDetails.Count > 0
                ?
                Result<ProductListBySearchFilterResponce>.Success(result, "Product with given search filter found")
                :
                Result<ProductListBySearchFilterResponce>.Fail("Product with given search filter not found");
            }
            catch (Exception ex)
            {
                throw new Exception($"Some error occur while seraching the product List by the search filter in product service{ex.Message}");
            }

        }
        
        public async Task<string> AddProductImagesAsync(AddProductImage request)
        {
            try
            {
                var result = await iProductRepository.AddMultipleProductImagesAsync(request);
                return result;
              
            }
            catch (Exception ex)
            {
                throw new Exception($"Some error occur while seraching the product List by the search filter in product service{ex.Message}");
            }

        }

        
        public async Task<IResult<GetProductImagesResponse>> GetImagesByProductIdAsync(GetProductImagesRequest request)
        {
            try
            {
                var result = await iProductRepository.GetImagesByProductIdAsync(request);
                var url = "api/Files/download/";
                if (result != null && result.ProductImages.Any())
                {

                    foreach (var image in result.ProductImages)
                    {
                        image.ImageURL = url + image.ImageName;
                    }

                    return Result<GetProductImagesResponse>.Success(result);
                }
                else
                    return Result<GetProductImagesResponse>.Fail(result,"No data Found");
               


            }
            catch (Exception ex)
            {
                throw new Exception($"Some error occur while seraching the product List by the category in product service{ex.Message}");
            }
        }

    }
}