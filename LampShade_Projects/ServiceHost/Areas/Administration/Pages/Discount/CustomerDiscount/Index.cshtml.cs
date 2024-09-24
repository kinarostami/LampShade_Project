using DiscountManagement.Application.Contracts.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Discount.CustomerDiscount
{
    public class IndexModel : PageModel
    {
        [TempData] public string Messsage { get; set; }

        public CustomerDiscountSearchModel SearchModel;
        public List<CustomerDiscountViewModel> CustomerDiscount;
        private readonly ICustomerDiscountApplication _customerDiscountApplication;

        public SelectList Products;
        private readonly IProductApplication _application;
        public IndexModel(ICustomerDiscountApplication customerDiscountApplication, IProductApplication application)
        {
            _customerDiscountApplication = customerDiscountApplication;
            _application = application;
        }


        public void OnGet(CustomerDiscountSearchModel SearchModel)
        { 
           Products = new SelectList(_application.GetProducts(), "Id", "Name");
           CustomerDiscount = _customerDiscountApplication.Search(SearchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new DefineCustomerDiscount
            {
                Products = _application.GetProducts()
            };
            return Partial("./Create",command);
        }
        public JsonResult OnPostCreate(DefineCustomerDiscount command)
        {
           var result =  _customerDiscountApplication.Define(command);
           return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var customerDiscount = _customerDiscountApplication.Details(id);
            customerDiscount.Products = _application.GetProducts();
            return Partial("./Edit",customerDiscount);
        }
        public JsonResult OnPostEdit(EditCustomerDiscount command)
        {
            var result = _customerDiscountApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
