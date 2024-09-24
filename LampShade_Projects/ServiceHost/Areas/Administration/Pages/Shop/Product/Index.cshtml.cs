using _0_Framework.Infrastucture;
using _0_Framework.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.Product
{
    //[Authorize(Roles = "3,5")]
    public class IndexModel : PageModel
    {
        [TempData] public string Messsage { get; set; }

        public ProductSearchModel SearchModel;
        public List<ProductViewModel> ProductView;
        private readonly IProductApplication _productApplication;

        public SelectList Category;
        private readonly IProductCategoryApplication _application;

        public IndexModel(IProductCategoryApplication application, IProductApplication productApplication)
        {
            _application = application;
            _productApplication = productApplication;
        }

        [NeedsPermission(ShopPermission.ListProduct)]
        public void OnGet(ProductSearchModel SearchModel)
        { 
           Category = new SelectList(_application.GetProductCategory(),"Id","Name");
           ProductView = _productApplication.Search(SearchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateProduct
            {
                Category = _application.GetProductCategory()
            };
            return Partial("./Create",command);
        }

        [NeedsPermission(ShopPermission.CreateProduct)]
        public JsonResult OnPostCreate(CreateProduct command)
        {
           var result =  _productApplication.Create(command);
           return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var product = _productApplication.GetDetails(id);
            product.Category = _application.GetProductCategory();
            return Partial("./Edit", product);
        }

        [NeedsPermission(ShopPermission.EditProduct)]
        public JsonResult OnPostEdit(EditProduct command)
        {
            var result = _productApplication.Edit(command);
            return new JsonResult(result);
        }

    }
}
