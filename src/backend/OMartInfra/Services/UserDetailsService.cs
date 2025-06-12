using System.Collections;
using Microsoft.AspNetCore.Identity;
using OMartApplication.Repositories;
using OMartApplication.Services;
using OMartApplication.Services.Wrapper;
using OMartDomain.Models.Seller.RequestAndResponce;
using OMartDomain.Models.User;
using OMartDomain.Models.User.RequestAndResponse;
using OMartDomain.Models.Wrapper;
using OMartInfra.Repositories;

namespace OMartInfra.Services
{
    public class UserDetailsService : IUserDetailsService
    {

        private readonly IUserDetailsRepository _iUserDetailsRepository;

        private readonly IOTPService _iOPTService;
        private readonly IPasswordEncryptionService encryptionService;
        private readonly IEmailService _iEmailService;

        public UserDetailsService(IUserDetailsRepository iUserDetailsRepository, IOTPService iOPTService, IPasswordEncryptionService encryptionService, IEmailService iEmailService)
        {
            _iUserDetailsRepository = iUserDetailsRepository;
            _iOPTService = iOPTService;
            _iEmailService = iEmailService;
            this.encryptionService = encryptionService;
        }


        public async Task<IResult<OtpGenerateResponce>> sendUserRegistrationOtpViaEmailAsync(OtpGenerateRequest otpGenerateRequest)
        {
            try
            {
                List<string> messages = new List<string>();

                var checkUserExistInTheUserdetailsTable = await _iUserDetailsRepository.VerifyForExistingUserAsync(otpGenerateRequest.email);

                if (checkUserExistInTheUserdetailsTable)
                {
                    messages.Add("User Already Exsist in userdatails table");
                    return Result<OtpGenerateResponce>.Fail(messages);
                }
                messages.Add("User with email did not exist in the userdetails tables");

                var otpResult = await _iOPTService.generateAndStoreOtpAsync(otpGenerateRequest);

                if (otpResult.otp == 0)
                {
                    messages.Add("User with the given email already verified");
                    return Result<OtpGenerateResponce>.Fail(messages);
                }
                messages.Add("Successfully store otp details in database");

                string otp = otpResult.otp.ToString();

                var emailResult = await _iEmailService.SendEmailAsync(otpGenerateRequest.email, otp);

                if (!emailResult)
                {
                    messages.Add("Error while sending the email.");
                    return Result<OtpGenerateResponce>.Fail(messages);
                }
                return Result<OtpGenerateResponce>.Success("Successfully Otp send to UserEmail");

            }
            catch (Exception ex)
            {
                throw new Exception($"An Error occur in userdeatails service while storing and sending otp and email data {ex.Message}");
            }
        }

        public async Task<IResult<OtpGenerateForResetPasswordResponse>> sendUserResetPasswordOtpViaUserEmailAsync(OtpGenerateRequest otpGenerateRequest)
        {
            try
            {
                List<string> messages = new List<string>();

                var isUserActive = await _iUserDetailsRepository.VerifyForExistingUserAsync(otpGenerateRequest.email);
                if (!isUserActive)
                {
                    messages.Add("No active user found with the provided email address.");
                    return Result<OtpGenerateForResetPasswordResponse>.Fail(messages);
                }
                messages.Add("Active user with the email found");

                var otpResult = await _iOPTService.generateAndStoreOtpAsync(otpGenerateRequest);
                string otp = otpResult.otp.ToString();

                if (otp == null)
                {
                    messages.Add("Something went wrong while storing otp data");
                    return Result<OtpGenerateForResetPasswordResponse>.Fail(messages);
                }
                messages.Add("Otp data stored Successfully");

                var emailResult = await _iEmailService.SendPasswordResetEmailAsync(otpGenerateRequest.email, otp);

                if (!emailResult)
                {
                    messages.Add("Error while sending the email.");
                    return Result<OtpGenerateForResetPasswordResponse>.Fail(messages);
                }
                messages.Add("Successfully Otp send to UserEmail for user reset password");

                return Result<OtpGenerateForResetPasswordResponse>.Success("Email Send to the given user pls verify otp");
            }
            catch (Exception ex)
            {
                throw new Exception($"Something went wrong in otp service while sending reset password email to user {ex.Message}");
            }
        }

        public async Task<IResult<VerifyOtpWithEmailResponse>> verifyUserRegistrationrOtpWithEmailAsync(VerifyOtpWithEmailRequest verifyOtpWithEmailRequest)
        {
            List<string> messages = new List<string>();

            if (string.IsNullOrWhiteSpace(verifyOtpWithEmailRequest.email) || verifyOtpWithEmailRequest.otp <= 0)
            {
                messages.Add("Invalid email or OTP.");
                return Result<VerifyOtpWithEmailResponse>.Fail(messages);
            }
            messages.Add("Email and otp is valid");

            try
            {
                // Verify the OTP using the repository
                VerifyOtpWithEmailResponse otpVerificationResult = await _iOPTService.GetVerificationStatusAsync(verifyOtpWithEmailRequest);

                if (otpVerificationResult.is_expired)
                {
                    return Result<VerifyOtpWithEmailResponse>.Fail(otpVerificationResult.verificationMessage);
                }

                return Result<VerifyOtpWithEmailResponse>.Success(otpVerificationResult);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred during OTP verification: {ex.Message}");
            }
        }

