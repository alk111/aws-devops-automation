using System.Net.Mail;
using OMartApplication.Repositories;
using OMartApplication.Services;
using OMartApplication.Services.Wrapper;
using OMartDomain.Models.Seller.RequestAndResponce;
using OMartDomain.Models.User.RequestAndResponse;
using OMartDomain.Models.Wrapper;

namespace OMartInfra.Services
{
    public class OTPService : IOTPService
    {
        private static readonly Random _random = new Random();
        private readonly IOTPRepository _iOTPRepository;
        public OTPService(IOTPRepository iOTRepository)
        {
            _iOTPRepository = iOTRepository;
        }


        //Users

        public async Task<OtpGenerateResponce> generateAndStoreOtpAsync(OtpGenerateRequest otpGenerateRequest)
        {
            try
            {
                ValidateEmail(otpGenerateRequest.email);

                int newOtp = GenerateOtp();

                // bool isUserAlreadyVerified = await _iOTPRepository.IsEmailVerifiedByOtpAsync(otpGenerateRequest.email);

                // if (isUserAlreadyVerified)
                // {
                //     return new OtpGenerateResponce { otp = 0 };
                // }

                bool isOtpDataStored = await _iOTPRepository.StoreOtpDataAsync(otpGenerateRequest.email, newOtp);

                return new OtpGenerateResponce { otp = newOtp };

            }
            catch (Exception ex)
            {
                throw new Exception($"Error while generating OTP in otp service: {ex.Message}");
            }
        }

        public async Task<VerifyOtpWithEmailResponse> GetVerificationStatusAsync(VerifyOtpWithEmailRequest verifyOtpWithEmailRequest)
        {
            Dictionary<string, object> res = await _iOTPRepository.GetVerificationStatusAsync(verifyOtpWithEmailRequest.email, verifyOtpWithEmailRequest.otp);

            VerifyOtpWithEmailResponse response = new VerifyOtpWithEmailResponse
            {
                is_expired = res.ContainsKey("is_expired") ? Convert.ToBoolean(res["is_expired"]) : false,
                is_verified = res.ContainsKey("is_verified") ? Convert.ToBoolean(res["is_verified"]) : false,
                verificationMessage = res.ContainsKey("verificationMessage") ? res["verificationMessage"].ToString() : "No message"
            };

            Console.WriteLine(response);

            return response;
        }

        // public async Task<OtpGenerateForResetPasswordResponse> generateAndStoreUserOtpForResetPasswordAsync(OtpGenerateForResetPasswordRequest otpGenerateForResetPasswordRequest)
        // {
        //     try
        //     {
        //         ValidateEmail(otpGenerateForResetPasswordRequest.email);

        //         int newOtp = GenerateOtp();

        //         bool isOtpDataForResetPasswordStored = await _iOTPRepository.StoreOtpDataForResetPassword(otpGenerateForResetPasswordRequest.email, newOtp);

        //         return new OtpGenerateForResetPasswordResponse { otp = newOtp };

        //     }
        //     catch (Exception ex)
        //     {
        //         throw new Exception($"Error while generating OTP for reset password in otp service: {ex.Message}");
        //     }
        // }

        // public async Task<VerifyOtpWithEmailResponse> userOtpVerificationAsync(string email, int otp)
        // {
        //     try
        //     {
        //         int result = await _iOTPRepository.OTPVerificationAsync(email, otp);

        //         if (result == 0)
        //         {
        //             return new VerifyOtpWithEmailResponse { code = 0, message = "otp or email is wrong" };
        //         }
        //         else if (result == 1)
        //         {
        //             return new VerifyOtpWithEmailResponse { code = 1, message = "otp has expired" };
        //         }
        //         else if (result == 2)
        //         {
        //             return new VerifyOtpWithEmailResponse { code = 2, message = "Otp verified successfully" };
        //         }
        //         else
        //         {
        //             return new VerifyOtpWithEmailResponse { code = 3, message = "Something went wrong while updating otp status" };
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         throw new Exception($"Error while verifying OTP for reset password in otp service: {ex.Message}");
        //     }
        // }


        // //Users



        // //Sellers

        // public async Task<SellerOtpGenerateResponse> generateAndStoreSellerOtpAsync(SellerOtpGenerateRequest sellerOtpGenerateRequest)
        // {
        //     try
        //     {

        //         ValidateEmail(sellerOtpGenerateRequest.email);

        //         int newOtp = GenerateOtp();

        //         bool isOtpDataStored = await _iOTPRepository.StoreOtpDataAsync(sellerOtpGenerateRequest.email, newOtp);

        //         return new SellerOtpGenerateResponse { otp = newOtp };


        //     }
        //     catch (Exception ex)
        //     {
        //         throw new Exception($"Error while generating OTP for seller in otp service: {ex.Message}");
        //     }
        // }

        // public async Task<VerifySellerOtpWithEmailResponse> sellerOtpVerficationAsync(VerifySellerOtpWithEmailRequest verifySellerOtpWithEmailRequest)
        // {
        //     try
        //     {
        //         int result = await _iOTPRepository.OTPVerificationAsync(verifySellerOtpWithEmailRequest.email, verifySellerOtpWithEmailRequest.otp);

        //         if (result == 0)
        //         {
        //             return new VerifySellerOtpWithEmailResponse { code = 0, message = "otp or email is wrong" };
        //         }
        //         else if (result == 1)
        //         {
        //             return new VerifySellerOtpWithEmailResponse { code = 1, message = "otp has expired" };
        //         }
        //         else if (result == 2)
        //         {
        //             return new VerifySellerOtpWithEmailResponse { code = 2, message = "Otp verified successfully" };
        //         }
        //         else
        //         {
        //             return new VerifySellerOtpWithEmailResponse { code = 3, message = "Something went wrong while updating otp status" };
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         throw new Exception($"Error while verifying OTP for seller in otp service: {ex.Message}");
        //     }
        // }

        // //Sellers



        //Commons
        public async Task<bool> IsEmailVerifiedByOtpAsync(string email)
        {
            try
            {
                ValidateEmail(email);
                return await _iOTPRepository.IsEmailVerifiedByOtpAsync(email);
            }
            catch (Exception ex)
            {
                throw new Exception("Error checking email verification status", ex);
            }
        }

        public async Task<bool> IsEmailVerifiedByOtpForLetestRecordAsync(string email)
        {
            try
            {
                ValidateEmail(email);
                return await _iOTPRepository.IsEmailVerifiedByOtpForLetestRecordAsync(email);
            }
            catch (Exception ex)
            {
                throw new Exception("Error checking email verification status", ex);
            }
        }

        private static int GenerateOtp(int length = 6)
        {
            if (length < 4 || length > 10)
            {
                throw new ArgumentException("OTP length should be between 4 and 10 digits.");
            }
            var minValue = (int)Math.Pow(10, length - 1);
            var maxValue = (int)Math.Pow(10, length) - 1;
            var otp = _random.Next(minValue, maxValue + 1);
            return otp;
        }

        public Task<bool> GenerateOtp(string email)
        {
            throw new NotImplementedException();
        }

        public Task<string> OTPVerification(string email, int otp)
        {
            throw new NotImplementedException();
        }

        private void ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || !MailAddress.TryCreate(email, out _))
            {
                throw new ArgumentException("Invalid email format.");
            }
        }

        //Commons
    }
}