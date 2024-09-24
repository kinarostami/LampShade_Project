using AccountManagement.Application.Contract.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Areas.Administration.Pages.Shop.Order
{
    //[Authorize(Roles = "3,5")]
    public class IndexModel : PageModel
    {
        public OrderSearchModel SearchModel;
        public List<OrderViewModel> Orders;
        public SelectList Accounts;

        private readonly IOrderApplication _orderApplication;
        private readonly IAccountApplication _accountApplication;

        public IndexModel(IOrderApplication orderApplication, IAccountApplication accountApplication)
        {
            _orderApplication = orderApplication;
            _accountApplication = accountApplication;
        }

        public void OnGet(OrderSearchModel searchModel)
        {
            Accounts = new SelectList(_accountApplication.GetList(), "Id", "Fullname");
            Orders = _orderApplication.Search(searchModel);
        }

        public IActionResult OnGetConfirm(long id)
        {
            _orderApplication.PaymentSucceeded(id, 0);
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetCancel(long id)
        {
            _orderApplication.Cancel(id);
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetItems(long orderId)
        {
           var items = _orderApplication.GetItems(orderId);
           return Partial("Items", items);
        }

    }
}
