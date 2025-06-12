using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMartDomain.Models.Order;
using OMartDomain.Models.Order.RequestAndResponse;

namespace OMartApplication.Services
{
    public interface IOrderService
    {
            Task<AddOrderResponse> AddOrder(AddMultipleOrderRequest request);
            //Task<GetOrderbyUserResponse> GetOrderbyUser(GetOrderbyUserRequest request);  
            //Task<InserOrderListResponse> InserOrderList(InserOrderListRequest request);  
            Task<GetOrderListResponse> GetOrderList(GetOrderListByOrderIdRequest request);
            Task<GetOrderListforBuyerResponse> GetOrderItemsforBuyer(GetOrderListByOrderIdRequest request);
            Task<InserOrderListResponse> UpdateOrderList(UpdateOrderListRequest request);
           
            Task<GetOrderbySellerIDResponse> GetOrderbySellerID( GetOrderbySellerIDRequest request);
            Task<GetOrderwithItemsbyUserIDResponse> GetOrderwithItemsbyUserID(GetOrderwithItemsbyUserIDRequest request);
    }
}