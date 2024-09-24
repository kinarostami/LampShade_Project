using BlogManagement.Application.Contract.Article;
using BlogManagement.Application.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Article
{
    public class IndexModel : PageModel
    {
        public ArticleSearchModel SearchModel;
        public List<ArticleViewModel> Article;
        public SelectList ArticleCategories;

        private readonly IArticleApplication _application;
        private readonly IArticleCategoryApplication _articleCategory;
        public IndexModel(IArticleApplication application, IArticleCategoryApplication articleCategory)
        {
            _application = application;
            _articleCategory = articleCategory;
        }

        public void OnGet(ArticleSearchModel SearchModel)
        {
            ArticleCategories = new SelectList(_articleCategory.GetArticleCategories(), "Id", "Name");
           Article = _application.SearchModel(SearchModel);
        }

    }
}
