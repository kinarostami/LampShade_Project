using BlogManagement.Application.Contract.Article;
using BlogManagement.Application.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Article
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public CreateArticle Article { get; set; }
        private readonly IArticleApplication _application;

        public SelectList ArticleCategories;
        private readonly IArticleCategoryApplication _articleCategory;

        public CreateModel(IArticleApplication application, IArticleCategoryApplication articleCategory)
        {
            _application = application;
            _articleCategory = articleCategory;
        }

        public void OnGet()
        {
            ArticleCategories = new SelectList(_articleCategory.GetArticleCategories(), "Id", "Name");
        }

        public IActionResult OnPost()
        {
            _application.Create(Article);
            return RedirectToPage("./Index");
        }
    }
}
