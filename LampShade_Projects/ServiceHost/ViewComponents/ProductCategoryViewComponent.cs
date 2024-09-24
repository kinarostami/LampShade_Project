using _01_LampshadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoryViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _categoryQuery;

        public ProductCategoryViewComponent(IProductCategoryQuery categoryQuery)
        {
            _categoryQuery = categoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var productCategory = _categoryQuery.GetCategories();
            return View(productCategory);
        }
    }
}
