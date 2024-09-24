using _0_Framework.Infrastucture;
using _01_LampshadeQuery.Contracts.Inventory;
using _01_LampshadeQuery.Query;
using InventoryManagement.Application;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastucture.Configuration.Permission;
using InventoryManagement.Infrastucture.EFCore;
using InventoryManagement.Infrastucture.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Infrastucture.Configuration
{
    public class InventoryBootstrapper
    {
        public static void Configure(IServiceCollection services,string connectionString)
        {
            services.AddTransient<IInventoryApplication, InventoryApplication>();
            services.AddTransient<IInventoryRepository, InventoryRepository>();

            services.AddTransient<IPermissionExposer, InventoryPermissionExposer>();

            services.AddTransient<IInventoryQuery, InventoryQuery>();

            services.AddDbContext<InventoryContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
