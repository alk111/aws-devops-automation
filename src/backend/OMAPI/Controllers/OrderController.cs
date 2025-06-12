using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMartApplication.Services;
using OMartDomain.Models.Order;
using OMartDomain.Models.Order.RequestAndResponse;

namespace OMAPI.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            this._orderService = orderService;
        }


        [HttpPost("AddOrder")]
        public async Task<ActionResult> AddOrder(AddMultipleOrderRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid order request.");
            }

            try
            {
                var response = await _orderService.AddOrder(request);
                if (response == null)
                {
                    return NotFound("Order could not be added.");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception (if you have logging configured)
                return StatusCode(500, $"An error occurred while processing your request.{ex.Message}");
            }
        }

        //[HttpPost("InserOrderList")]
        //public async  Task<InserOrderListResponse> InserOrderList(InserOrderListRequest request)
        //{
        //   return await _addOrderService.InserOrderList(request);
        //}
        // [HttpPost("GetOrderbyUser")]
        // public async Task<GetOrderbyUserResponse> GetOrderbyUser(GetOrderbyUserRequest request)
        // {
        //     return await _addOrderService.GetOrderbyUser(request);
        // }
        // [HttpPost("GetOrderbySeller")]
        // public async Task<GetOrderbyUserResponse> GetOrderbySeller(GetOrderbySellerRequest request)
        // {
        //     return await _addOrderService.GetOrderbySeller(request);
        // }
        [HttpPost("GetOrderList")]
        public async Task<GetOrderListResponse> GetOrderList(GetOrderListByOrderIdRequest request)
        {
            return await _orderService.GetOrderList(request);
        }
        [HttpPost("GetOrderItemforBuyer")]
        public async Task<GetOrderListforBuyerResponse> GetOrderItemsforBuyer(GetOrderListByOrderIdRequest request)
        {
            return await _orderService.GetOrderItemsforBuyer(request);
        }
        [HttpPut("UpdateOrderList")]
        public async Task<InserOrderListResponse> UpdateOrderList(UpdateOrderListRequest request)
        {
            return await _orderService.UpdateOrderList(request);
        }

        [HttpPost("GetOrderwithItemsbyUserID")]
        public async Task<GetOrderwithItemsbyUserIDResponse> GetOrderwithItemsbyUserID(GetOrderwithItemsbyUserIDRequest request)
        {
            return await _orderService.GetOrderwithItemsbyUserID(request);
        }

        [HttpPost("GetOrderbySellerID")]
        public async  Task<GetOrderbySellerIDResponse> GetOrderbySellerID( GetOrderbySellerIDRequest request)
        {
            return await _orderService.GetOrderbySellerID(request);
        }
    }
}