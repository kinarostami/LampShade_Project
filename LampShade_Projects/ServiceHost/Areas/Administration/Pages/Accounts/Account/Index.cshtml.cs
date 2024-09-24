using AccountManagement.Application.Contract.Account;
using AccountManagement.Application.Contract.Role;
using AccountManagement.Domain.RoleAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account
{
    public class IndexModel : PageModel
    {
        [TempData] public string Messsage { get; set; }

        public AccountSearchModel SearchModel;
        public List<AccountViewModel> Account;
        private readonly IAccountApplication _accountApplication;
        
        public SelectList Roles;
        private readonly IRoleApplication _roleApplication;
        public IndexModel( IAccountApplication accountApplication, IRoleApplication roleApplication)
        {
            _accountApplication = accountApplication;
            _roleApplication = roleApplication;
        }

        public void OnGet(AccountSearchModel SearchModel)
        {
            Roles = new SelectList(_roleApplication.GetRole(), "Id", "Name");
            Account = _accountApplication.Search(SearchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new RegisterAccount
            {
                Roles = _roleApplication.GetRole()
            };
            return Partial("./Create",command);
        }
        public JsonResult OnPostCreate(RegisterAccount command)
        {
           var result =  _accountApplication.Register(command);
           return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var account = _accountApplication.GetDetails(id);
            account.Roles = _roleApplication.GetRole();
            return Partial("./Edit", account);
        }
        public JsonResult OnPostEdit(EditAccount command)
        {
            var result = _accountApplication.Edit(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetChangePassword(long id)
        {
            var account = new ChangePassword{Id = id};
            return Partial("ChangePassword", account);
        }
        public JsonResult OnPostChangePassword(ChangePassword command)
        {
            var result = _accountApplication.ChangePassword(command);
            return new JsonResult(result);
        }

    }
}
