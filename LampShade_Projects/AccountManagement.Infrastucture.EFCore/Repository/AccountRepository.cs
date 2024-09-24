using _0_Framework.Application;
using _0_Framework.Infrastucture;
using AccountManagement.Application.Contract.Account;
using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastucture.EFCore.Repository
{
    public class AccountRepository : RepositoryBase<long,Account>, IAccountRepository
    {
        private readonly AccountContext _context;

        public AccountRepository(AccountContext context) : base(context)
        {
            _context = context;
        }

        public Account GetBy(string username)
        {
            return _context.Accounts.FirstOrDefault(x => x.Username == username);
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            var query = _context.Accounts
                .Include(x => x.Roles)
                .Select(x => new AccountViewModel
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Username = x.Username,
                Mobile = x.Mobile,
                ProfilePhoto = x.ProfilePhoto,
                RoleId = x.RoleId,
                Role = x.Roles.Name,
                CreationDate = x.CreationDate.ToFarsi()
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Fullname))
            {
                query = query.Where(x => x.Fullname.Contains(searchModel.Fullname));
            }
            if (!string.IsNullOrWhiteSpace(searchModel.Username))
            {
                query = query.Where(x => x.Username.Contains(searchModel.Username));
            }
            if (!string.IsNullOrWhiteSpace(searchModel.Mobile))
            {
                query = query.Where(x => x.Mobile.Contains(searchModel.Mobile));
            }
            if (searchModel.RoleId > 0)
            {
                query = query.Where(x => x.RoleId == searchModel.RoleId);
            }


            return query.OrderByDescending(x => x.Id).ToList();
        }

        public EditAccount GetDetails(long id)
        {
            return _context.Accounts.Select(x => new EditAccount
            {
                Fullname = x.Fullname,
                Username = x.Username,
                Password = x.Password,
                Mobile = x.Mobile,
                RoleId = x.RoleId,
                Id = x.Id,


            }).FirstOrDefault(x => x.Id == id);
        }

        public List<AccountViewModel> GetList()
        {
            return _context.Accounts.Select(x => new AccountViewModel
            {
                Id = x.Id,
                Fullname = x.Fullname
            }).ToList();
        }
    }
}
