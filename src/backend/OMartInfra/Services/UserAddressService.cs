using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMartApplication.Repositories;
using OMartApplication.Services;
using OMartDomain.Models.UserAddress.RequestAndResponse;

namespace OMartInfra.Services
{
    public class UserAddressService : IUserAddressService
    {
         private readonly IUserAddressRepository userAddressRepository1;
         public UserAddressService(IUserAddressRepository userAddressRepository1)
         {
             this.userAddressRepository1=userAddressRepository1;
         }

         public async Task<InsertUserAddressResponse> InsertUserAddress(InsertUserAddressRequest request)
         {
              return await userAddressRepository1.InsertUserAddress(request);
         }

          public async Task<GetUserAddressbyUserIDResponse> GetUserAddressbyUserID(string UserID)
          {
            return await userAddressRepository1.GetUserAddressbyUserID(UserID);
          }

           public async Task<InsertUserAddressResponse> UpdateUserAddress(UpdateUserAddressRequest request)
           {
            return await userAddressRepository1.UpdateUserAddress(request);
           }

           public async Task<InsertUserAddressResponse> DeleteUserAddress(string AddressID)
           {
            return await userAddressRepository1.DeleteUserAddress(AddressID);
           }
    }
}