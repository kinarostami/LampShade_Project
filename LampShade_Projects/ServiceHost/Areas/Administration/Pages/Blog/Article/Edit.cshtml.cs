using BlogManagement.Application.Contract.Article;
using BlogManagement.Application.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Article
{
    public class EditModel : PageModel
    {
        [BindProperty] public EditArticle Article { get; set; }
        private readonly IArticleApplication _application;

        public SelectList ArticleCategories;
        private readonly IArticleCategoryApplication _category;

        public EditModel(IArticleApplication application, IArticleCategoryApplication category)
        {
            _application = application;
            _category = category;
        }

        public void OnGet(long id)
        {
            Article = _application.GetDetails(id);
            ArticleCategories = new SelectList(_category.GetArticleCategories(), "Id", "Name");
        }

        public IActionResult OnPost()
        {
            _application.Edit(Article);
            return RedirectToPage("./Index");
        }
    }
}
