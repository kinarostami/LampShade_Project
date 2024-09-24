using _01_LampshadeQuery.Contracts.Inventory;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Infrastucture.EFCore;
using ShopManagement.Infrastucture.EfCore;

namespace _01_LampshadeQuery.Query
{
    public class InventoryQuery : IInventoryQuery
    {
        private readonly InventoryContext _inventoryContext;
        private readonly ShopContext _shopContext;

        public InventoryQuery(InventoryContext inventoryContext, ShopContext shopContext)
        {
            _inventoryContext = inventoryContext;
            _shopContext = shopContext;
        }

        public StockStatus CheckStatus(IsInStock command)
        {
            var inventory = _inventoryContext.Inventory.FirstOrDefault(x => x.ProductId == command.ProductId);
            if (inventory == null || inventory.CalculateCurrentCount() < command.Count)
            {
                var product = _shopContext.Products.Select(x => new { x.Id, x.Name })
                    .FirstOrDefault(x => x.Id == command.ProductId);
                return new StockStatus
                {
                    IsStock = false,
                    ProductName = product.Name
                };
            }
            else
            {
                return new StockStatus
                {
                    IsStock = true
                };
            }
        }
    }
}
