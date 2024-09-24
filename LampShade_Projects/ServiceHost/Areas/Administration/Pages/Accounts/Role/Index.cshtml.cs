using AccountManagement.Application.Contract.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class IndexModel : PageModel
    {
        [TempData] public string Messsage { get; set; }

        public List<RoleViewModel> Role;
        private readonly IRoleApplication _roleApplication;

        public IndexModel( IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        public void OnGet()
        {
            Role = _roleApplication.GetRole();
        }
    }
}
