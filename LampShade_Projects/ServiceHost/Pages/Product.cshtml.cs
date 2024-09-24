using _01_LampshadeQuery.Contracts.Product;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Infrastucture.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IProductQuery _productQuery;
        public ProductQueryModel Product;

        private readonly ICommentApplication _application;
        public ProductModel(IProductQuery productQuery, ICommentApplication application)
        {
            _productQuery = productQuery;
            _application = application;
        }

        public void OnGet(string id)
        {
            Product = _productQuery.GetProductDetails(id);
        }

        public IActionResult OnPost(CreateComment command,string ProductSlug)
        {
            command.Type = CommentType.Product;
            _application.Create(command);
            return RedirectToPage("/Product" ,new { Id = ProductSlug });
        }
    }
}
