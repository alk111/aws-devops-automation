using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using OMartApplication.Repositories;
using OMartDomain.Common;
using OMartDomain.Models.BookMark.requestAndResponse;
using OMartDomain.Models.Order;
using OMartDomain.Models.Order.RequestAndResponse;
using Org.BouncyCastle.Asn1.X509;

namespace OMartInfra.Repositories
{
    public class OrderRepository : CentralRepository, IOrderRepository
    {
        private readonly string? _connectionString;

        public OrderRepository(IConfiguration configuration) : base(configuration, "OMartDevDbConnection")
        {
            _connectionString = configuration.GetConnectionString(name: "OMartDevDbConnection");
        }

        public async Task<AddOrderResponse> AddOrder(AddMultipleOrderRequest request)
        {

            try
            {

                string orders = JsonSerializer.Serialize(request.Orders);
                //string orderItemsJson = JsonSerializer.Serialize(request.OrderItems);
                //var parameters = new DynamicParameters();
                //parameters.AddDynamicParams(new
                ////var parameters= new 
                //{
                //    //p_order_id=request.order_id,
                //    p_user_id = request.user_id,
                //    p_entity_id=request.entity_id,
                //    //p_product_id = request.product_id,
                //    p_pickup_address = request.pickup_address,
                //    p_shipping_address = request.shipping_address,
                //    //p_added_on = request.added_on,
                //    //p_quantity = request.quantity,
                //    p_total_price = request.total_price,
                //    p_mode_of_payment = request.mode_of_payment,
                //    //p_alloted_delivery_date = request.alloted_delivery_date,
                //    //p_order_delivery_time = request.order_delivery_time,
                //    //p_is_order_delivered = request.is_order_delivered,
                //    //p_is_order_canceled = request.is_order_canceled,
                //    //p_order_cancellation_time = request.order_cancellation_time,
                //    //p_order_cancellation_reason = request.order_cancellation_reason,
                //    //p_is_return_requested = request.is_return_requested,
                //    //p_order_return_reason = request.order_return_reason,
                //    //p_retun_pickup_time = request.retun_pickup_time,
                //    //p_is_replacement_requested = request.is_replacement_requested,
                //    //p_replacement_time = request.replacement_time,
                //    //p_order_replacement_reason = request.order_replacement_reason


                //});
                //parameters.Add("orders", orders);
                var parameter = new
                { orders = orders};

                    var result = await ExecuteQueryListAsync<string>(SPConstant.AddMultipleOrders, parameter);
                //var result = await ExecuteQueryAsync<Order>(SPConstant.AddOrder, parameters);
                if (result == null)
                {
                    throw new Exception("The stored procedure returned no result, indicating that the order might not have been added successfully.");
                }

                return new AddOrderResponse { order_id = result }; ;
            }
            catch (Exception ex)
            {
                throw new Exception($"order is not added successfully...{ex.Message}");
            }
        }

        public async Task<InserOrderListResponse> InserOrderList(InserOrderListRequest request)
        {
            try
            {
                var parameters = new
                {
                    p_OrderID = request.OrderID,
                    p_ProductID = request.ProductID,
                    p_Quantity = request.Quantity,
                    p_Price = request.Price,
                    p_SalePrice = request.SalePrice,
                    p_TaxAmount = request.TaxAmount,
                    p_DeliveryAmount = request.DeliveryAmount,
                    p_Amount = request.Amount,
                    p_TaxID = request.TaxID,
                };
                var result = await ExecuteQueryAsync<InserOrderListResponse>(SPConstant.InsertOrderList, parameters);

                return new InserOrderListResponse { Message = "Inserted successfullyy..." };
            }
            catch (Exception ex)
            {
                throw new Exception($"orderList is not added successfully...{ex.Message}");
            }
        }

