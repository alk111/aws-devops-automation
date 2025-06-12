using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMartApplication.Services;
using OMartDomain.Models.UserCart;

namespace OMAPI.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class UserCartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public UserCartController(ICartService addCartService)
        {
            this._cartService = addCartService;
        }

        [HttpPost("AddToCart")]
        public async Task<ActionResult> AddToCartAsync(AddCartRequest addCartRequest)
        {
            var response = await _cartService.AddToCart(addCartRequest);
            return Ok(response);
        }



        [HttpGet("GetCartData/{user_id}")]
        public async Task<ActionResult> GetCartData(string user_id)
        {
            var response = await _cartService.GetCartData(user_id);
            return Ok(response);
        }



        [HttpPut("updateProductQuantity")]
        public async Task<ActionResult> UpdateProductquantity(AddCartRequest updateCartRequest)
        {
            var response = await _cartService.updateCartproductQuantity(updateCartRequest);
            return response.Succeeded ? Ok(response) : Conflict(response);
        }


        [HttpDelete("removeFromCart")]
        public async Task<ActionResult> removeFromCart(RemoveCart removeCart)
        {
            var response = await _cartService.removeFromCart(removeCart);
            return response.Succeeded ? Ok(response) : Conflict(response);
        }

        [HttpDelete("removeFromCartByCartId/{CartId}")]
        public async Task<ActionResult> RemoveFromCartByCartId(int CartId)
        {
            var response = await _cartService.RemoveFromCartByCartId(CartId);
            return response.Succeeded ? Ok(response) : Conflict(response);
        }
    }
}