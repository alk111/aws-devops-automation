using OMartApplication.Repositories;
using Microsoft.Extensions.Configuration;
using OMartDomain.Common;
using OMartDomain.Models.User;
using OMartDomain.Models.User.RequestAndResponse;

namespace OMartInfra.Repositories
{
    public class OTPRepository : CentralRepository, IOTPRepository
    {
        private readonly string _connectionString;
        public OTPRepository(IConfiguration configuration) : base(configuration, "OMartDevDbConnection")
        {
            _connectionString = configuration.GetConnectionString("OMartDevDbConnection") ?? throw new InvalidOperationException("The connection string 'OMartDevDbConnection' is not found in the configuration.");
        }

        public async Task<bool> StoreOtpDataAsync(string userEmail, int userOtp)
        {

            try
            {
                var parameters = new
                {
                    email = userEmail,
                    otp = userOtp
                };
                // Execute the stored procedure to insert OTP
                var result = await ExecuteQueryAsync<int>(SPConstant.InsertOtpForEmailVerification, parameters);

                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occur with storing the data in the database {ex.Message}");
            }
        }


        public async Task<Dictionary<string, object>> GetVerificationStatusAsync(string userEmail, int userOtp)
        {
            var parameters = new
            {
                p_email = userEmail,
                p_otp = userOtp
            };

            var result = await ExecuteQueryAsync<VerifyOtpWithEmailResponse>(SPConstant.VerifyOtp, parameters);

            Dictionary<string, object> res = new Dictionary<string, object>
            {
                { "is_expired", result.is_expired },
                { "is_verified", result.is_verified },
                { "verificationMessage", result.verificationMessage }
            };


            return res;
        }

        // public async Task<bool> StoreOtpDataForResetPassword(string userEmail, int userOtp)
        // {
        //     try
        //     {

        //         var parameters = new
        //         {
        //             email = userEmail,
        //             otp = userOtp
        //         };
        //         // Execute the stored procedure to insert OTP
        //         var result = await ExecuteQueryAsync<int>(SPConstant.InsertOtpForEmailVerification, parameters);

        //         // Return success or failure based on result
        //         return result > 0;
        //     }
        //     catch (Exception ex)
        //     {
        //         // Handle and log the exception
        //         throw new Exception("An error occur in otp repository while storing the otp details for user password reset");
        //     }
        // }

        // public async Task<int> OTPVerificationAsync(string userEmail, int userOtp)
        // {
        //     try
        //     {
        //         // Set parameters for the stored procedure to get OTP details
        //         var parameters = new
        //         {
        //             p_email = userEmail,
        //             p_otp = userOtp
        //         };

        //         // Execute the stored procedure to get OTP details
        //         var otpDetails = await ExecuteQueryAsync<OTPDetails>(SPConstant.GetOtpDetails, parameters);

        //         // If no result is returned, the OTP or email is incorrect
        //         if (otpDetails == null)
        //         {

        //             return 0; //invalid otp
        //         }

        //         // Check if the OTP has expired
        //         if (otpDetails.is_expired || DateTime.UtcNow > otpDetails.otp_expires_on)
        //         {
        //             return 1; //otp expire
        //         }

        //         // Set parameters for the stored procedure to update OTP status
        //         var updateParameters = new
        //         {
        //             p_email = userEmail,
        //             p_otp = userOtp
        //         };

        //         // Execute the stored procedure to update OTP status
        //         var updateResult = await ExecuteQueryAsync<int>(SPConstant.UpdateOtpStatus, updateParameters);

        //         // Check if the update was successful
        //         if (updateResult > 0)
        //         {
        //             return 2; // otp verify successfully
        //         }
        //         else
        //         {
        //             return 3; //failed to verify otp
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         // Handle and log the exception
        //         throw new Exception($"An error in otp repositoty occurred while verifying OTP: {ex.Message}");
        //     }
        // }


        public async Task<bool> IsEmailVerifiedByOtpAsync(string email)
        {

            try
            {
                var parameters = new
                {
                    p_email = email
                };

                var result = await ExecuteQueryAsync<int>(SPConstant.IsEmailVerifiedByOtp, parameters);

                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error in otp repository occurred while checking email verification status: {ex.Message}", ex);
            }
        }

        public async Task<bool> IsEmailVerifiedByOtpForLetestRecordAsync(string userEmail)
        {
            try
            {

                var parameters = new
                {
                    input_email = userEmail
                };

                OTPDetails result = await ExecuteQueryAsync<OTPDetails>(SPConstant.GetLatestEntryByEmailForUserPasswordReset, parameters);

                bool isVerified = Convert.ToBoolean(result.is_verified);

                return isVerified;

            }
            catch (Exception ex)
            {
                throw new Exception($"An error in otp repository occurred while checking email verification status: {ex.Message}", ex);
            }
        }

    }
}