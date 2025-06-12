using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMartDomain.Common
{
    public static class SPConstant
    {
        public const string GetUserByEmailorMobileNumber = "openmart_dev.GetUserByEmailorMobileNumber";
        public const string AuthByEmailAndPassword = "openmart_dev.AuthByEmailAndPassword";
        public const string GetSellerDetailsBySellerId = "openmart_dev.GetSellerDetailsBySellerId";
        public const string UpdateSellerDetailsBySellerId = "openmart_dev.UpdateSellerDetailsBySellerId";
        //Account 
        public const string InsertOtpForEmailVerification = "openmart_dev.InsertOtpForEmailVerification";
        public const string IsEmailVerifiedByOtp = "openmart_dev.IsEmailVerifiedByOtp";
        public const string VerifyOtp = "openmart_dev.VerifyOtp";
        public const string InsertUserWithSecrets = "openmart_dev.InsertUserWithSecrets";
        //User
        public const string GetUserDetailsByUserId = "openmart_dev.GetUserDetailsByUserId";
        public const string UpdateUserDetails = "openmart_dev.UpdateUserDetails";
        //Entity
        public const string InsertEntityDetails = "openmart_dev.InsertEntityDetails";
        public const string CheckUserIdExistInEntityDetails = "openmart_dev.CheckUserIdExistInEntityDetails";
        public const string UpdateUserPasswordByEmailAndOtp = "openmart_dev.UpdateUserPasswordByEmailAndOtp";
        public const string GetLatestEntryByEmailForUserPasswordReset = "openmart_dev.GetLatestEntryByEmailForUserPasswordReset";
        public const string GetUserIdByEmail = "openmart_dev.GetUserIdByEmail";

        public const string GetUserActiveStatusByEmail = "openmart_dev.GetUserActiveStatusByEmail";
        public const string GetUserActiveStatusByUserId = "openmart_dev.GetUserActiveStatusByUserId";
        public const string UpdateUserLocation = "openmart_dev.UpdateUserLocation";

        //Products
        public const string GetAllProductsbySellers = "openmart_dev.getAllProductsbySellers";
        public const string InsertProduct = "openmart_dev.InsertProduct";
        public const string EditProduct = "openmart_dev.EditProduct";
        public const string SearchProductsByName = "openmart_dev.SearchProductsByName";

        public const string GetProductDetailsById = "openmart_dev.GetProductDetailsById";
        public const string GetProductsByCategory = "openmart_dev.GetProductsByCategory";
        public const string SearchProductsFilter = "openmart_dev.SearchProductsFilter";
        public const string updateCartproductQuantity = "openmart_dev.updateCartproductQuantity";
        //RemoveFromCart 
        public const string RemovefromcartByUserIdNadproductId = "openmart_dev.RemovefromcartByUserIdNadproductId";
        public const string AddCart = "openmart_dev.AddCart";
        public const string GetCartData = "openmart_dev.GetCartData";

        //Order
        public const string AddMultipleOrders = "openmart_dev.AddMultipleOrders";
        public const string AddOrderWithItems = "openmart_dev.AddOrderWithItems";

        //Book Marks
        public const string InsertBookmark = "openmart_dev.InsertBookmark";
        public const string UpdateBookmark = "openmart_dev.UpdateBookmark";
        public const string GetBookmarksbyUserID = "openmart_dev.GetBookmarksbyUserID";
        public const string DeleteBookmark = "openmart_dev.DeleteBookmark";

        //category
        public const string GetAllProductCategories = "openmart_dev.GetAllProductCategories";
        public const string GetCategoryByCategoriId = "openmart_dev.GetCategoryByCategoriId";

        //SAmple products
        public const string insertSampleProduct = "openmart_dev.insertSampleProduct";
        public const string updateSampleProductDetails = "openmart_dev.updateSampleProductDetails";
        public const string GetSampleProductBySampleProdID = "openmart_dev.GetSampleProductBySampleProdID";
        public const string deleteSampleProductDetails = "openmart_dev.deleteSampleProductDetails";
        public const string InsertSampleProductField = "openmart_dev.InsertSampleProductField";
        public const string UpdateSampleProductField = "openmart_dev.UpdateSampleProductField";
        public const string GetBySampleProdOptionalFieldID = "openmart_dev.GetBySampleProdOptionalFieldID";
        public const string DeleteBySampleProdOptionalFieldID = "openmart_dev.DeleteBySampleProdOptionalFieldID";

        //Seeler delivery
        public const string AddDeliveryTimeSlot = "openmart_dev.AddDeliveryTimeSlot";
        public const string GetDeliveryTimeSlotsByEntityID = "openmart_dev.GetDeliveryTimeSlotsByEntityID";
        public const string DeleteEnDeliveryTimeSlot = "openmart_dev.DeleteEnDeliveryTimeSlot";
        public const string UpdateEnDelTimeSlot = "openmart_dev.UpdateEnDelTimeSlot";

        //Product Models
        public const string AddProductModel = "openmart_dev.AddProductModel";
        public const string UpdateProductModel = "openmart_dev.UpdateProductModel";
        public const string GetProductModelsByProductID = "openmart_dev.GetProductModelsByProductID";
        public const string DeleteProductModel = "openmart_dev.DeleteProductModel";

        //Order List
        public const string InsertOrderList = "openmart_dev.InsertOrderList";
        public const string GetOrderListByOrderID = "openmart_dev.GetOrderListByOrderID";
        public const string UpdateOrderList = "openmart_dev.UpdateOrderList";
        public const string GetOrderwithItemsbyOrderID = "openmart_dev.GetOrderwithItemsbyOrderID";
        public const string GetOrderwithItemsforBuyerbyOrderID = "openmart_dev.GetOrderwithItemsforBuyerbyOrderID";


        //Product Images
        public const string AddMultipleProductImages = "openmart_dev.AddMultipleProductImages";
        public const string GetProductImages = "openmart_dev.GetProductImages";

        public const string GetOrderwithItemsbyUserId = "openmart_dev.GetOrderwithItemsbyUserId";
        public const string GetOrderbySellerID = "openmart_dev.GetOrderbySellerID";

        public const string removefromcartByCartId = "openmart_dev.removefromcartByCartId";


        //Refresh Tokens

        public const string AddRefreshToken = "openmart_dev.AddRefreshToken";
        public const string RevokeRefreshToken = "openmart_dev.RevokeRefreshToken";
        public const string GetUserRefreshTokens = "openmart_dev.GetUserRefreshTokens";
        public const string InsertUserAddress = "openmart_dev.InsertUserAddress";
        public const string GetUserAddressbyUserID = "openmart_dev.GetUserAddressbyUserID";
        public const string UpdateUserAddress = "openmart_dev.UpdateUserAddress";
        public const string DeleteUserAddress = "openmart_dev.DeleteUserAddress";
    }
}
