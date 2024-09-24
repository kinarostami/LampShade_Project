using _0_Framework.Infrastucture;

namespace AccountManagement.Application.Contract.Role;

public class EditRole : CreateRole
{
    public long Id { get; set; }
    public List<PermissionDto> MapedPermissions { get; set; }
}