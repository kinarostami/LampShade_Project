using System.Collections;
using _0_Framework.Application;

namespace AccountManagement.Application.Contract.Account
{
    public interface IAccountApplication
    {
        AccountViewModel GetAccountBy(long id);
        OperationResult Register(RegisterAccount command);
        OperationResult Edit(EditAccount command);
        OperationResult Login(Login command);
        EditAccount GetDetails(long id);
        OperationResult ChangePassword(ChangePassword command);
        List<AccountViewModel> Search(AccountSearchModel searchModel);
        List<AccountViewModel> GetList();
        void Logout();
    }
}
