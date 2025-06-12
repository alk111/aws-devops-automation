using Microsoft.AspNetCore.Identity;
using OMartApplication.Services;
using System.Security.Cryptography;
using System.Text;

namespace OMartInfra.Services
{
    public class PasswordEncryptionService : IPasswordEncryptionService
    {
        private static readonly string hash = "f0xln@rn";


        public async Task<string> EncryptPassword(string password)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(password);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider()
                {
                    Key = keys,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] result = transform.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(result, 0, result.Length);
                }
            }
        }

        public async Task<string> DecryptPassword(string password)
        {
            byte[] data = Convert.FromBase64String(password);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider()
                {
                    Key = keys,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] result = transform.TransformFinalBlock(data, 0, data.Length);
                    return UTF8Encoding.UTF8.GetString(result);
                }
            }
        }

        public async Task<bool> ValidatePassword(string normalPassword, string encryptedPassword)
        {
            string encryptedNormalPassword = await EncryptPassword(normalPassword);
            return encryptedNormalPassword == encryptedPassword;
        }

        public async Task<string> PasswordHasher(string password)
        {
            var passwordHasher = new PasswordHasher<object>();
            var hashedPassword = passwordHasher.HashPassword(null, password);
            return hashedPassword;
        }
        public async Task<PasswordVerificationResult> PasswordHashValidator(string hashedPassword, string password)
        {
            var passwordHasher = new PasswordHasher<object>();
            var result = passwordHasher.VerifyHashedPassword(null, hashedPassword, password);
            return result;

        }

    }
}