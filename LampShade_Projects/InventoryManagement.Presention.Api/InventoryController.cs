
using _01_LampshadeQuery.Contracts.Inventory;
using InventoryManagement.Application.Contract.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Controllers
{
    [Route("[api/[controller]]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryApplication _inventoryApplication;
        private readonly IInventoryQuery _inventoryQuery;

        public InventoryController(IInventoryApplication inventoryApplication, IInventoryQuery inventoryQuery)
        {
            _inventoryApplication = inventoryApplication;
            _inventoryQuery = inventoryQuery;
        }

        //[HttpGet("{id}")]
        //public List<InventoryOperationViewModel> GetOperationBy(long id)
        //{
        //    return _inventoryApplication.GetOperationLog(id);
        //}

        //[HttpPost]
        //public StockStatus CheckStock(IsInStock command)
        //{
        //    return _inventoryQuery.CheckStatus(command);
        //}
    }
}
