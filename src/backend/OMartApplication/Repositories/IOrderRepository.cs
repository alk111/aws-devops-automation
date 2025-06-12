using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMartDomain.Models.BookMark.requestAndResponse;
using OMartDomain.Models.Order;
using OMartDomain.Models.Order.RequestAndResponse;

namespace OMartApplication.Repositories
{
    public interface IOrderRepository
    {
           Task<AddOrderResponse> AddOrder(AddMultipleOrderRequest request);
           Task<InserOrderListResponse> InserOrderList(InserOrderListRequest request);
          Task<GetOrderListResponse> GetOrderList(GetOrderListByOrderIdRequest request);
          Task<GetOrderListforBuyerResponse> GetOrderItemsforBuyer(GetOrderListByOrderIdRequest request);
           Task<InserOrderListResponse> UpdateOrderList(UpdateOrderListRequest request);

         
           Task<GetOrderbySellerIDResponse> GetOrderbySellerID( GetOrderbySellerIDRequest request);
           Task<List<GetOrderResponse>> GetOrderwithItemsbyUserId(GetOrderwithItemsbyUserIDRequest request);

    }
}