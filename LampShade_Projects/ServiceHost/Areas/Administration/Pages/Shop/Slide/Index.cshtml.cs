using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Slide;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slide
{
    public class IndexModel : PageModel
    {
        [TempData] public string Messsage { get; set; }
        public List<SlideViewModel> SlideView;

        private readonly ISlideApplication _application;

        public IndexModel(ISlideApplication application)
        {
            _application = application;
        }

        public void OnGet()
        {
            SlideView = _application.GetList();
        }

        public IActionResult OnGetCreate()
        {
            var create = new CreateSlide();
            return Partial("./Create",create);
        }
        public JsonResult OnPostCreate(CreateSlide command)
        {
           var result = _application.Create(command);
           return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var edit = _application.GetDetails(id);
            return Partial("./Edit", edit);
        }
        public JsonResult OnPostEdit(EditSlide command)
        {
            var result = _application.Edit(command);
            return new JsonResult(result);
        }

        public RedirectToPageResult OnGetRemove(long id)
        {
           var result = _application.Remove(id);
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
        public RedirectToPageResult OnGetRestore(long id)
        {
            var result = _application.Restore(id);
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
