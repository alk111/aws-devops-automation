using OMartDomain.Models;
using OMartDomain.Models.Account;
using OMartDomain.Models.Demo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMartApplication.Repositories
{
    public interface IAccountRepository
    {
        Task<UserDetailsResponse> GetUserbyEmailorMobileAsync(GetUserByEmailorMobileNumber request);
        Task<UserDetailsResponse> AuthbyEmailorMobileAsync(AuthenticationRequest request);
        Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest request);
        Task<int> AddRefreshTokenAsync(AddRefreshTokenRequest refreshToken);
        Task<int> RevokeRefreshTokenAsync(RevokeTokenRequest request);
        Task<GetRefreshTokenResponse> GetUserRefreshTokensAsync(string userId);
    }
}
