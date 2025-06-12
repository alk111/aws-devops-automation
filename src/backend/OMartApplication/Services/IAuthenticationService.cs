using OMartDomain.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OMartApplication.Services
{
    public  interface IAuthenticationService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<AuthenticationResponse> NewRefreshTokenAsync(RefreshTokenRequest refreshToken);
        Task<int> RevokeAsync(RevokeTokenRequest revokeTokenRequest);
        //ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
        //Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);
    }
}
