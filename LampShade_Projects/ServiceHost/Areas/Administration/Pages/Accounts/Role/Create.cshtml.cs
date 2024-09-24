using AccountManagement.Application.Contract.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public CreateRole Command { get; set; }
        private readonly IRoleApplication _application;

        public CreateModel(IRoleApplication application)
        {
            _application = application;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
             _application.Create(Command);
            return RedirectToPage("Index");
        }
    }
}
