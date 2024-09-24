using _0_Framework.Application;
using _0_Framework.Infrastucture;
using AccountManagement.Application.Contract.Role;
using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastucture.EFCore.Repository
{
    public class RoleRepository : RepositoryBase<long,Role>, IRoleRepository
    {
        private readonly AccountContext _context;

        public RoleRepository(AccountContext context) : base(context)
        {
            _context = context;
        }

        public EditRole GetDetails(long id)
        {
            return _context.Roles.Select(x => new EditRole
            {
                Id = x.Id,
                Name = x.Name,
                MapedPermissions = MapPermissions(x.Permissions)
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        private static List<PermissionDto> MapPermissions(List<Permission> permissions)
        {
            return permissions.Select(x => new PermissionDto(x.Code,x.Name)).ToList();
        }

        public List<RoleViewModel> GetRole()
        {
            return _context.Roles.Select(x => new RoleViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CreationDate = x.CreationDate.ToFarsi()
            }).OrderByDescending(x => x.Id).ToList();
        }
    }
}
