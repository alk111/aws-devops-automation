using OMartApplication.Services.Wrapper;
using OMartDomain.Models.Account;
using OMartDomain.Models.Seller;
using OMartDomain.Models.Seller.RequestAndResponce;
using OMartDomain.Models.User;
using OMartDomain.Models.User.RequestAndResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMartApplication.Services
{
    public interface ISellerServices
    {
        Task<GetProductListbySellerResponse> GetAllProductsbySellersAsync(GetProductListbySellerRequest request);
        Task<AddProductResponse> AddProduct(AddProductRequest request);
        Task<AddProductResponse> UpdateProduct(UpdateProductRequest request);

        //seller
        Task<IResult<SellerEmailVerificationResponce>> sendSellerOtpViaEmailAsync(OtpGenerateRequest otpGenerateRequest);
        Task<IResult<VerifyOtpWithEmailResponse>> verifySellerOtpWithEmailAsync(VerifyOtpWithEmailRequest verifyOtpWithEmailRequest);
        Task<IResult<SellerRegistrationResponce>> CreateSellerWithUserIdAsync(SellerRegistrationRequest sellerRegistrationRequest);
        Task<IResult<GetSellerDetailsResponce>> getSellerDetailsBySellerId(string sellerId);
        Task<IResult<bool>> updateSellerDetailsBySellerId(UpdateSellerDetails updateSellerDetails);

    }
}
