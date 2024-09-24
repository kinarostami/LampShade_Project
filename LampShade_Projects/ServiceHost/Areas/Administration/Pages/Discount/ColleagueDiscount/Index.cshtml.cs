using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Discount.ColleagueDiscount
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public ColleagueDiscountSearchModel SearchModel { get; set; }
        public List<ColleagueDiscountViewModel> ColleagueDiscount;
        private readonly IColleagueDiscountApplication _colleagueDiscountApplication;

        public SelectList Products;
        private readonly IProductApplication _application;
        public IndexModel(IProductApplication application, IColleagueDiscountApplication colleagueDiscountApplication)
        {
            _application = application;
            _colleagueDiscountApplication = colleagueDiscountApplication;
        }


        public void OnGet(ColleagueDiscountSearchModel SearchModel)
        { 
           Products = new SelectList(_application.GetProducts(), "Id", "Name");
           ColleagueDiscount = _colleagueDiscountApplication.Search(SearchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new DefineColleagueDiscount()
            {
                Products = _application.GetProducts()
            };
            return Partial("./Create",command);
        }
        public JsonResult OnPostCreate(DefineColleagueDiscount command)
        {
           var result = _colleagueDiscountApplication.Define(command);
           return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var customerDiscount = _colleagueDiscountApplication.GetDetails(id);
            customerDiscount.Products = _application.GetProducts();
            return Partial("./Edit",customerDiscount);
        }
        public JsonResult OnPostEdit(EditColleagueDiscount command)
        {
            var result = _colleagueDiscountApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            _colleagueDiscountApplication.Remove(id);
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetRestore(long id)
        {
             _colleagueDiscountApplication.Restore(id);
            return RedirectToPage("./Index");
        }
    }
}
