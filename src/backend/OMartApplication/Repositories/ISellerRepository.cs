using OMartApplication.Services.Wrapper;
using OMartDomain.Models.Seller;
using OMartDomain.Models.Seller.RequestAndResponce;
using OMartDomain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMartApplication.Repositories
{
    public interface ISellerRepository
    {
        Task<GetProductListbySellerResponse> GetAllProductsbySellersAsync(GetProductListbySellerRequest request);
        Task<AddProductResponse> AddProduct(AddProductRequest request);
        Task<AddProductResponse> UpdateProduct(UpdateProductRequest request);

        //seller
        Task<string> getUserIdByEmail(string email);
        Task<bool> checkUserIdExistInEntityDetailsTable(string userId);
        Task<UserDetailsResponce> getUserDetailsByUserId(string userId);
        Task<SellerRegistrationResponce> createSellerWithUserDetailsAsync(UserDetailsResponce userDetails, EstablishmentDetails establishmentDetails);
        Task<GetSellerDetailsResponce> getEntityDetailsBySellerId(string sellerId);
        Task<bool> updateSellerDetailsBySellerId(UpdateSellerDetails updateSellerDetails);
    }
}
