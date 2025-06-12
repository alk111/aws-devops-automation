using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mysqlx.Session;
using OMartApplication.Repositories;
using OMartApplication.Services;
using OMartApplication.Services.Wrapper;
using OMartDomain.Models.Account;
using OMartDomain.Models.Utility;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OMartInfra.Services.Account
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountRepository accountRepo;
        private readonly IPasswordEncryptionService hashService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly JwtSettings jwtOptions;

        public AuthenticationService(IAccountRepository accountRepo, IPasswordEncryptionService hashService, IOptions<JwtSettings> jwtOptions, IHttpContextAccessor httpContextAccessor)
        {
            this.accountRepo = accountRepo;
            this.hashService = hashService;
            this.httpContextAccessor = httpContextAccessor;
            this.jwtOptions = jwtOptions.Value;
        }
        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {

            AuthenticationResponse response = new AuthenticationResponse { };
            GetUserByEmailorMobileNumber getUserByEmailorMN = new GetUserByEmailorMobileNumber { Email = request.Email, MobileNumber = request.MobileNumber };
            //check user exist
            var userDetails = await accountRepo.GetUserbyEmailorMobileAsync(getUserByEmailorMN);

            if (userDetails == null)
            {
                return null;
            }
            //userDetails = await accountRepo.AuthbyEmailorMobileAsync(request);
            //if no
            PasswordVerificationResult status = await hashService.PasswordHashValidator(userDetails.Secret, request.Password);
            if (status.ToString() == "Failed")
            {
                response.Token = null;
                return response;
            }


            if (userDetails.Contact_Email != null && status.ToString() == "Success")
            {
                string authToken = GenerateToken(userDetails);
                string refreshToken = GenerateRefreshToken();

                var addRefreshTokenRequest = new AddRefreshTokenRequest
                {
                    RefreshToken = refreshToken,
                    UserId = userDetails.user_id,
                    //Expires = DateTime.UtcNow.AddDays(jwtOptions.RTDurationInDays),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    CreatedByIp = GetClientIp()
                };


                var result = await accountRepo.AddRefreshTokenAsync(addRefreshTokenRequest);

                if (result == 1)
                {

                    response.Token = authToken;
                    response.RefreshToken = refreshToken;
                    response.RefreshTokenExpiration = addRefreshTokenRequest.Expires;
                }
                return response;
            }

            return response;
        }



        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        public string GenerateToken(UserDetailsResponse person)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            ClaimsIdentity claims = new ClaimsIdentity();

            //claims.AddClaim(new Claim(nameof(ClaimTypes.PrimarySid), person.user_id.ToString()));
            claims.AddClaim(new Claim(nameof(ClaimTypes.NameIdentifier), person.user_id.ToString()));
            claims.AddClaim(new Claim(nameof(ClaimTypes.Name), person.First_Name));
            claims.AddClaim(new Claim(nameof(ClaimTypes.Surname), person.last_name));
            claims.AddClaim(new Claim(nameof(ClaimTypes.Email), person.Contact_Email));
            if (/*person.is_seller &&*/ !person.entity_id.IsNullOrEmpty())
            {
                claims.AddClaim(new Claim("sellerID", person.entity_id));
                claims.AddClaim(new Claim(nameof(ClaimTypes.Role), "Seller"));
            }
            else
            {
                claims.AddClaim(new Claim(nameof(ClaimTypes.Role), "Buyer"));

            }
            //This should be in secret storage
            byte[] jwtKey = Encoding.ASCII.GetBytes(jwtOptions.Key);

            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(jwtOptions.DurationInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(jwtKey),
                SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(TokenDescriptor);
            return tokenHandler.WriteToken(token);

        }

        public string GetClientIp()
        {
            var ipAddress = httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();

            // Check for X-Forwarded-For if behind a proxy
            if (httpContextAccessor.HttpContext?.Request.Headers.ContainsKey("X-Forwarded-For") == true)
            {
                ipAddress = httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"];
            }

            return ipAddress;
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }



        public async Task<AuthenticationResponse> NewRefreshTokenAsync(RefreshTokenRequest refreshToken)
        {
            var ip = GetClientIp();
            var principal = GetPrincipalFromExpiredToken(refreshToken.Token);
            if (principal == null)
            {
                return null;
            }

            var claims = principal.Claims.Select(c => new
            {
                c.Type,
                c.Value
            }).ToList();


            string ID = "";
            foreach (var claim in claims) { if (claim.Type == "NameIdentifier") { ID = claim.Value; } }

            var user = await accountRepo.GetUserRefreshTokensAsync(ID);

            // add IP Address Check TBD

            if (user == null || user.RefreshToken != refreshToken.RefreshToken || user.Expires <= DateTime.UtcNow)
            {
                return null;
            }
            GetUserByEmailorMobileNumber getUserByEmailorMN = new GetUserByEmailorMobileNumber { Email = user.contact_email, MobileNumber = "1" };
            var userDetails = await accountRepo.GetUserbyEmailorMobileAsync(getUserByEmailorMN);

            var newAccessToken = GenerateToken(userDetails);
            var newRefreshToken = GenerateRefreshToken();

            //user.RefreshToken = newRefreshToken;

            var addRefreshTokenRequest = new AddRefreshTokenRequest
            {
                RefreshToken = newRefreshToken,
                UserId = userDetails.user_id,
                //Expires = DateTime.UtcNow.AddDays(jwtOptions.RTDurationInDays),
                Expires = DateTime.UtcNow.AddMinutes(5),
                CreatedByIp = ip
            };
            var result = await accountRepo.AddRefreshTokenAsync(addRefreshTokenRequest);

            if (result == 1)
            {
                AuthenticationResponse response = new AuthenticationResponse
                {

                    Token = newAccessToken,
                    RefreshToken = newRefreshToken,
                    RefreshTokenExpiration = addRefreshTokenRequest.Expires
                };

                var test = response;
                return test;
            }
            return null;
        }

        public async Task<int> RevokeAsync(RevokeTokenRequest request)
        {
            var result = await accountRepo.RevokeRefreshTokenAsync(request);
            if (result == 0) { return 0; }
            return result;
            
        }
    }
}
