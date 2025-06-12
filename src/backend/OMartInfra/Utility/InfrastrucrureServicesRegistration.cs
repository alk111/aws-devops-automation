using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OMartApplication.Repositories;
using OMartApplication.Services;
using OMartInfra.Repositories;
using OMartInfra.Services;
using OMartInfra.Services.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMartInfra.Utility
{
    public static class InfrastrucrureServicesRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Account
            //services.AddTransient<IAccountRepository, AccountRepository>();
            //services.AddTransient<IAccountServices, AccountServices>();
            //services.AddTransient<IWinAuthServices, WinAuthServices>();


            services.AddTransient<IOTPService, OTPService>();
            services.AddTransient<IOTPRepository, OTPRepository>();

            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IPasswordEncryptionService, PasswordEncryptionService>();

            services.AddTransient<IUserDetailsRepository, UserDetailsRepository>();
            services.AddTransient<IUserDetailsService, UserDetailsService>();

            //Test Services
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IAccountRepository, AccountRepository>();

            //Seller
            services.AddTransient<ISellerServices, SellerServices>();
            services.AddTransient<ISellerRepository, SellerRepository>();

            //Product
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductService, ProductService>();

            services.AddTransient<ICartRepository, CartRepositroy>();
            services.AddTransient<ICartService, CartService>();
            
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderService, OrderService>();

            services.AddTransient<IFileRepository, FileRepository>();
            services.AddTransient<IFileServices, FileServices>();

            services.AddTransient<IBookMarkRepository, BookMarkRepository>();
            services.AddTransient<IBookMarkService,BookMarkService>();

            services.AddTransient<ICategorieRepository, CategorieRepository>();
            services.AddTransient<ICategoriesService,CategorieService>();

            services.AddTransient<ISampleProductDetailsService, SampleProductDetailsService>();
            services.AddTransient<ISampleProductRepository,SampleProductDetailsRepository>();

            services.AddTransient<ISampleProdFieldsRepository,SampleProdFeildsRepository>();
            services.AddTransient<ISampleProdFieldsService,SampleProdFieldsService>();

            services.AddTransient<IEnDelTimeSlotRepository, EnDelTimeSlotRepository>();
            services.AddTransient<IEnDelTimeSlotService,EnDelTimeSlotService>();

            services.AddTransient<IProductModelsService,ProductModelsService>();
            services.AddTransient<IAddProductModelRepository,AddProductModelRepository>();

            services.AddTransient<IUserAddressService,UserAddressService>();
            services.AddTransient<IUserAddressRepository,UserAddressRepository>();

        }
    }
}
