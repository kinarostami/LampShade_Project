using _0_Framework.Infrastucture;
using _0_Framework.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategory
{
    [Authorize(Roles = "3,5")]
    public class IndexModel : PageModel
    {
        public ProductCategorySearchModel SearchModel;
        public List<ProductCategoryViewModel> CategoryViewModels;

        private readonly IProductCategoryApplication _application;

        public IndexModel(IProductCategoryApplication application)
        {
            _application = application;
        }

        [NeedsPermission(ShopPermission.ListProductCategory)]
        public void OnGet(ProductCategorySearchModel SearchModel)
        { 
           CategoryViewModels = _application.Search(SearchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateProductCategory());
        }
        
        [NeedsPermission(ShopPermission.CreateProductCategory)]
        public JsonResult OnPostCreate(CreateProductCategory command)
        {
           var result =  _application.Create(command);
           return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
           var productCategory = _application.GetDetails(id);
            return Partial("./Edit", productCategory);
        }
        
        [NeedsPermission(ShopPermission.EditProductCategory)]
        public JsonResult OnPostEdit(EditProductCategory command)
        {
            var result = _application.Edit(command);
            return new JsonResult(result);
        }
    }
}
