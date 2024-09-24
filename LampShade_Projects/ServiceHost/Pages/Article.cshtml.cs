using _01_LampshadeQuery.Contracts.Article;
using _01_LampshadeQuery.Contracts.ArticleCategory;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastucture.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {
        public ArticleQueryModel Article { get; set; }
        public List<ArticleQueryModel> LatestArticles;
        private readonly IArticleQuery _articleQuery;

        public List<ArticleCategoryQueryModel> LatestCategory;
        private readonly IArticleCategoryQuery _categoryQuery;

        private readonly ICommentApplication _Commentapplication;
        public ArticleModel(IArticleQuery articleQuery, IArticleCategoryQuery categoryQuery, ICommentApplication application)
        {
            _articleQuery = articleQuery;
            _categoryQuery = categoryQuery;
            _Commentapplication = application;
        }

        public void OnGet(string id)
        {
            Article = _articleQuery.GetDetails(id);
            LatestArticles = _articleQuery.GetArticles();

            LatestCategory = _categoryQuery.GetArticleCategory();
        }

        public IActionResult OnPost(CreateComment command,string articleSlug)
        {
            command.Type = CommentType.Article;
            _Commentapplication.Create(command);
            return RedirectToPage("/Article", new {Id = articleSlug});
        }
    }
}
