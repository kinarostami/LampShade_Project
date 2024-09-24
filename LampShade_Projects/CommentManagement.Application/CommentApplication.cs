using _0_Framework.Application;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Domain.CommentAgg;

namespace CommentManagement.Application
{
    public class CommentApplication : ICommentApplication
    {
        private readonly ICommentRepository _commentRepository;

        public CommentApplication(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public OperationResult Create(CreateComment command)
        {
            var operation = new OperationResult();
            var comment = new Comment(command.Name, command.Message, command.Email, command.OwnerRecordId,command.Type,command.Website,command.ParentId);
            _commentRepository.Create(comment);
            _commentRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Confirmed(long id)
        {
            var operation = new OperationResult();
            var comment = _commentRepository.Get(id);
            if (comment == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            else
            {
                comment.Confirmed();
                _commentRepository.SaveChanges();
                return operation.Succeeded();
            }
        }

        public OperationResult Cancel(long id)
        {
            var operation = new OperationResult();
            var comment = _commentRepository.Get(id);
            if (comment == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            else
            {
                comment.Cancel();
                _commentRepository.SaveChanges();
                return operation.Succeeded();
            }
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            return _commentRepository.Search(searchModel);
        }
    }
}
