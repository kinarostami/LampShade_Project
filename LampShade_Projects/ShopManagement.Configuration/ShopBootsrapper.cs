using _0_Framework.Infrastucture;
using _01_LampshadeQuery.Contracts;
using _01_LampshadeQuery.Contracts.Product;
using _01_LampshadeQuery.Contracts.ProductCategory;
using _01_LampshadeQuery.Contracts.Slide;
using _01_LampshadeQuery.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Configuration.Permissions;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.Services;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.Infrastucture.AccountAcl;
using ShopManagement.Infrastucture.EfCore;
using ShopManagement.Infrastucture.EFCore.Repository;
using ShopManagement.Infrastucture.InventoryAcl;

namespace ShopManagement.Configuration
{
    public class ShopBootstrap
    {
        public static void Configuration(IServiceCollection service,string conectionstring)
        {
            //Category
            service.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            service.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();

            //Product
            service.AddTransient<IProductApplication, ProductApplication>();
            service.AddTransient<IProductRepository, ProductRepository>();

            //ProductPicture
            service.AddTransient<IProductPictureApplication, ProductPictureApplication>();
            service.AddTransient<IProductPictureRepository, ProductPictureRepository>();

            //Slide
            service.AddTransient<ISlideApplication, SlideApplication>();
            service.AddTransient<ISlideRepository, SlideRepository>();
            
            //SlideQuery
            service.AddTransient<ISlideQuery, SlideQuery>();
            
            //ProductCategoryQuery
            service.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();

            //ProductQuery
            service.AddTransient<IProductQuery, ProductQuery>();

            // Permission
            service.AddTransient<IPermissionExposer, ShopPermissionExposer>();

            // CheckOut in Cart
            service.AddTransient<ICartCalculatorService, CartCalculatorService>();
            
            // Order
            service.AddTransient<IOrderApplication, OrderApplication>();
            service.AddTransient<IOrderRepository, OrderRepository>();

            // CartService
            service.AddSingleton<ICartService, CartService>();
            
            // ShopInventoryAcl
            service.AddTransient<IShopInventoryAcl, ShopInventoryAcl>();
            
            // ShopAccountAcl
            service.AddTransient<IShopAccountAcl, ShopAccountAcl>();

            service.AddDbContext<ShopContext>(x =>
                x.UseSqlServer(conectionstring));
        }
    }
}
