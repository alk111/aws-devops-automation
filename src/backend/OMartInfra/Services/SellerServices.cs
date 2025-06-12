using OMartApplication.Repositories;
using OMartApplication.Services;
using OMartApplication.Services.Wrapper;
using OMartDomain.Models.Account;
using OMartDomain.Models.Order;
using OMartDomain.Models.Seller;
using OMartDomain.Models.Seller.RequestAndResponce;
using OMartDomain.Models.User;
using OMartDomain.Models.User.RequestAndResponse;
using OMartDomain.Models.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMartInfra.Services
{
    public class SellerServices : ISellerServices
    {
        private readonly ISellerRepository sellerRepository;
        private readonly IEmailService iEmailService;
        private readonly IProductService productService;
        private readonly IOTPService iOPTService;

        public SellerServices(ISellerRepository sellerRepository, IOTPService _iOPTService, IEmailService _iEmailService, IProductService productService)
        {
            this.sellerRepository = sellerRepository;
            iOPTService = _iOPTService;
            iEmailService = _iEmailService;
            this.productService = productService;
        }


        public async Task<GetProductListbySellerResponse> GetAllProductsbySellersAsync(GetProductListbySellerRequest request)
        {
            var resultDetails = await sellerRepository.GetAllProductsbySellersAsync(request);
            return resultDetails;
        }

        public async Task<AddProductResponse> AddProduct(AddProductRequest request)
        {
            var resultAddProduct = await sellerRepository.AddProduct(request);
            return resultAddProduct;
        }

        public async Task<AddProductResponse> UpdateProduct(UpdateProductRequest request)
        {
            var product = await productService.getProductDeatils(request.product_id);
            if (product.Succeeded)
            {
                //if(product.Data.productDetails.added_on.Date == )
                //request.added_on = product.Data.productDetails.added_on;
                //request.updated_on = DateTime.Now;
                if (product.Data.productDetails.iterations == null)
                    request.iterations = 2;
                else
                {

                    request.iterations = product.Data.productDetails.iterations + 1;
                }
                var resultupdateProduct = await sellerRepository.UpdateProduct(request);
                return resultupdateProduct;
            }
            else
            {
                return null;
            }
        }

        //seller

        public async Task<IResult<SellerEmailVerificationResponce>> sendSellerOtpViaEmailAsync(OtpGenerateRequest otpGenerateRequest)
        {
            try
            {

                string user_id = await sellerRepository.getUserIdByEmail(otpGenerateRequest.email);

                if (user_id == null)
                {
                    return Result<SellerEmailVerificationResponce>.Fail("User with the given Email is not found please first create user account and craete seller account");
                }

                bool isSellerAlreadyExistInEntityTable = await sellerRepository.checkUserIdExistInEntityDetailsTable(user_id);

                if (isSellerAlreadyExistInEntityTable)
                {
                    return Result<SellerEmailVerificationResponce>.Fail("Seller with the given email already exist in the entity table");
                }

                OtpGenerateResponce responce = await iOPTService.generateAndStoreOtpAsync(otpGenerateRequest);

                string otp = responce.otp.ToString();

                var emailResult = await iEmailService.SendSellerRegistrationEmailAsync(otpGenerateRequest.email, otp);

                if (!emailResult)
                {
                    return Result<SellerEmailVerificationResponce>.Fail("Error while sending the email.");
                }


                return (responce.otp > 0) ? Result<SellerEmailVerificationResponce>.Success("Otp has send to the seller email") : Result<SellerEmailVerificationResponce>.Fail("An error occur while storing otp and email data into central otp");

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occur while sending the otp to seller email in seller service {ex.Message}");
            }
        }

        public async Task<IResult<VerifyOtpWithEmailResponse>> verifySellerOtpWithEmailAsync(VerifyOtpWithEmailRequest verifyOtpWithEmailRequest)
        {
            try
            {

                VerifyOtpWithEmailResponse verifySellerOtpWithEmailResponse = await iOPTService.GetVerificationStatusAsync(verifyOtpWithEmailRequest);

                if (verifySellerOtpWithEmailResponse.is_expired)
                {
                    return Result<VerifyOtpWithEmailResponse>.Fail(verifySellerOtpWithEmailResponse.verificationMessage);
                }

                return Result<VerifyOtpWithEmailResponse>.Success(verifySellerOtpWithEmailResponse);

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occur while verifing the otp with email in seller service {ex.Message}");
            }
        }

        public async Task<IResult<SellerRegistrationResponce>> CreateSellerWithUserIdAsync(SellerRegistrationRequest sellerRegistrationRequest)
        {
            try
            {
                List<string> messages = new List<string>();

                //first getting user details
                UserDetailsResponce getUserDetails = await sellerRepository.getUserDetailsByUserId(sellerRegistrationRequest.user_id);

                if (getUserDetails == null)
                {
                    messages.Add("User with the given User id is not found");
                    return Result<SellerRegistrationResponce>.Fail(messages);
                }
                messages.Add("User with the given id found");

                bool IsEmailVerifiedByOtp = await iOPTService.IsEmailVerifiedByOtpForLetestRecordAsync(getUserDetails.contact_email);

                if (!IsEmailVerifiedByOtp)
                {
                    messages.Add("Seller email is not verified");
                    return Result<SellerRegistrationResponce>.Fail(messages);
                }

                messages.Add("Sellers email is verified with the otp");

                //In user-details user is active or not
                if (!getUserDetails.is_active)
                {
                    messages.Add("User with id is not active");
                    return Result<SellerRegistrationResponce>.Fail(messages);
                }

                messages.Add("User with the given id is active currently");

                //For checking any userId already exist in entity table
                var isExist = await sellerRepository.checkUserIdExistInEntityDetailsTable(sellerRegistrationRequest.user_id);

                if (isExist)
                {
                    messages.Add("Seller with Given Id Already Exist");
                    return Result<SellerRegistrationResponce>.Fail(messages);
                }

                messages.Add("Seller with the given user id is not present in the entity table");

                EstablishmentDetails establishmentDetails = new EstablishmentDetails();

                establishmentDetails.establishment_type = sellerRegistrationRequest.CompanyType;
                establishmentDetails.establishment_name = sellerRegistrationRequest.CompanyName;
                establishmentDetails.address = sellerRegistrationRequest.address;
                establishmentDetails.contact_no_2 = sellerRegistrationRequest.contactPhoneNumber;
                //establishmentDetails. = sellerRegistrationRequest.PhoneNumber;



                var creationResult = await sellerRepository.createSellerWithUserDetailsAsync(getUserDetails, establishmentDetails);

                messages.Add(creationResult.message);

                return creationResult.message.Contains("successfully")
                ?
                Result<SellerRegistrationResponce>.Success(creationResult, messages)
                :
                Result<SellerRegistrationResponce>.Fail(messages);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occur while creating the seller in seller service {ex.Message}");
            }
        }

        public async Task<IResult<GetSellerDetailsResponce>> getSellerDetailsBySellerId(string sellerId)
        {

            try
            {

                var serllerdetails = await sellerRepository.getEntityDetailsBySellerId(sellerId);

                return serllerdetails != null ? Result<GetSellerDetailsResponce>.Success(serllerdetails)
                :
                Result<GetSellerDetailsResponce>.Fail("Something went wrong");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occur while getting the seller details in seller service {ex.Message}");
            }
        }
        public async Task<IResult<bool>> updateSellerDetailsBySellerId(UpdateSellerDetails updateSellerDetails)
        {

            try
            {

                var isUpdated = await sellerRepository.updateSellerDetailsBySellerId(updateSellerDetails);

                return isUpdated ? Result<bool>.Success(true)
                :
                Result<bool>.Fail("Something went wrong");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occur while getting the seller details in seller service {ex.Message}");
            }
        }

        //seller
    }
}
