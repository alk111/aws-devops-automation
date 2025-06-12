using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMartApplication.Services;
using OMartApplication.Services.Wrapper;
using OMartDomain.Models.UserAddress.RequestAndResponse;

namespace OMAPI.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class UserAddressController : ControllerBase
    {
        private readonly IUserAddressService userAddressService;
        public UserAddressController(IUserAddressService userAddressService)
        {
             this.userAddressService = userAddressService;
        }


     [HttpPost("InsertUserAddress")]
     public async Task<InsertUserAddressResponse> InsertUserAddress(InsertUserAddressRequest request)
     {
       var userAddress= await userAddressService.InsertUserAddress(request);
        return userAddress;
     }
     
      [HttpPost("GetUserAddressbyUserID/{UserID}")]
      public async Task<GetUserAddressbyUserIDResponse> GetUserAddressbyUserID(string UserID)
      {
         return await userAddressService.GetUserAddressbyUserID(UserID);
      }

       [HttpPost("UpdateUserAddress/AddressID")]
       public async Task<InsertUserAddressResponse> UpdateUserAddress(UpdateUserAddressRequest request)
       {
        return await userAddressService.UpdateUserAddress(request);
       }
      
       [HttpDelete("DeleteUserAddress/{AddressID}")]
       public async Task<InsertUserAddressResponse> DeleteUserAddress(string AddressID)
       {
         return await userAddressService.DeleteUserAddress(AddressID);
       }

    }
}