        public async Task<IResult<VerifyOtpWithEmailResponse>> verifyUserResetPasswordOtpWithEmailAsync(VerifyOtpWithEmailRequest verifyOtpWithEmailRequest)
        {
            List<string> messages = new List<string>();

            if (string.IsNullOrWhiteSpace(verifyOtpWithEmailRequest.email) || verifyOtpWithEmailRequest.otp <= 0)
            {
                messages.Add("Invalid email or OTP.");
                return Result<VerifyOtpWithEmailResponse>.Fail(messages);
            }
            messages.Add("Email and otp is valid");
            try
            {
                VerifyOtpWithEmailResponse otpVerificationResult = await _iOPTService.GetVerificationStatusAsync(verifyOtpWithEmailRequest);

                if (otpVerificationResult.is_expired)
                {
                    return Result<VerifyOtpWithEmailResponse>.Fail(otpVerificationResult.verificationMessage);
                }

                return Result<VerifyOtpWithEmailResponse>.Success(otpVerificationResult);
            }
            catch (Exception ex)
            {
                // Return a failure result with the exception message
                return Result<VerifyOtpWithEmailResponse>.Fail($"An error occurred during OTP verification: {ex.Message}");
            }
        }

        public async Task<IResult<UserCreationResponse>> createUserWithSecretAsync(UserCreationRequest userCreationRequest)
        {
            try
            {
                // Check if the email is verified
                var verfiedEmail = await _iOPTService.IsEmailVerifiedByOtpAsync(userCreationRequest.ContactEmail);
                if (!verfiedEmail)
                {
                    // Return a failure result if email is not verified
                    return Result<UserCreationResponse>.Fail("Email is not verified.");
                }
                //check if user with email already exists
                var alreadyExistingEmail = await _iUserDetailsRepository.VerifyForExistingUserAsync(userCreationRequest.ContactEmail);
                if (alreadyExistingEmail)
                {
                    return Result<UserCreationResponse>.Fail("User Already Exists.");
                }

                // Encrypt the user's password
                userCreationRequest.Password = await encryptionService.PasswordHasher(userCreationRequest.Password);

                // Create the user in the repository
                var creationResult = await _iUserDetailsRepository.createUserWithSecretAsync(userCreationRequest);

                return creationResult.user_id.Any()
                ?
                Result<UserCreationResponse>.Success(creationResult, "User with the given password is created")
                :
                Result<UserCreationResponse>.Fail("Something went wrong in userdetails service could not create user");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during user creation: {ex.Message}");
            }

        }

        public async Task<IResult<UserPasswordResetResponse>> userPasswordResetAsync(UserPasswordResetRequest userPasswordResetRequest)
        {
            try
            {

                var checkEmailIsVerified = await _iOPTService.IsEmailVerifiedByOtpForLetestRecordAsync(userPasswordResetRequest.email);
                if (!checkEmailIsVerified)
                {
                    return Result<UserPasswordResetResponse>.Fail("Email is not Verified");
                }

                // Encrypt the user's password
                userPasswordResetRequest.newPassword = await encryptionService.PasswordHasher(userPasswordResetRequest.newPassword);

                // Get User Id
                var getUserId = await _iUserDetailsRepository.getUserIdByUserEmail(userPasswordResetRequest.email);
                if (getUserId == null)
                {
                    return Result<UserPasswordResetResponse>.Fail("UserId is not Found");
                }
                // Update user Password
                var updateUser = await _iUserDetailsRepository.ResetUserPasswordWithOtpAsync(getUserId, userPasswordResetRequest.newPassword);

                return updateUser.message.Any() ? Result<UserPasswordResetResponse>.Success(updateUser) : Result<UserPasswordResetResponse>.Fail(updateUser.message);

            }
            catch (Exception ex)
            {
                throw new Exception($"Something went wrong while reseting password in userdetails service {ex.Message}");
            }
        }
        public async Task<IResult<UpdateUserLocationResponse>> updateUserLocationAsync(UpdateUserLocationRequest updateUserLocationRequest)
        {
            try
            {
                UpdateUserLocationResponse updateUserLocationResponse = await _iUserDetailsRepository.UpdateUserLocationAsync(updateUserLocationRequest);
                if (!updateUserLocationRequest.userId.Any())
                {
                    return Result<UpdateUserLocationResponse>.Fail("could not found the user with given id");
                }
                return Result<UpdateUserLocationResponse>.Success(updateUserLocationResponse, "User location updated successfully");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occur while updating userlocation in userdetails service {ex.Message}");
            }
        }

        public async Task<IResult<GetUserDetailsResponce>> getUserDetailsByUserId(string Id)
        {
            try
            {

                var details = await _iUserDetailsRepository.getUserDetailsByUserId(Id);

                return details != null ? Result<GetUserDetailsResponce>.Success(details)
                :
                Result<GetUserDetailsResponce>.Fail("Something went wrong");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occur while getting the details in service {ex.Message}");
            }
        }
        public async Task<IResult<bool>> updateUserDetailsByUserId(UpdateUserDetailsRequest updateDetails)
        {
            try
            {

                var isUpdated = await _iUserDetailsRepository.updateUserDetailsByUserId(updateDetails);

                return isUpdated ? Result<bool>.Success(true)
                :
                Result<bool>.Fail("Something went wrong");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occur while getting the details in seller service {ex.Message}");
            }
        }
    }
}