        public async Task<GetOrderListResponse> GetOrderList(GetOrderListByOrderIdRequest request)
        {
            var orderDictionary = new Dictionary<string, GetOrderListResponse>();
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    try
                    {
                        var result = await connection.QueryAsync<GetOrderListResponse, OrderList, GetOrderListResponse>(SPConstant.GetOrderwithItemsbyOrderID,
                        (order, orderList) =>
                        {
                            if (!orderDictionary.TryGetValue(order.order_id, out var currentOrder))
                            {
                                currentOrder = order;
                                orderDictionary.Add(currentOrder.order_id, currentOrder);
                            }

                            currentOrder.orderLists.Add(orderList);
                            return currentOrder;
                        },
                        new { p_OrderID= request.OrderID },
                        splitOn: "OrderListID" // This is the field where the split occurs between Order and OrderItem
                        );
                        return result.SingleOrDefault();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }


         }
        public async Task<GetOrderListforBuyerResponse> GetOrderItemsforBuyer(GetOrderListByOrderIdRequest request)
        {
            var orderDictionary = new Dictionary<string, GetOrderListforBuyerResponse>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                try
                {
                    var result = await connection.QueryAsync<GetOrderListforBuyerResponse, OrderList, GetOrderListforBuyerResponse>(SPConstant.GetOrderwithItemsforBuyerbyOrderID,
                    (order, orderList) =>
                    {
                        if (!orderDictionary.TryGetValue(order.order_id, out var currentOrder))
                        {
                            currentOrder = order;
                            orderDictionary.Add(currentOrder.order_id, currentOrder);
                        }

                        currentOrder.orderLists.Add(orderList);
                        return currentOrder;
                    },
                    new { p_OrderID = request.OrderID },
                    splitOn: "OrderListID" // This is the field where the split occurs between Order and OrderItem
                    );
                    return result.SingleOrDefault();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }


        }


        public async Task<InserOrderListResponse> UpdateOrderList(UpdateOrderListRequest request)
        {
            try
            {
                var parameters = new
                {
                    p_OrderListID = request.OrderListID,
                    p_OrderID = request.OrderID,
                    p_ProductID = request.ProductID,
                    p_Quantity = request.Quantity,
                    p_Price = request.Price,
                    p_SalePrice = request.SalePrice,
                    p_TaxAmount = request.TaxAmount,
                    p_DeliveryAmount = request.DeliveryAmount,
                    p_Amount = request.Amount,
                    p_TaxID = request.TaxID,
                };
                var result = await ExecuteQueryAsync<OrderList>(SPConstant.UpdateOrderList, parameters);
                if (result == null)
                {
                    return new InserOrderListResponse { Message = "data is not updated in db.." };
                }
                return new InserOrderListResponse { Message = "data updated Successfullyy.." };
            }
            catch (Exception ex)
            {
                throw new Exception($"order is not added successfully...{ex.Message}");
            }
        }


        //GetOrderwithItemsbyOrderID
        public async Task<List<GetOrderResponse>> GetOrderwithItemsbyUserId(GetOrderwithItemsbyUserIDRequest request)
        {
                        
             var orderDictionary = new Dictionary<string, GetOrderResponse>();
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    try
                    {
                        var result = await connection.QueryAsync<GetOrderResponse, OrderList, GetOrderResponse>(SPConstant.GetOrderwithItemsbyUserId,
                        (order, orderList) =>
                        {
                            if (!orderDictionary.TryGetValue(order.order_id, out var currentOrder))
                            {
                                currentOrder = order;
                                orderDictionary.Add(currentOrder.order_id, currentOrder);
                            }

                            currentOrder.orderLists.Add(orderList);
                            return currentOrder;
                        },
                        new { p_user_id= request.user_id },
                        splitOn: "OrderListID" // This is the field where the split occurs between Order and OrderItem
                        );
                        return result.ToList();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }


            }


 //GetOrderbySellerID
            public async  Task<GetOrderbySellerIDResponse> GetOrderbySellerID( GetOrderbySellerIDRequest request)
                {
                try
                {
                    var parameters = new
                    {
                        p_entity_id=request.entity_id 
                    };

                    var result = await ExecuteQueryListAsync<GetOrderbySellerIDResponse2>(SPConstant.GetOrderbySellerID, parameters);
                    if (result == null)
                    {
                        return new GetOrderbySellerIDResponse { message = " not found ...", orders = null };
                    }
                    return new GetOrderbySellerIDResponse { message = " Fetched Successfullyyy...", orders=result };
                }
                catch (Exception ex)
                {
                    throw new Exception($" not get successfully...{ex.Message}");
                }  
           }
        }

 }