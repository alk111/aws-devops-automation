using Microsoft.Extensions.Configuration;
using MySqlX.XDevAPI.Common;
using OMartApplication.Repositories;
using OMartApplication.Services;
using OMartApplication.Services.Wrapper;
using OMartDomain.Common;
using OMartDomain.Models.Seller;
using OMartDomain.Models.Seller.RequestAndResponce;
using OMartDomain.Models.User;
using OMartDomain.Models.User.RequestAndResponse;

namespace OMartInfra.Repositories
{
    public class UserDetailsRepository : CentralRepository, IUserDetailsRepository
    {

        private readonly string _connectionString;
        private readonly IPasswordEncryptionService _iPasswordEncryptionService;
        public UserDetailsRepository(IConfiguration configuration, IPasswordEncryptionService iPasswordEncryptionService) : base(configuration, "OMartDevDbConnection")
        {
            _connectionString = configuration.GetConnectionString("OMartDevDbConnection") ?? throw new InvalidOperationException("The connection string 'OMartDevDbConnection' is not found in the configuration.");
            _iPasswordEncryptionService = iPasswordEncryptionService;
        }



        public async Task<UserCreationResponse> createUserWithSecretAsync(UserCreationRequest userCreationRequest)
        {
            UserDetails userDetails = new UserDetails
            {
                first_name = userCreationRequest.FirstName,
                last_name = userCreationRequest.LastName,
                middle_name = userCreationRequest.MiddleName,
                contact_email = userCreationRequest.ContactEmail,
                contact_phone_number = userCreationRequest.ContactPhoneNumber,
                is_active = true
            };

            try
            {

                string secret = "";

                if (!string.IsNullOrEmpty(userCreationRequest.Password))
                {
                    secret = userCreationRequest.Password;
                }
                else
                {
                    throw new ArgumentException("Password cannot be null or empty");
                }

                var parameters = new
                {
                    p_user_id = userDetails.user_id,
                    p_iterations = userDetails.iterations,
                    p_first_name = userDetails.first_name,
                    p_middle_name = userDetails.middle_name,
                    p_last_name = userDetails.last_name,
                    p_date_of_birth = userDetails.date_of_birth,
                    p_country = userDetails.country,
                    p_residential_address = userDetails.residential_address,
                    p_permanent_address = userDetails.permanent_address,
                    p_contact_phone_number = userDetails.contact_phone_number,
                    p_contact_email = userDetails.contact_email,
                    p_two_factor_verified = userDetails.TwoFA_verified,
                    p_registered_on = userDetails.registered_on,
                    p_updated_on = userDetails.updated_on,
                    p_is_active = userDetails.is_active,
                    p_is_deleted = userDetails.is_deleted,
                    p_secret = secret
                };

                var result = await ExecuteQueryAsync<int>(SPConstant.InsertUserWithSecrets, parameters);

                return result > 0
                ?
                new UserCreationResponse { user_id = userDetails.user_id.ToString(), message = "UserCreated successfully" }
                :
                new UserCreationResponse { user_id = null, message = "User could not be created" };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during transaction in userdetails repository: {ex.Message}");
            }
        }

        public async Task<bool> VerifyForExistingUserAsync(string email)
        {
            try
            {
                var parameters = new
                {
                    input_contact_email = email
                };

                var userActiveStatus = await ExecuteQueryAsync<int>(SPConstant.GetUserActiveStatusByEmail, parameters);

                return userActiveStatus > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while checking email verification status: {ex.Message}", ex);
            }
        }

        public async Task<UserPasswordResetResponse> ResetUserPasswordWithOtpAsync(string user_id, string newPassword)
        {
            try
            {

                var parameters = new
                {
                    p_userId = user_id,
                    p_newPassword = newPassword
                };

                var result = await ExecuteQueryAsync<int>(SPConstant.UpdateUserPasswordByEmailAndOtp, parameters);

                return result > 0
                ?
                new UserPasswordResetResponse { message = "User password updated successfully" }
                :
                new UserPasswordResetResponse { message = "User password filed to update" };

            }
            catch (Exception ex)
            {
                throw new Exception($"Error during reseting password in userdetails repository : {ex.Message}");
            }
        }

        public async Task<UpdateUserLocationResponse> UpdateUserLocationAsync(UpdateUserLocationRequest updateUserLocationRequest)
        {
            try
            {
                var parameters = new
                {
                    input_user_id = updateUserLocationRequest.userId,
                    input_permanent_address = updateUserLocationRequest.permanent_address,
                    input_residential_address = updateUserLocationRequest.residential_address,
                    input_country = updateUserLocationRequest.country
                };

                int updateResult = await ExecuteQueryAsync<int>(SPConstant.UpdateUserLocation, parameters);

                return updateResult > 0 ? new UpdateUserLocationResponse { message = "User location updated successfully." } : new UpdateUserLocationResponse { message = "Failed to update user location." };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during update the user location in userdetails repository{ex.Message}");
            }
        }
        public async Task<string> getUserIdByUserEmail(string email)
        {
            try
            {
                var parameters = new
                {
                    userEmail = email
                };

                string user_id = await ExecuteQueryAsync<string>(SPConstant.GetUserIdByEmail, parameters);

                return user_id.Any() ? user_id : null;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occur while getting user id in userdetails repository");
            }
        }


        public async Task<GetUserDetailsResponce> getUserDetailsByUserId(string userId)
        {
            try
            {
                var parameters = new
                {
                    p_user_id = userId
                };

                // Fetch user details using stored procedure
                GetUserDetailsResponce userDetailData = await ExecuteQueryAsync<GetUserDetailsResponce>(SPConstant.GetUserDetailsByUserId, parameters);

                // Check if the result is null or empty
                if (userDetailData == null)
                {
                    throw new Exception("User details not found.");
                }

                return userDetailData;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while getting user details: {ex.Message}", ex);
            }
        }
        public async Task<bool> updateUserDetailsByUserId(UpdateUserDetailsRequest updateDetails)
        {

            try
            {

                var parameters = new
                {
                    input_user_id = updateDetails.user_id,
                    input_first_name = updateDetails.first_name,
                    input_last_name = updateDetails.last_name,
                    input_middle_name = updateDetails.middle_name,
                    input_residential_address = updateDetails.residential_address,
                    input_permanent_address = updateDetails.permanent_address,
                    input_country = updateDetails.country
                };

                int row = await ExecuteQueryAsync<int>(SPConstant.UpdateUserDetails, parameters);

                return row > 0 ? true : false;

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occur while Update User details in repository {ex.Message}");
            }

        }

        
    }
}