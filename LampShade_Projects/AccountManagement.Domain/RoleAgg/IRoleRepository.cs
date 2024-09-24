﻿using _0_Framework.Domain;
using AccountManagement.Application.Contract.Role;

namespace AccountManagement.Domain.RoleAgg
{
    public interface IRoleRepository : IRepository<long,Role>
    {
        EditRole GetDetails(long id);
        List<RoleViewModel> GetRole();
    }
}
