using _0_Framework.Application;
using _0_Framework.Infrastucture;
using AccountManagement.Infrastucture.EFCore;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using ShopManagement.Infrastucture.EfCore;

namespace InventoryManagement.Infrastucture.EFCore.Repository
{
    public class InventoryRepository : RepositoryBase<long,Inventory>,IInventoryRepository
    {
        private readonly AccountContext _accountContext;
        private readonly InventoryContext _context;
        private readonly ShopContext _shopContext;
        public InventoryRepository(InventoryContext context, ShopContext shopContext, AccountContext accountContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
            _accountContext = accountContext;
        }

        public EditInventory GetDetails(long id)
        {
            return _context.Inventory.Select(x => new EditInventory
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice

            }).FirstOrDefault(x => x.Id == id);
        }

        public Inventory GetBy(long productId)
        {
            return _context.Inventory.FirstOrDefault(x => x.ProductId == productId);
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = _context.Inventory.Select(x => new InventoryViewModel
            {
                Id = x.Id,
                UnitPrice = x.UnitPrice,
                ProductId = x.ProductId,
                InStock = x.InStock,
                CurrentCount = x.CalculateCurrentCount(),
                CreationDate = x.CreationDate.ToFarsi()

            });
            if (searchModel.ProductId > 0)
            {
                query = query.Where(x => x.ProductId == searchModel.ProductId);
            }
            if (searchModel.InStock)
            {
                query = query.Where(x => !x.InStock);
            }

            var inventory = query.OrderByDescending(x => x.Id).ToList();
            inventory.ForEach(item => item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name);
            return inventory;
        }

        public List<InventoryOperationViewModel> GetOperationLog(long inventoryId)
        {
            var accounts = _accountContext.Accounts.Select(x => new { x.Id, x.Fullname }).ToList();
            var inventory = _context.Inventory.FirstOrDefault(x => x.Id == inventoryId);
            var operations = inventory.Operations.Select(x => new InventoryOperationViewModel
            {
                Id = x.Id,
                Operation = x.Operation,
                Count = x.Count,
                OperatorId = x.OperationId,
                OperationDate = x.OperationDate.ToFarsi(),
                CurrentCount = x.CurrentCount,
                Description = x.Description,
                OrderId = x.OrderId,
            }).OrderByDescending(x => x.Id).ToList();

            foreach (var operation in operations)
            {
                operation.Operator = accounts.FirstOrDefault(x => x.Id == operation.OperatorId)?.Fullname;
            }

            return operations;
        }
    }
}
