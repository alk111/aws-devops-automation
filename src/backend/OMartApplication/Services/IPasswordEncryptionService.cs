using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMartApplication.Services
{
    public interface IPasswordEncryptionService
    {
        Task<string> EncryptPassword(string password);
        Task<string> DecryptPassword(string password);
        Task<bool> ValidatePassword(string normalPassword, string encryptedPassword);
        Task<string> PasswordHasher(string password);
        Task<PasswordVerificationResult> PasswordHashValidator(string hashedPassword, string password);
    }
}