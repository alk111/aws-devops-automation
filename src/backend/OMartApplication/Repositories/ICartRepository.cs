using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMartDomain.Models.Cart.RequestAndresponse;
using OMartDomain.Models.UserCart;

namespace OMartApplication.Repositories
{
   public interface ICartRepository  
    {
        Task<CartResponse> AddToCart(AddCartRequest addCartRequest);
        Task<GetCartDataResponse> GetCartData( string user_id);
         Task<UserCartResponse> updateCartproductQuantity(AddCartRequest updateCartRequest);
         Task<UserCartResponse> removeFromCart(RemoveCart removeCart);
         Task<RemoveFromCartByCartIdResponse> RemoveFromCartByCartId(int CartId);
    }
}