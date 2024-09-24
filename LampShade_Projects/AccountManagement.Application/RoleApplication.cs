using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contract.Role;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application
{
    public class RoleApplication : IRoleApplication
    {
        private readonly IRoleRepository _roleRepository;

        public RoleApplication(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public OperationResult Create(CreateRole command)
        {
            var operation = new OperationResult();
            if (_roleRepository.Exist(x => x.Name == command.Name))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            else
            {
                var role = new Role(command.Name);
                _roleRepository.Create(role);
                
                _roleRepository.SaveChanges();
                return operation.Succeeded();
            }
        }

        public OperationResult Edit(EditRole command)
        {
            var operation = new OperationResult();
            var roles = _roleRepository.Get(command.Id);
            if (roles == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            if (_roleRepository.Exist(x => x.Name == command.Name && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            else
            {
                var permission = new List<Permission>();
                command.Permissions.ForEach(code => permission.Add(new Permission(code)));

                roles.Edit(command.Name,permission);

                _roleRepository.SaveChanges();
                return operation.Succeeded();
            }
        }

        public EditRole GetDetails(long id)
        {
            return _roleRepository.GetDetails(id);
        }

        public List<RoleViewModel> GetRole()
        {
            return _roleRepository.GetRole();
        }
    }
}
