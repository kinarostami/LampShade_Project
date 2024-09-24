using _0_Framework.Domain;
using ShopManagement.Domain.ProductAgg;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class Inventory : EntityBase
    {
        public long ProductId { get;private set; }
        public double UnitPrice { get; private set; }
        public bool InStock { get; private set; }
        public List<InventoryOperation> Operations { get; private set; }
        public Inventory(long productId,double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            InStock = false;
            Operations = new List<InventoryOperation>();
        }

        public void Edit(long productId,double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
        }

        //محاسبه فعلی موجود در انبار
        public long CalculateCurrentCount()
        {
            var plus = Operations.Where(x => x.Operation).Sum(x => x.Count);
            var minus = Operations.Where(x => !x.Operation).Sum(x => x.Count);
            return plus - minus;
        }

        //افزایش موجودی
        public void Increase(long count,long operationId,string description)
        {
            var currentCount = CalculateCurrentCount() + count;
            var operation = new InventoryOperation(true, count, operationId, currentCount, 
                description, 0, Id);
            Operations.Add(operation);
            InStock = currentCount > 0;
        }

        //کاهش موجودی
        public void Reduce(long count, long operationId, string description,long orderId)
        {
            var currentCount = CalculateCurrentCount() - count;
            var operation = new InventoryOperation(false, count, operationId, currentCount,
                description, orderId, Id);
            Operations.Add(operation);
            InStock = currentCount > 0;
        }
    }
}
