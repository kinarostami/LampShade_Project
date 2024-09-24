using _0_Framework.Infrastucture;
using _0_Framework.Permission;
using InventoryManagement.Application.Contract.Inventory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    [Authorize(Roles = Roles.Administration)]
    public class IndexModel : PageModel
    {
        public InventorySearchModel SearchModel;
        public List<InventoryViewModel> Inventory;
        private readonly IInventoryApplication _inventoryApplication;

        public SelectList Products;
        private readonly IProductApplication _application;
        public IndexModel(IProductApplication application, IInventoryApplication inventoryApplication)
        {
            _application = application;
            _inventoryApplication = inventoryApplication;
        }

        [NeedsPermission(InventoryPermissions.ListInventory)]
        public void OnGet(InventorySearchModel SearchModel)
        { 
           Products = new SelectList(_application.GetProducts(), "Id", "Name");
           Inventory = _inventoryApplication.Search(SearchModel);
        }

        [NeedsPermission(InventoryPermissions.CreateInventory)]
        public IActionResult OnGetCreate()
        {
            var command = new CreateInventory()
            {
                Products = _application.GetProducts()
            };
            return Partial("./Create",command);
        }

        [NeedsPermission(InventoryPermissions.CreateInventory)]
        public JsonResult OnPostCreate(CreateInventory command)
        {
           var result = _inventoryApplication.Create(command);
           return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var inventory = _inventoryApplication.GetDetails(id);
            inventory.Products = _application.GetProducts();
            return Partial("./Edit",inventory);
        }

        [NeedsPermission(InventoryPermissions.EditInventory)]
        public JsonResult OnPostEdit(EditInventory command)
        {
            var result = _inventoryApplication.Edit(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetIncrease(long id)
        {
            var command = new IncreaseInventory()
            {
                InventoryId = id
            };
            return Partial("Increase",command);
        }

        [NeedsPermission(InventoryPermissions.Increase)]
        public JsonResult OnPostIncrease(IncreaseInventory command)
        {
            var result = _inventoryApplication.Increase(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetReduce(long id)
        {
            var command = new ReduceInventory()
            {
                InventoryId = id
            };
            return Partial("Reduce",command);
        }

        [NeedsPermission(InventoryPermissions.Decrease)]
        public JsonResult OnPostReduce(ReduceInventory command)
        {
            var result = _inventoryApplication.Reduce(command);
            return new JsonResult(result);
        }

        [NeedsPermission(InventoryPermissions.HistoryLog)]
        public IActionResult OnGetLog(long id)
        {
            var log = _inventoryApplication.GetOperationLog(id);
            return Partial("OperationLog", log);
        }
    }
}
