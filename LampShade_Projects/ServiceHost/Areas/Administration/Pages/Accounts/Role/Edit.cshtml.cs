using _0_Framework.Infrastucture;
using AccountManagement.Application.Contract.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public EditRole Command { get; set; }

        public List<SelectListItem> Permissions = new List<SelectListItem>();
        private readonly IRoleApplication _application;
        private readonly IEnumerable<IPermissionExposer> _permissionsExposer;
        public EditModel(IRoleApplication application, IEnumerable<IPermissionExposer> permissionsExposer)
        {
            _application = application;
            _permissionsExposer = permissionsExposer;
        }

        public void OnGet(long id)
        {
           Command = _application.GetDetails(id);

           var permission = new List<PermissionDto>();
           foreach (var exposer in _permissionsExposer)
           {
               var exposerPermission = exposer.Expose();
               foreach (var (key,value) in exposerPermission)
               {
                   permission.AddRange(value);
                   var group = new SelectListGroup()
                   {
                       Name = key
                   };
                   foreach (var permissions in value)
                   {
                       var item = new SelectListItem(permissions.Name, permissions.Code.ToString())
                       {
                           Group = group
                       };

                       if (Command.MapedPermissions.Any(x => x.Code == permissions.Code))
                       {
                           item.Selected = true;
                       }

                       Permissions.Add(item);

                   }
               }
           }
        }
        public IActionResult OnPost()
        {
            _application.Edit(Command);
            return RedirectToPage("Index");
        }
    }
}
