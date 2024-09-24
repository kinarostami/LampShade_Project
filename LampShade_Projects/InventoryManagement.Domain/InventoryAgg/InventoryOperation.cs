namespace InventoryManagement.Domain.InventoryAgg;

public class InventoryOperation
{
    public long Id { get; private set; }
    public bool Operation { get;private set; }
    public long Count { get; private set; }
    public long OperationId { get; private set; }
    public DateTime OperationDate { get; private set; }
    public long CurrentCount { get; private set; }
    public string Description { get; private set; }
    public long OrderId { get; private set; }
    public long InventoryId { get; private set; }
    public Inventory Inventory { get; private set; }

    public InventoryOperation(bool operation,long count,long operationId,long currentCount,string description,long orderId,long inventoryId)
    {
        Operation = operation;
        Count = count;
        OperationId = operationId;
        CurrentCount = currentCount;
        Description = description;
        OrderId = orderId;
        InventoryId = inventoryId;
        OperationDate = DateTime.Now;
    }
}