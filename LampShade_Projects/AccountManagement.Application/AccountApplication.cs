using System.Collections;
using _0_Framework.Application;
using AccountManagement.Application.Contract.Account;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAccountRepository _accountRepository;
        private readonly IAuthHelper _authHelper;
        private readonly IRoleRepository _roleRepository;
        public AccountApplication(IAccountRepository accountRepository, IFileUploader fileUploader, IPasswordHasher passwordHasher, IAuthHelper authHelper, IRoleRepository roleRepository)
        {
            _accountRepository = accountRepository;
            _fileUploader = fileUploader;
            _passwordHasher = passwordHasher;
            _authHelper = authHelper;
            _roleRepository = roleRepository;
        }

        public AccountViewModel GetAccountBy(long id)
        {
            var account = _accountRepository.Get(id);
            return new AccountViewModel
            {
                Fullname = account.Fullname,
                Mobile = account.Mobile
            };
        }

        public OperationResult Register(RegisterAccount command)
        {
            var operation = new OperationResult();
            if (_accountRepository.Exist(x => x.Username == command.Username || x.Mobile == command.Mobile))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            else
            {
                var password = _passwordHasher.Hash(command.Password);
                var path = $"profilePhotos";
                var picturePath = _fileUploader.Upload(command.ProfilePhoto, path);

                var account = new Account(command.Fullname, command.Username, password, command.Mobile,
                    picturePath, command.RoleId);
                
                _accountRepository.Create(account);
                _accountRepository.SaveChanges();
                return operation.Succeeded();
            }
        }

        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);
            if (account == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            if (_accountRepository.Exist(x => (x.Username == command.Username || x.Mobile == command.Mobile) && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            else
            {
                var path = $"profilePhotos";
                var picturePath = _fileUploader.Upload(command.ProfilePhoto, path);

                account.Edit(command.Fullname,command.Username,command.Mobile,picturePath,command.RoleId);

                _accountRepository.SaveChanges();
                return operation.Succeeded();
            }
        }

        public OperationResult Login(Login command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetBy(command.Username);
            if (account == null)
            {
                return operation.Failed(ApplicationMessages.WrongUsePass);
            }

            var result = _passwordHasher.Check(account.Password, command.Password);
            if (!result.Verified)
            {
                return operation.Failed(ApplicationMessages.WrongUsePass);
            }

            var permissions = _roleRepository
                .Get(account.RoleId).Permissions
                .Select(x => x.Code).ToList();

            var authViewModel = new AuthViewModel(account.Id, account.RoleId, account.Fullname, account.Username,account.Mobile,permissions);
            _authHelper.Signin(authViewModel);

            return operation.Succeeded();
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);
            if (account == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            if (command.Paasword != command.RePassword)
            {
                return operation.Failed(ApplicationMessages.PasswordsNotMatch);
            }
            else
            {
                var password = _passwordHasher.Hash(command.Paasword);
                account.ChangePassword(password);
                
                _accountRepository.SaveChanges();
                return operation.Succeeded();
            }

        }

        public EditAccount GetDetails(long id)
        {
            return _accountRepository.GetDetails(id);
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            return _accountRepository.Search(searchModel);
        }

        public List<AccountViewModel> GetList()
        {
            return _accountRepository.GetList();
        }

        public void Logout()
        {
            _authHelper.SignOut();
        }
    }
}
