using _01_LampshadeQuery.Contracts.Product;
using _01_LampshadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class SearchModel : PageModel
    {
        public string Value;
        public List<ProductQueryModel> Product { get; set; }
        private readonly IProductQuery _productQuery;

        public SearchModel(IProductQuery productquery)
        {
            _productQuery = productquery;
        }

        public void OnGet(string value)
        {
            Value = value;
            Product = _productQuery.Search(value);
        }
    }
}
