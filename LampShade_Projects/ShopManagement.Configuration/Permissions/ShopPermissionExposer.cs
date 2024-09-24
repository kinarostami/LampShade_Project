using _0_Framework.Infrastucture;
using _0_Framework.Permission;

namespace ShopManagement.Configuration.Permissions
{
    public class ShopPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Product", new List<PermissionDto>
                    {
                        new(ShopPermission.ListProduct,"ListProduct"),
                        new(ShopPermission.SearchProduct,"SearchProduct"),
                        new(ShopPermission.CreateProduct,"CreateProduct"),
                        new(ShopPermission.EditProduct,"EditProduct"),
                    }

                },
                {
                    "ProductCategory", new List<PermissionDto>
                    {
                        new(ShopPermission.ListProductCategory,"ListProductCategory"),
                        new(ShopPermission.SearchProductCategory,"SearchProductCategory"),
                        new(ShopPermission.CreateProductCategory,"CreateProductCategory"),
                        new(ShopPermission.EditProductCategory,"EditProductCategory"),
                    }
                }
            };
        }
    }
}
