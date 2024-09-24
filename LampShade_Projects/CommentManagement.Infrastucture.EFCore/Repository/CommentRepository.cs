using _0_Framework.Application;
using _0_Framework.Infrastucture;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Domain.CommentAgg;

namespace CommentManagement.Infrastucture.EFCore.Repository
{
    public class CommentRepository : RepositoryBase<long,Comment>,ICommentRepository
    {
        private readonly CommentContext _shopContext;

        public CommentRepository(CommentContext shopContext) : base(shopContext)
        {
            _shopContext = shopContext;
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {

            var query = _shopContext.Comments
                .Select(x => new CommentViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Website = x.Website,
                Message = x.Message,
                Type = x.Type,
                OwnerRecordId = x.OwnerRecordId,
                IsCanceled = x.IsCanceled,
                IsConfirmed = x.IsConfirmed,
                CreationDate = x.CreationDate.ToFarsi(),

            });
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            }
            if (!string.IsNullOrWhiteSpace(searchModel.Email))
            {
                query = query.Where(x => x.Email.Contains(searchModel.Email));
            }

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
