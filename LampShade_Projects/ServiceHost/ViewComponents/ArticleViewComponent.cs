using _01_LampshadeQuery.Contracts.Article;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class ArticleViewComponent : ViewComponent
    {
        private readonly IArticleQuery _application;

        public ArticleViewComponent(IArticleQuery application)
        {
            _application = application;
        }

        public IViewComponentResult Invoke()
        {
            var article = _application.GetArticles();
            return View(article);
        }
    }
}
