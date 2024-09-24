using _01_LampshadeQuery.Contracts.Article;
using _01_LampshadeQuery.Contracts.ArticleCategory;
using _01_LampshadeQuery.Query;
using BlogManagement.Application.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleCategoryModel : PageModel
    {
        public ArticleCategoryQueryModel ArticleCategories;
        public List<ArticleCategoryQueryModel> ArticleCategory;
        private readonly IArticleCategoryQuery _categoryQuery;
        
        public List<ArticleQueryModel> LatestArticles;
        private readonly IArticleQuery _articlesQuery;

        public ArticleCategoryModel(IArticleCategoryQuery categoryQuery, IArticleQuery articlesQuery)
        {
            _categoryQuery = categoryQuery;
            _articlesQuery = articlesQuery;
        }

        public void OnGet(string id)
        {
            ArticleCategories = _categoryQuery.GetDetails(id);
            ArticleCategory = _categoryQuery.GetArticleCategory();

            LatestArticles = _articlesQuery.GetArticles();
        }
    }
}
