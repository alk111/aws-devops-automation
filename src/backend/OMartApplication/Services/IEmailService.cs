using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartApplication.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string toEmail, string otp);
        Task<bool> SendPasswordResetEmailAsync(string toEmail, string otp);
        Task<bool> SendSellerRegistrationEmailAsync(string toEmail, string otp);
    }
}