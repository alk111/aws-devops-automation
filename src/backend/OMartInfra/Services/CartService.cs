using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MySqlX.XDevAPI.Common;
using OMartApplication.Repositories;
using OMartApplication.Services;
using OMartApplication.Services.Wrapper;
using OMartDomain.Models.Cart.RequestAndresponse;
using OMartDomain.Models.UserCart;
using OMartDomain.Models.Wrapper;


namespace OMartInfra.Services
{
    //CartService

    public class CartService : ICartService
    {
        private readonly ICartRepository updateCartProductquantityRepository;

        public CartService(ICartRepository updateCartProductquantityRepository)
        {
            this.updateCartProductquantityRepository = updateCartProductquantityRepository;
        }


        public async Task<IResult<CartResponse>> AddToCart(AddCartRequest addCartRequest)
        {
            var result = await updateCartProductquantityRepository.AddToCart(addCartRequest);
            return Result<CartResponse>.Success(result);
        }

        public async Task<IResult<GetCartDataResponse>> GetCartData(string user_id)
        {
            var result = await updateCartProductquantityRepository.GetCartData(user_id);

            var url = "api/Files/download/";
            if (result != null && result.carts.Any())
            {

                foreach (var image in result.carts)
                {
                    if (image != null)
                    {
                        image.ImageName = url + image.ImageName;
                    }
                }
            }
            return result.carts.Count > 0 ? Result<GetCartDataResponse>.Success(result) : Result<GetCartDataResponse>.Fail("the list of empty...");
        }
        public async Task<IResult<UserCartResponse>> updateCartproductQuantity(AddCartRequest updateCartRequest)
        {
            UserCartResponse updateCartproductQuantity = await updateCartProductquantityRepository.updateCartproductQuantity(updateCartRequest);
            return updateCartproductQuantity.message.Contains("Successfully") ? Result<UserCartResponse>.Success(updateCartproductQuantity) :
            Result<UserCartResponse>.Fail(updateCartproductQuantity.message);
        }


        public async Task<IResult<UserCartResponse>> removeFromCart(RemoveCart removeCart)
        {
            var removeFromCart = await updateCartProductquantityRepository.removeFromCart(removeCart);
            return removeFromCart.message.Contains("Successfully") ? Result<UserCartResponse>.Success(removeFromCart) :
             Result<UserCartResponse>.Fail(removeFromCart.message);
        }

         public async  Task<IResult<RemoveFromCartByCartIdResponse>>  RemoveFromCartByCartId(int CartId)
        {
            var removeFromCartByCartId = await updateCartProductquantityRepository.RemoveFromCartByCartId(CartId);
            return removeFromCartByCartId.message.Contains("Successfully") ? Result<RemoveFromCartByCartIdResponse>.Success(removeFromCartByCartId) :
             Result<RemoveFromCartByCartIdResponse>.Fail(removeFromCartByCartId.message);
        }
    }
}