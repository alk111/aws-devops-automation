using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OMartApplication.Services;
using OMartDomain.Models.Seller;
using OMartDomain.Models.Seller.RequestAndResponce;
using OMartDomain.Models.User;
using OMartDomain.Models.User.RequestAndResponse;
using OMartDomain.Models.Wrapper;
using OMartInfra.Services;
using OMartInfra.Utility;

namespace OMAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class UserRegistration : ControllerBase
    {
        private readonly IUserDetailsService _iUserDetailsService;

        public UserRegistration(IUserDetailsService iUserDetailsService)
        {
            _iUserDetailsService = iUserDetailsService;
        }

        [HttpPost("EmailRegistration")]
        public async Task<IActionResult> EmailRegistration(OtpGenerateRequest otpGenerateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _iUserDetailsService.sendUserRegistrationOtpViaEmailAsync(otpGenerateRequest);
                if (result.Succeeded)
                {
                    return Ok(result);
                    //return Ok(new { Message = "OTP generated and sent successfully.", IsSent = true });
                }
                else
                {
                    return Conflict(result);
                    //return Conflict(new { Message = "Email Address already exists.", IsSent = false });
                }
            }
            catch (Exception ex)
            {
                // return StatusCode(500, Result<string>.Fail($"An error occurred : {ex.Message}"));
                return StatusCode(500, ex.Message.AsFailure());
            }
        }

        [HttpPost("ResetPasswrodEmail")]
        public async Task<IActionResult> ResetPasswrodEmail(OtpGenerateRequest otpGenerateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _iUserDetailsService.sendUserResetPasswordOtpViaUserEmailAsync(otpGenerateRequest);
                if (result.Succeeded)
                {
                    return Ok(result);
                    //return Ok(new { Message = "OTP generated and sent successfully.", IsSent = true });
                }
                else
                {
                    return Conflict(result);
                    //return Conflict(new { Message = "Email Address already exists.", IsSent = false });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Result<string>.Fail($"An error occurred : {ex.Message}"));
            }
        }

        [HttpPost("VerifyOTP")]
        public async Task<IActionResult> VerifyOtp(VerifyOtpWithEmailRequest verifyOtpWithEmailRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _iUserDetailsService.verifyUserRegistrationrOtpWithEmailAsync(verifyOtpWithEmailRequest);

                if (result.Succeeded)
                {
                    return Ok(result);
                    //return Ok(new { Message = "OTP verified successfully.", IsVerified = true });
                }
                else
                {
                    return Conflict(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpPost("VerifyOTPForUserPasswordReset")]
        public async Task<IActionResult> VerifyOTPForUserPasswordReset(VerifyOtpWithEmailRequest verifyOtpWithEmailRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _iUserDetailsService.verifyUserResetPasswordOtpWithEmailAsync(verifyOtpWithEmailRequest);

                if (result.Succeeded)
                {
                    return Ok(result);
                    //return Ok(new { Message = "OTP verified successfully.", IsVerified = true });
                }
                else
                {
                    return Conflict(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpPost("UserRegisteration")]
        public async Task<IActionResult> UserRegisteration(UserCreationRequest userCreationRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _iUserDetailsService.createUserWithSecretAsync(userCreationRequest);

                if (result.Succeeded)
                {
                    return Ok(result);
                }
                else
                {
                    return Conflict(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Result<string>.Fail($"An error occurred : {ex.Message}"));
                //return StatusCode(500, new { Message = $"Internal server error: {ex.Message}", IsRegistered = false });
            }
        }

        [HttpPost("ResetUserPassword")]
        public async Task<IActionResult> ResetUserPassword(UserPasswordResetRequest userPasswordResetRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _iUserDetailsService.userPasswordResetAsync(userPasswordResetRequest);

                if (result.Succeeded)
                {
                    return Ok(result);
                }
                else
                {
                    return Conflict(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Result<string>.Fail($"An error occurred : {ex.Message}"));
                //return StatusCode(500, new { Message = $"Internal server error: {ex.Message}", IsRegistered = false });
            }
        }

        [HttpPost("UpdateUserLocation")]
        public async Task<IActionResult> UpdateUserLocation(UpdateUserLocationRequest updateUserLocationRequest)
        {
            try
            {
                var result = await _iUserDetailsService.updateUserLocationAsync(updateUserLocationRequest);
                return result.Succeeded ? Ok(result) : Conflict(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Result.Fail($"An error occurred : {ex.Message}"));
            }
        }

        [AllowAnonymous]
        [HttpPost("UpdateUserDetailsByUserId")]
        public async Task<IActionResult> UpdateUserDetailsBySellerId(UpdateUserDetailsRequest updateDetails)
        {

            var response = await _iUserDetailsService.updateUserDetailsByUserId(updateDetails);
            return response.Succeeded ? Ok(response) : BadRequest(response);
        }

        [AllowAnonymous]
        [HttpGet("GetUserDetailsByUserId")]
        public async Task<IActionResult> GetsellerDetailsBySellerId(string? Id)
        {

            var response = await _iUserDetailsService.getUserDetailsByUserId(Id);

            return response.Succeeded ? Ok(response) : NotFound(response);

        }
    }
}