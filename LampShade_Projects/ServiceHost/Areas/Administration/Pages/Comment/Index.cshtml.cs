using CommentManagement.Application.Contract.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Comment
{
    public class IndexModel : PageModel
    {
        [TempData] public string Messsage { get; set; }
        public List<CommentViewModel> Comment;
        public CommentSearchModel Search;
        private readonly ICommentApplication _application;

        public IndexModel(ICommentApplication application)
        {
            _application = application;
        }

        public void OnGet(CommentSearchModel searchModel)
        {
            Comment = _application.Search(searchModel);
        }


        public RedirectToPageResult OnGetConfirmed(long id)
        {
           var result = _application.Confirmed(id);
           if (result.IsSuccedded)
           {
               return RedirectToPage("./Index");
           }
           else
           {
               Messsage = result.Message;
           }
           return RedirectToPage("./Index");
        }
        public RedirectToPageResult OnGetCancel(long id)
        {
            var result = _application.Cancel(id);
            if (result.IsSuccedded)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                Messsage = result.Message;
            }
            return RedirectToPage("./Index");
        }
    }
}
