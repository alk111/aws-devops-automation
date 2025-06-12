using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OMartApplication.Services;
using OMartDomain.Models.Account;
using OMartDomain.Models.Seller;
using OMartDomain.Models.Seller.RequestAndResponce;
using OMartDomain.Models.User;
using OMartDomain.Models.User.RequestAndResponse;
using OMartDomain.Models.Wrapper;
using OMartInfra.Services;
using OMartInfra.Utility;

namespace OMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class SellerController : ControllerBase
    {
        private readonly ISellerServices sellerServices;

        public SellerController(ISellerServices sellerServices)
        {
            this.sellerServices = sellerServices;
        }


        [HttpPost("GetAllProductsbySellers")]
        public async Task<ActionResult<GetProductListbySellerResponse>> GetAllProductsbySellersAsync(GetProductListbySellerRequest request)
        {
            var result = await sellerServices.GetAllProductsbySellersAsync(request);
            return Ok(result.AsSuccess("Success"));
        }



        [HttpPost("AddProducts")]
        public async Task<AddProductResponse> AddProduct(AddProductRequest request)
        {
            return await sellerServices.AddProduct(request);
        }

        [AllowAnonymous]
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(UpdateProductRequest request)
        {
            var response = await sellerServices.UpdateProduct(request);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("UpdateSellerDetailsBySellerId")]
        public async Task<IActionResult> UpdateSellerDetailsBySellerId(UpdateSellerDetails updateSellerDetails)
        {

            var response = await sellerServices.updateSellerDetailsBySellerId(updateSellerDetails);
            return response.Succeeded ? Ok(response) : BadRequest(response);
        }

        [AllowAnonymous]
        [HttpGet("GetsellerDetailsBySellerId")]
        public async Task<IActionResult> GetsellerDetailsBySellerId(string? sellerId)
        {

            var response = await sellerServices.getSellerDetailsBySellerId(sellerId);

            return response.Succeeded ? Ok(response) : NotFound(response);

        }

        [HttpPost("SellerLoginEmail")]
        public async Task<IActionResult> SellerLoginEmail(OtpGenerateRequest otpGenerateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await sellerServices.sendSellerOtpViaEmailAsync(otpGenerateRequest);
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
                return StatusCode(500, Result<string>.Fail($"An error occurred in seller controller: {ex.Message}"));
            }
        }

        [HttpPost("VerifyOtpForSellerEmail")]
        public async Task<IActionResult> VerifyOtpForSellerEmail(VerifyOtpWithEmailRequest verifyOtpWithEmailRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await sellerServices.verifySellerOtpWithEmailAsync(verifyOtpWithEmailRequest);

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
                return StatusCode(500, new { Message = $"Internal server error in seller controller: {ex.Message}" });
            }
        }

        [HttpPost("SellerRegestration")]
        public async Task<IActionResult> SellerRegisteration(SellerRegistrationRequest sellerRegistrationRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await sellerServices.CreateSellerWithUserIdAsync(sellerRegistrationRequest);

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
                return StatusCode(500, Result<string>.Fail($"An error occurred in seller controller: {ex.Message}"));
            }
        }
    }
}
