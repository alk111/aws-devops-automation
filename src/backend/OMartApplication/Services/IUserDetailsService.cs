using OMartApplication.Services.Wrapper;
using OMartDomain.Models.Seller.RequestAndResponce;
using OMartDomain.Models.User;
using OMartDomain.Models.User.RequestAndResponse;

namespace OMartApplication.Services
{
    public interface IUserDetailsService
    {

        //user creation methods
        Task<IResult<OtpGenerateResponce>> sendUserRegistrationOtpViaEmailAsync(OtpGenerateRequest otpGenerateRequest);

        Task<IResult<VerifyOtpWithEmailResponse>> verifyUserRegistrationrOtpWithEmailAsync(VerifyOtpWithEmailRequest verifyOtpWithEmailRequest);

        Task<IResult<UserCreationResponse>> createUserWithSecretAsync(UserCreationRequest userCreationRequest);
        //user creation methods



        //user reset password methods
        Task<IResult<OtpGenerateForResetPasswordResponse>> sendUserResetPasswordOtpViaUserEmailAsync(OtpGenerateRequest otpGenerateRequest);

        Task<IResult<VerifyOtpWithEmailResponse>> verifyUserResetPasswordOtpWithEmailAsync(VerifyOtpWithEmailRequest verifyOtpWithEmailRequest);

        Task<IResult<UserPasswordResetResponse>> userPasswordResetAsync(UserPasswordResetRequest userPasswordResetRequest);
        //user reset password methods



        //user update methods
        Task<IResult<UpdateUserLocationResponse>> updateUserLocationAsync(UpdateUserLocationRequest updateUserLocationRequest);
        //user update methods
        Task<IResult<GetUserDetailsResponce>> getUserDetailsByUserId(string Id);
        Task<IResult<bool>> updateUserDetailsByUserId(UpdateUserDetailsRequest updateDetails);

        //commons methods
        //commons methpds
    }
}