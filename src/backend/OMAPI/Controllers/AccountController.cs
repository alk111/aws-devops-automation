using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OMartApplication.Services;
using OMartDomain.Models.Account;
using OMartDomain.Models.Wrapper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
        {
            var response = await _authenticationService.AuthenticateAsync(request);
            if (response == null || response.Token == null)
                return Unauthorized();
            else 
                return Ok(response);
            //return BadRequest();
        }

       

        //[HttpPost("refresh-token")]
        //public async Task<IActionResult> RefreshTokenAsync(RefreshTokenRequest request)
        //{
        //    return Ok(await _authenticationService.RefreshTokenAsync(request));
        //}
        [HttpPost("revoke")]
        public async Task<IActionResult> RevokeRefreshToken(RevokeTokenRequest revokeRequest)
        {
            if (revokeRequest == null || string.IsNullOrEmpty(revokeRequest.Token))
            {
                return BadRequest("Invalid revoke request.");
            }

            // Revoke the existing refresh token
            await _authenticationService.RevokeAsync(revokeRequest);

            return Ok(new { Message = "Refresh token revoked successfully." });
        }

        [AllowAnonymous]
        [HttpPost("refreshAuth")]
        public async Task<IActionResult> AuthByRefreshToken(RefreshTokenRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Token))
            {
                return BadRequest("Invalid request.");
            }

           var result=  await _authenticationService.NewRefreshTokenAsync(request);

            return Ok( result);
        }



    }
}
