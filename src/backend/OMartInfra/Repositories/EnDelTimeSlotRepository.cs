using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OMartApplication.Repositories;
using OMartDomain.Common;
using OMartDomain.Models.EnDelTimeSlot;
using OMartDomain.Models.EnDelTimeSlot.EnDelTimeSlotRequestAndResponse;

namespace OMartInfra.Repositories
{
    public class EnDelTimeSlotRepository : CentralRepository, IEnDelTimeSlotRepository
    {
          private readonly string? _connectionString;

        public EnDelTimeSlotRepository(IConfiguration configuration) : base(configuration, "OMartDevDbConnection")
        {
            _connectionString = configuration.GetConnectionString(name: "OMartDevDbConnection");

        }

        public async   Task<EnDelTimeSlotResponse> AddDeliveryTimeSlot(EnDelTimeSlotRequest request)
            {
               try{
                    var parameters=new 
                    {
                        p_EnDelTimeSlotID=request.EnDelTimeSlotID,
                        p_EnityID=request.EnityID,
                        p_DeliveryTimeStart=request.DeliveryTimeStart,
                        p_DeliveryTimeEnd=request.DeliveryTimeEND
                    };
                    int result = await ExecuteQueryAsync<int>(SPConstant.AddDeliveryTimeSlot, parameters);
                    if (result > 0)
                    {
                        throw new Exception("The stored procedure returned no result, indicating that the order might not have been added successfully.");
                    }

                    return new EnDelTimeSlotResponse { message = "Data Added Successfully..." };
                 }
                    catch (Exception ex)
                    {
                        throw new Exception($"EnDelTimeSlot is not added successfully...{ex.Message}");
                    }
               } 
            
           

                public async  Task<EnDelTimeSlotResponse> DeleteEnDeliveryTimeSlot(int EnDelTimeSlotID)
                {
                    try{
                            var parameters = new
                            {
                                p_EnDelTimeSlotID=EnDelTimeSlotID
                            };
                            var result= await ExecuteQueryAsync<int>(SPConstant.DeleteEnDeliveryTimeSlot,parameters);
                            if(result > 0)
                            {
                                return new EnDelTimeSlotResponse{message="data not found in db"};
                            }
                          return new EnDelTimeSlotResponse{message="data deleted successfully...."};
                       }
                       catch(Exception ex)
                       {
                        throw new Exception($"Exception while connecting to dataBase :{ex.Message}");
                       }
                       
                }
            
             public async Task<UpdateEnDelTimeSlotResponse> UpdateEnDelTimeSlot(UpdateEnDelTimeSlotRequest request)
             {
                try{
                     var parameters = new
                     {
                        p_EnityID=request.EnityID,
                        p_EnDelTimeSlotID=request.EnDelTimeSlotID,
                        p_DeliveryTimeStart=request.DeliveryTimeStart,
                        p_DeliveryTimeEND=request.DeliveryTimeEND
                     };
                     int result= await ExecuteQueryAsync<int>(SPConstant.UpdateEnDelTimeSlot,parameters);
                     return new UpdateEnDelTimeSlotResponse{message="Updated successfully..."};
                }
            catch (Exception ex)
            {
                        throw new Exception($"Exception while connecting to dataBase :{ex.Message}");
                       }
             }
    }
}