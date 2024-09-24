using BlogManagement.Application.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Blog.ArticleCategory
{
    public class IndexModel : PageModel
    {
        public ArticleCategorySearchModel SearchModel;
        public List<ArticleCategoryViewModel> ArticleCategory;

        private readonly IArticleCategoryApplication _application;

        public IndexModel(IArticleCategoryApplication application)
        {
            _application = application;
        }

        public void OnGet(ArticleCategorySearchModel SearchModel)
        { 
           ArticleCategory = _application.Search(SearchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("Create", new CreateArticleCategory());
        }
        public JsonResult OnPostCreate(CreateArticleCategory command)
        {
           var result =  _application.Create(command);
           return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
           var articleCategory = _application.GetDetails(id);
            return Partial("./Edit", articleCategory);
        }
        public JsonResult OnPostEdit(EditArticleCategory command)
        {
            var result = _application.Edit(command);
            return new JsonResult(result);
        }
    }
}
