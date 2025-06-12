using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OMartApplication.Repositories;
using OMartApplication.Services.Wrapper;
using OMartDomain.Common;
using OMartDomain.Models.Account;
using OMartDomain.Models.Products;
using OMartDomain.Models.Seller;
using OMartDomain.Models.Seller.RequestAndResponce;
using OMartDomain.Models.User;
using OMartDomain.Models.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMartInfra.Repositories
{
    internal class SellerRepository : CentralRepository, ISellerRepository
    {
        private readonly string? _connectionString;

        public SellerRepository(IConfiguration configuration) : base(configuration, "OMartDevDbConnection")
        {
            _connectionString = configuration.GetConnectionString(name: "OMartDevDbConnection");

        }
        public async Task<GetProductListbySellerResponse> GetAllProductsbySellersAsync(GetProductListbySellerRequest request)
        {
            var parameters = new
            {
                EntityID = request.EntityID,
            };
            var result = await ExecuteQueryListAsync<ProductDetails>(SPConstant.GetAllProductsbySellers, parameters);
            GetProductListbySellerResponse getProductListbySellerResponse = new GetProductListbySellerResponse { Products = result };

            return getProductListbySellerResponse;
        }

        public async Task<AddProductResponse> AddProduct(AddProductRequest request)
        {
            //try{
            //var query = "CALL UpdateProduct(@entity_id, @product_id, @iterations, @category, @details, @price, @stock_quantity, @currency_mode, @estimated_delivery_time, @can_be_returned, @estimated_return_pickup_time, @can_be_replaced, @estimated_replacement_time, @added_on, @updated_on, @is_deleted, @productName)";
            var parameters = new
            {
                entity_id = request.entity_id,
                //product_id = request.product_id,
                iterations = request.iterations,
                category = request.category,
                details = request.details,
                price = request.price,
                stock_quantity = request.stock_quantity,
                currency_mode = request.currency_mode,
                estimated_delivery_time = request.estimated_delivery_time,
                can_be_returned = request.can_be_returned,
                estimated_return_pickup_time = request.estimated_return_pickup_time,
                can_be_replaced = request.can_be_replaced,
                estimated_replacement_time = request.estimated_replacement_time,
                added_on = request.added_on,
                updated_on = request.updated_on,
                isSubscribable = request.isSubscribable,
                productName = request.productName,
                UnitofMeasure = request.UnitofMeasure,
                UnitofQuantity = request.UnitofQuantity,

            };

            var result = await ExecuteQueryAsync<dynamic>(SPConstant.InsertProduct, parameters);
            AddProductResponse addproduct = new AddProductResponse { product_id = result.product_id };

            return addproduct;
            //}
            // catch(Exception ex)
            //         {
            //           throw new Exception($"Exception while Connecting Database.... {ex.Message}");
            //         } 
        }

        public async Task<AddProductResponse> UpdateProduct(UpdateProductRequest request)
        {
            try
            {

                var parameters = new
                {
                    entityId = request.entity_id,
                    productId = request.product_id,
                    iterations = request.iterations,
                    category = request.category,
                    details = request.details,
                    price = request.price,
                    stock_quantity = request.stock_quantity,
                    currency_mode = request.currency_mode,
                    estimated_delivery_time = request.estimated_delivery_time,
                    can_be_returned = request.can_be_returned,
                    estimated_return_pickup_time = request.estimated_return_pickup_time,
                    can_be_replaced = request.can_be_replaced,
                    estimated_replacement_time = request.estimated_replacement_time,
                    //  added_on = request.added_on,
                    //  updated_on = request.updated_on,
                    isSubscribable = request.isSubscribable,
                    productName = request.productName,
                    UnitofMeasure = request.UnitofMeasure,
                    UnitofQuantity = request.UnitofQuantity,

                };
                var result = await ExecuteQueryAsync<string>(SPConstant.EditProduct, parameters);
                AddProductResponse EditProduct = new AddProductResponse { product_id = result };
                return EditProduct;
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception while Connecting Database.... {ex.Message}");
            }

        }



        //Seller Creation 

        public async Task<SellerRegistrationResponce> createSellerWithUserDetailsAsync(UserDetailsResponce userDetails, EstablishmentDetails establishmentDetails)
        {
            try
            {
                // Create an instance of EstablishmentDetails and map the data
                //EstablishmentDetails establishmentDetails = new EstablishmentDetails
                //{
                establishmentDetails.user_id = userDetails.user_id.ToString();
                establishmentDetails.contact_no_1 = userDetails.contact_phone_number;
                establishmentDetails.contact_email_1 = userDetails.contact_email;
                establishmentDetails.is_active = userDetails.is_active;
                //};

                // Define parameters for inserting into EstablishmentDetails
                var establishmentParams = new
                {
                    p_user_id = establishmentDetails.user_id,
                    p_entity_id = establishmentDetails.entity_id,
                    p_establishment_type = establishmentDetails.establishment_type,
                    p_establishment_name = establishmentDetails.establishment_name,
                    p_address = establishmentDetails.address,
                    p_contact_no_1 = establishmentDetails.contact_no_1,
                    p_contact_no_2 = establishmentDetails.contact_no_2,
                    p_contact_email_1 = establishmentDetails.contact_email_1,
                    p_contact_email_2 = establishmentDetails.contact_email_2,
                    p_contact_1_2fa_verified = establishmentDetails.contact_1_2fa_verified,
                    p_contact_2_2fa_verified = establishmentDetails.contact_2_2fa_verified,
                    p_registered_on = establishmentDetails.registered_on,
                    p_updated_on = establishmentDetails.updated_on,

                };


                var result = await ExecuteQueryAsync<int>(SPConstant.InsertEntityDetails, establishmentParams);

                if (result > 0)
                {
                    return new SellerRegistrationResponce { message = "Seller is created successfully", sellerId = establishmentDetails.entity_id.ToString() };
                }
                else
                {
                    return new SellerRegistrationResponce { message = "Somthing went wrong could not create seller", sellerId = null };
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occur while creating seller in seller repository : {ex.Message}");
            }
        }

        //for existing Seller with the particular userId
        public async Task<bool> checkUserIdExistInEntityDetailsTable(string userId)
        {
            try
            {
                var parameters = new
                {
                    p_user_id = userId
                };

                var result = await ExecuteQueryAsync<int>(SPConstant.CheckUserIdExistInEntityDetails, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Something went wrong while checking the existing user {ex.Message}");
            }
        }

        public async Task<UserDetailsResponce> getUserDetailsByUserId(string userId)
        {
            try
            {
                var parameters = new
                {
                    p_user_id = userId
                };

                // Fetch user details using stored procedure
                UserDetailsResponce userDetailData = await ExecuteQueryAsync<UserDetailsResponce>(SPConstant.GetUserDetailsByUserId, parameters);

                // Check if the result is null or empty
                if (userDetailData == null)
                {
                    throw new Exception("User details not found.");
                }

                return userDetailData;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while getting user details: {ex.Message}", ex);
            }
        }

        public async Task<string> getUserIdByEmail(string email)
        {
            try
            {
                var parameters = new
                {
                    userEmail = email
                };

                string userId = await ExecuteQueryAsync<string>(SPConstant.GetUserIdByEmail, parameters);

                return userId;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occur while getting user id by user email in seller repository {ex.Message}");
            }
        }

        public async Task<GetSellerDetailsResponce> getEntityDetailsBySellerId(string sellerId)
        {

            try
            {

                var parameters = new
                {
                    sellerId = sellerId
                };

                GetSellerDetailsResponce getSellerDetailsResponce = await ExecuteQueryAsync<GetSellerDetailsResponce>(SPConstant.GetSellerDetailsBySellerId, parameters);

                return getSellerDetailsResponce == null ? null : getSellerDetailsResponce;

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occur while getting seller details in seller repository {ex.Message}");
            }
        }
        public async Task<bool> updateSellerDetailsBySellerId(UpdateSellerDetails updateSellerDetails)
        {

            try
            {

                var parameters = new
                {
                    sellerId = updateSellerDetails.seller_Id,
                    p_address = updateSellerDetails.address,
                    p_contact_no_2 = updateSellerDetails.contact_no_2,
                    p_contact_email_2 = updateSellerDetails.contact_email_2,
                    p_establishment_name = updateSellerDetails.establishment_name,
                    p_establishment_type = updateSellerDetails.establishment_type,
                    p_gmaps_location = updateSellerDetails.gmaps_location
                };

                int row = await ExecuteQueryAsync<int>(SPConstant.UpdateSellerDetailsBySellerId, parameters);

                return row > 0 ? true : false;

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occur while Update seller details in seller repository {ex.Message}");
            }

        }

    }
}
