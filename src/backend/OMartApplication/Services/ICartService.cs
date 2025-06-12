using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMartApplication.Services.Wrapper;
using OMartDomain.Models.Cart.RequestAndresponse;
using OMartDomain.Models.UserCart;

namespace OMartApplication.Services
{
    public interface ICartService
    {
        Task<IResult<CartResponse>> AddToCart(AddCartRequest addCartRequest);
        Task<IResult<GetCartDataResponse>> GetCartData( string user_id);
        Task<IResult< UserCartResponse>> updateCartproductQuantity(AddCartRequest updateCartRequest);
        Task<IResult<UserCartResponse>> removeFromCart(RemoveCart removeCart);
         Task<IResult<RemoveFromCartByCartIdResponse>>  RemoveFromCartByCartId(int CartId);
    }
}