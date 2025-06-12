using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OMartApplication.Repositories;
using OMartDomain.Common;
using OMartDomain.Models.Cart;
using OMartDomain.Models.Cart.RequestAndresponse;
using OMartDomain.Models.Products;
using OMartDomain.Models.UserCart;

namespace OMartInfra.Repositories
{
    public class CartRepositroy : CentralRepository, ICartRepository
    {
        private readonly string? _connectionString;

        public CartRepositroy(IConfiguration configuration) : base(configuration, "OMartDevDbConnection")
        {
            _connectionString = configuration.GetConnectionString(name: "OMartDevDbConnection");
        }

        public async Task<CartResponse> AddToCart(AddCartRequest addCartRequest)
        {

            var parameteresforproductdetails = new
            {
                input_product_id = addCartRequest.product_id,
            };

            //var productDetails = await ExecuteQueryAsync<ProductDetails>(SPConstant.GetProductDetailsById, parameteresforproductdetails);
            //var total_price = Convert.ToDouble(productDetails.Price) * addCartRequest.quantity;


            var parameters = new
            {
                p_user_id = addCartRequest.user_id,
             //   p_total_price = total_price,
                p_quantity = addCartRequest.quantity,
                p_product_id = addCartRequest.product_id,
                //p_added_on = DateTime.UtcNow,
               //  p_entity_id=addCartRequest.entity_id,
            };

            var result = await ExecuteQueryAsync<int>(SPConstant.AddCart, parameters);
            if (!(result > 0))
            {
                throw new InvalidOperationException("Not Added");
            }

            CartResponse addCart = new CartResponse()
            {
                NumberofdataPresentInTheCart = result
            };
            return addCart;
        }

        public async Task<GetCartDataResponse> GetCartData(string user_id)
        {
            var parameters = new
            {
                input_user_id = user_id,
            };

            List<UserCarts> result = await ExecuteQueryListAsync<UserCarts>(SPConstant.GetCartData, parameters);
            if (result.IsNullOrEmpty())
            {
                return new GetCartDataResponse { carts = new List<UserCarts>() };
            }
            GetCartDataResponse getCartData = new GetCartDataResponse
            {
                carts = result
            };
            return getCartData;
        }
        public async Task<UserCartResponse> updateCartproductQuantity(AddCartRequest updateCartRequest)
        {

            try
            {
                var parameters = new
                {
                    p_user_id = updateCartRequest.user_id,
                    P_quantity = updateCartRequest.quantity,
                    p_product_id = updateCartRequest.product_id

                };
                //Console.WriteLine(parameters);
                var result = await ExecuteQueryAsync<int>(SPConstant.updateCartproductQuantity, parameters);

                return result > 0 ? new UserCartResponse { message = "Updated Successfully" } : new UserCartResponse { message = "Not updated" };

            }
            catch (Exception ex)
            {
                throw new Exception($"Order update failed: {ex.Message}");
            }
        }


        public async Task<UserCartResponse> removeFromCart(RemoveCart removeCart)
        {
            try
            {

                var parameters = new
                {
                    p_user_id = removeCart.user_id,
                    p_product_id = removeCart.product_id,
                    //p_entity_id=removeCart.entity_id,

                };

                var result = await ExecuteQueryAsync<int>(SPConstant.RemovefromcartByUserIdNadproductId, parameters);
                return result > 0 ? new UserCartResponse { message = "Removed Successfully" } : new UserCartResponse { message = "Not removed" };
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed: {ex.Message}");
            }

        }
//removefromcartByCartId
         public async  Task<RemoveFromCartByCartIdResponse> RemoveFromCartByCartId(int CartId)
            {
                try {
                    var parameters = new
                    {
                        p_cartId = CartId,
                    };
                    var result= await ExecuteQueryListAsync<int>(SPConstant.removefromcartByCartId, parameters);
                    if(result==null)
                    {
                        return new RemoveFromCartByCartIdResponse{ message = "Not Found.." };
                    }
                    return new RemoveFromCartByCartIdResponse { message="deleted.."};
                }
                catch(Exception ex)
                {
                   throw new Exception($"Failed: {ex.Message}");
                }
            }
    }
}