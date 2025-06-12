using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OMartApplication.Repositories;
using OMartDomain.Common;
using OMartDomain.Models.UserAddress;
using OMartDomain.Models.UserAddress.RequestAndResponse;

namespace OMartInfra.Repositories
{
    public class UserAddressRepository :CentralRepository, IUserAddressRepository
    {
         private readonly string? _connectionString;

        public UserAddressRepository(IConfiguration configuration) : base(configuration, "OMartDevDbConnection")
        {
            _connectionString = configuration.GetConnectionString(name: "OMartDevDbConnection");
        }

       public async Task<InsertUserAddressResponse> InsertUserAddress(InsertUserAddressRequest request)
            {
                try
                {
                    var parameters = new
                    {
                            p_UserID=request.userID,
                            p_Apt_Street=request.aptStreet,
                            p_Area=request.Area,
                            p_Landmark=request.landmark,
                            p_PinCode=request.pinCode,
                            p_City=request.city,
                            p_State=request.state,
                            p_Country=request.country
                    };
                int result = await ExecuteQueryAsync<int>(SPConstant.InsertUserAddress, parameters);

                    if (result > 0)
                    {
                        throw new Exception("The stored procedure returned no result, indicating that the order might not have been added successfully.");
                    }

                    return new InsertUserAddressResponse { message = "Added 😃😃😃..." };
                }
                catch (Exception ex)
                {
                    throw new Exception($"UserAddress is not added successfully...{ex.Message}");
                }
            }

//GetUserAddressbyUserID

           public async Task<GetUserAddressbyUserIDResponse> GetUserAddressbyUserID(string UserID)
                {
                    try{
                        var parameters = new
                        {
                            p_user_id=UserID
                        };
                        var result = await ExecuteQueryListAsync<UserAddress>(SPConstant.GetUserAddressbyUserID, parameters);
                        if(result != null)
                        {
                            return new GetUserAddressbyUserIDResponse{message="data get successfully😃😃😃...",userAddress=result};
                        }
                        return new GetUserAddressbyUserIDResponse{message="data is not get successfully😯😯😯...",userAddress=null};  
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"while connecting to db :{ex.Message}");
                    }
                }
//updateUserAddress

         public async Task<InsertUserAddressResponse> UpdateUserAddress(UpdateUserAddressRequest request)
            {
                try{
                        var parameters = new
                        {
                            p_AddressID=request.AddressID,
                            p_UserID=request.UserID,
                            p_Apt_Street=request.AptStreet,
                            p_Area=request.Area,
                            p_Landmark=request.Landmark,
                            p_PinCode=request.PinCode,
                            p_City=request.City,
                            p_State=request.State,
                            p_Country=request.Country 
                        };
                        
                    var result= await ExecuteQueryAsync<UserAddress>(SPConstant.UpdateUserAddress,parameters);
                    return new InsertUserAddressResponse{message="updated😃😃😃...."};   

                }
                    catch(Exception ex)
                    {
                        throw new Exception($"While connecting to db :{ex.Message}");
                    }
            }

 //DeleteUserAddress
            
            public async Task<InsertUserAddressResponse> DeleteUserAddress(string AddressID)
            {
                    try{
                        var parameters = new
                        {
                            p_AddressID=AddressID,
                        };
                        var result= await ExecuteQueryAsync<UserAddress>(SPConstant.DeleteUserAddress,parameters);
                        if(result==null){
                            return new InsertUserAddressResponse{message="not found AddressID in db"};
                        }

                        return new InsertUserAddressResponse{message="Deleted😃😃😃..."};
                    }
                    catch(Exception ex)
                    {
                        throw new Exception($"while connecting to db :{ex.Message}");
                    }
            }          
    }
}