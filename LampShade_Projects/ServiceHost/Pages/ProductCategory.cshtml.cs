using _01_LampshadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductCategoryModel : PageModel
    {
        public ProductCategoryQueryModel ProductCategory { get; set; }
        private readonly IProductCategoryQuery _categoryQuery;

        public ProductCategoryModel(IProductCategoryQuery categoryQuery)
        {
            _categoryQuery = categoryQuery;
        }

        public void OnGet(string id)
        {
            ProductCategory = _categoryQuery.GetProductCategoryWithProductsBy(id);
        }
    }
}
