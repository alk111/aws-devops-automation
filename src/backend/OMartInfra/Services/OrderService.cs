using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMartApplication.Repositories;
using OMartApplication.Services;
using OMartDomain.Models.Order;
using OMartDomain.Models.Order.RequestAndResponse;

namespace OMartInfra.Services
{
    public class OrderService :IOrderService
    {
         private readonly IOrderRepository addOrderRepository;

        public OrderService(IOrderRepository addOrderRepository)
        {
            this.addOrderRepository=addOrderRepository;
        }    

        public async  Task<AddOrderResponse> AddOrder(AddMultipleOrderRequest request)
        {
               var addorder= await  addOrderRepository.AddOrder(request);
               return addorder;
        }

       
        // public async Task<GetOrderbyUserResponse> GetOrderbyUser(GetOrderbyUserRequest request)
        // {
        //     return await addOrderRepository.GetOrderList(request); 
        // }

        //public async  Task<InserOrderListResponse> InserOrderList(InserOrderListRequest request)
        //{
        //   return await addOrderRepository.InserOrderList(request);
        //}

       
        public async Task<GetOrderListResponse> GetOrderList(GetOrderListByOrderIdRequest request)
          {
            return await addOrderRepository.GetOrderList(request); 
          }
        public async Task<GetOrderListforBuyerResponse> GetOrderItemsforBuyer(GetOrderListByOrderIdRequest request)
        {
            return await addOrderRepository.GetOrderItemsforBuyer(request);
        }

        public async Task<InserOrderListResponse> UpdateOrderList(UpdateOrderListRequest request)
         {
            return await addOrderRepository.UpdateOrderList(request);
         }
//GetOrderwithItemsbyOrderID
         public async Task<GetOrderwithItemsbyUserIDResponse> GetOrderwithItemsbyUserID(GetOrderwithItemsbyUserIDRequest request)
         {
            var result= await addOrderRepository.GetOrderwithItemsbyUserId(request);
            if(result!=null)
            {
               return new GetOrderwithItemsbyUserIDResponse{message=" success", orders=result};
            }
           return new GetOrderwithItemsbyUserIDResponse{message=" fail", orders=null};
         }

         public async  Task<GetOrderbySellerIDResponse> GetOrderbySellerID( GetOrderbySellerIDRequest request)
          {
               return await addOrderRepository.GetOrderbySellerID(request);
          }
    }
}