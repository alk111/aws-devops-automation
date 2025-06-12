using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMartApplication.Services.Wrapper;
using OMartDomain.Models.Seller.RequestAndResponce;
using OMartDomain.Models.User;
using OMartDomain.Models.User.RequestAndResponse;

namespace OMartApplication.Repositories
{
    public interface IUserDetailsRepository
    {

        //user creation methods 

        Task<UserCreationResponse> createUserWithSecretAsync(UserCreationRequest userCreationRequest);

        //user creation methods


        //commons
        Task<bool> VerifyForExistingUserAsync(string email);
        Task<string> getUserIdByUserEmail(string email);
        //commons


        //update user
        Task<UpdateUserLocationResponse> UpdateUserLocationAsync(UpdateUserLocationRequest updateUserLocationRequest);
        Task<UserPasswordResetResponse> ResetUserPasswordWithOtpAsync(string user_id, string newPassword);
        //update user

        Task<GetUserDetailsResponce> getUserDetailsByUserId(string Id);
        Task<bool> updateUserDetailsByUserId(UpdateUserDetailsRequest updateDetails);
    }
}