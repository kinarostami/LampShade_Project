using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductPicture
{
    public class IndexModel : PageModel
    {
        [TempData] public string Messsage { get; set; }

        public ProductPictureSearchModel SearchModel;
        public List<ProductPictureViewModel> ProductPictureView;
        private readonly IProductPictureApplication _pictureApplication;

        public SelectList Product;
        private readonly IProductApplication _application;

        public IndexModel(IProductApplication application, IProductPictureApplication pictureApplication)
        {
            _application = application;
            _pictureApplication = pictureApplication;
        }

        public void OnGet(ProductPictureSearchModel SearchModel)
        { 
           Product = new SelectList(_application.GetProducts(),"Id","Name");
           ProductPictureView = _pictureApplication.Search(SearchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateProductPicture
            {
                ProductView = _application.GetProducts()
            };
            return Partial("./Create", command);
        }
        public JsonResult OnPostCreate(CreateProductPicture command)
        {
           var result = _pictureApplication.Create(command);
           return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var productPicture = _pictureApplication.GetDetails(id);
            productPicture.ProductView = _application.GetProducts();
            return Partial("./Edit", productPicture);
        }
        public JsonResult OnPostEdit(EditProductPicture command)
        {
            var result = _pictureApplication.Edit(command);
            return new JsonResult(result);
        }

        public RedirectToPageResult OnGetRemove(long id)
        {
           var result = _pictureApplication.Remove(id);
           if (result.IsSuccedded)
           {
               return RedirectToPage("./Index");
           }
           else
           {
               Messsage = result.Message;
           }
           return RedirectToPage("./Index");
        }
        public RedirectToPageResult OnGetRestore(long id)
        {
            var result = _pictureApplication.Restore(id);
            if (result.IsSuccedded)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                Messsage = result.Message;
            }
            return RedirectToPage("./Index");
        }
    }
}
