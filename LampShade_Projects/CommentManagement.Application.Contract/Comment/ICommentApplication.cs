using _0_Framework.Application;

namespace CommentManagement.Application.Contract.Comment
{
    public interface ICommentApplication
    {
        OperationResult Create(CreateComment command);
        OperationResult Confirmed(long id);
        OperationResult Cancel(long id);
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}
