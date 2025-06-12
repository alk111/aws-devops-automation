using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMartApplication.Services.Wrapper;
using OMartDomain.Models.Seller.RequestAndResponce;
using OMartDomain.Models.User;
using OMartDomain.Models.User.RequestAndResponse;


namespace OMartApplication.Services
{
    public interface IOTPService
    {
        Task<OtpGenerateResponce> generateAndStoreOtpAsync(OtpGenerateRequest otpGenerateRequest);
        Task<VerifyOtpWithEmailResponse> GetVerificationStatusAsync(VerifyOtpWithEmailRequest verifyOtpWithEmailRequest);
        // Task<OtpGenerateForResetPasswordResponse> generateAndStoreUserOtpForResetPasswordAsync(OtpGenerateForResetPasswordRequest otpGenerateForResetPasswordRequest);
        // Task<VerifyOtpWithEmailResponse> userOtpVerificationAsync(string email, int otp);
        // Task<SellerOtpGenerateResponse> generateAndStoreSellerOtpAsync(SellerOtpGenerateRequest sellerOtpGenerateRequest);
        // Task<VerifySellerOtpWithEmailResponse> sellerOtpVerficationAsync(VerifySellerOtpWithEmailRequest verifySellerOtpWithEmailRequest);
        Task<bool> IsEmailVerifiedByOtpAsync(string email);
        Task<bool> IsEmailVerifiedByOtpForLetestRecordAsync(string email);

    }
}