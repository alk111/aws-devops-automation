using Dapper;
using Microsoft.Extensions.Configuration;
using OMartApplication.Repositories;
using OMartDomain.Common;
using OMartDomain.Models.Account;
using OMartDomain.Models.Demo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OMartInfra.Repositories.AccountRepository;

namespace OMartInfra.Repositories
{
    public class AccountRepository : CentralRepository, IAccountRepository
    {
        private readonly string? _connectionString;

        public AccountRepository(IConfiguration configuration) : base(configuration, "OMartDevDbConnection")
        {
            _connectionString = configuration.GetConnectionString(name: "OMartDevDbConnection");
        }
        public async Task<UserDetailsResponse> GetUserbyEmailorMobileAsync(GetUserByEmailorMobileNumber request)
        {
            var parameters = new
            {
                Email = request.Email,
                MobileNumber = request.MobileNumber,
            };
            var result = await ExecuteQueryAsync<UserDetailsResponse>(SPConstant.GetUserByEmailorMobileNumber, parameters);
            return result;
        }
        public async Task<UserDetailsResponse> AuthbyEmailorMobileAsync(AuthenticationRequest request)
        {
            var parameters = new
            {
                Email = request.Email,
                passW = request.Password,
                MobileNumber = request.MobileNumber,
            };
            var result = await ExecuteQueryAsync<UserDetailsResponse>(SPConstant.AuthByEmailAndPassword, parameters);
            return result;
        }

        //public Task<AddRefreshTokenResponse> RefreshTokenAsync(AddRefreshTokenRequest request)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<int> AddRefreshTokenAsync(AddRefreshTokenRequest refreshToken)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_RefreshToken", refreshToken.RefreshToken);
                parameters.Add("p_Expires", refreshToken.Expires);
                parameters.Add("p_Created", refreshToken.Created);
                parameters.Add("p_CreatedByIp", refreshToken.CreatedByIp);
                parameters.Add("p_user_id", refreshToken.UserId);
                var result = await ExecuteQueryAsync<dynamic>(SPConstant.AddRefreshToken, parameters);
                //await _dbConnection.ExecuteAsync("AddRefreshToken", parameters, commandType: CommandType.StoredProcedure);

                return 1;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> RevokeRefreshTokenAsync(RevokeTokenRequest request)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_RefreshToken",request.Token);
                parameters.Add("p_Revoked", DateTime.UtcNow); // Set the current time as revoked time
                parameters.Add("p_RevokedByIp", request.RevokedByIp);
                parameters.Add("p_ReasonRevoked", request.Reason);
                var result = await ExecuteQueryAsync<dynamic>(SPConstant.RevokeRefreshToken, parameters);

                return 1;
            }
            catch (Exception ex)
            {

                return 0;
            }
            //await _dbConnection.ExecuteAsync("RevokeRefreshToken", parameters, commandType: CommandType.StoredProcedure);
        }
        public Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetRefreshTokenResponse> GetUserRefreshTokensAsync(string userId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_UserId", userId);

                var result = await ExecuteQueryAsync<GetRefreshTokenResponse>(SPConstant.GetUserRefreshTokens, parameters);

                return result;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        //public async Task<IEnumerable<Persons>> GetPersonsDataAsync()
        //{
        //    var temp = await ExecuteQueryAsync<Persons>(SPConstant.GetUserByEmailorMobileNumber, null);
        //    return temp;
        //}
    }
}
