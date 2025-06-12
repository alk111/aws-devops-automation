using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMartDomain.Models.UserAddress.RequestAndResponse;

namespace OMartApplication.Repositories
{
    public interface IUserAddressRepository
    {
        Task<InsertUserAddressResponse> InsertUserAddress(InsertUserAddressRequest request);
        Task<GetUserAddressbyUserIDResponse> GetUserAddressbyUserID(string UserID);
        Task<InsertUserAddressResponse> UpdateUserAddress(UpdateUserAddressRequest request);
        Task<InsertUserAddressResponse> DeleteUserAddress(string AddressID);
    }
}