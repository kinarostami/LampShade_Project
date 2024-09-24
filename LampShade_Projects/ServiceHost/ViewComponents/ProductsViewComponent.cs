using _01_LampshadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class ProductsViewComponent : ViewComponent
    {
        private readonly IProductQuery _productQuery;

        public ProductsViewComponent(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public IViewComponentResult Invoke()
        {
            var product = _productQuery.GetProducts();
            return View(product);
        }
    }
}
