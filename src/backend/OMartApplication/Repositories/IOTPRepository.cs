using OMartApplication.Services.Wrapper;
using OMartDomain.Models.User;

namespace OMartApplication.Repositories
{
    public interface IOTPRepository
    {
        Task<bool> StoreOtpDataAsync(string email, int otp);
        Task<Dictionary<string, object>> GetVerificationStatusAsync(string userEmail, int userOtp);
        // Task<bool> StoreOtpDataForResetPassword(string email, int otp);
        Task<bool> IsEmailVerifiedByOtpForLetestRecordAsync(string email);
        // Task<int> OTPVerificationAsync(string email, int otp);
        Task<bool> IsEmailVerifiedByOtpAsync(string email);
    }
}