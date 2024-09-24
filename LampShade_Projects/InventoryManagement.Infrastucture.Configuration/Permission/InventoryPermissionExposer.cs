using _0_Framework.Infrastucture;
using _0_Framework.Permission;

namespace InventoryManagement.Infrastucture.Configuration.Permission
{
    public class InventoryPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Inventory", new List<PermissionDto>
                    {
                        new(InventoryPermissions.CreateInventory,"CreateInventory"),
                        new(InventoryPermissions.Increase,"Increase"),
                        new(InventoryPermissions.Decrease,"Decrease"),
                        new(InventoryPermissions.HistoryLog,"HistoryLog"),
                        new(InventoryPermissions.SearchInventory,"SearchInventory"),
                        new(InventoryPermissions.EditInventory,"EditInventory"),
                        new(InventoryPermissions.ListInventory,"ListInventory"),
                    }

                }
            };
        }
    }
}
