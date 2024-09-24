using _01_LampshadeQuery.Contracts.ArticleCategory;
using _01_LampshadeQuery.Contracts.ProductCategory;
using _01_LampshadeQuery.Query;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _categoryQuery;
        private readonly IArticleCategoryQuery _articleCategory;
        public MenuViewComponent(IProductCategoryQuery categoryQuery, IArticleCategoryQuery articleCategory)
        {
            _categoryQuery = categoryQuery;
            _articleCategory = articleCategory;
        }

        public IViewComponentResult Invoke()
        {
            var result = new MenuModel
            {
                ArticleCategories = _articleCategory.GetArticleCategory(),
                ProductCategories = _categoryQuery.GetCategories()
            };
            return View(result);
        }
    }
}
