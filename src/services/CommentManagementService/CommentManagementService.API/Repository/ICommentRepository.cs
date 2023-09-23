

using CommentManagementService.Model;
using CommentManagementService.Repository;

public interface ICommentRepository: IRepository<Comment>
{

    CommentViewModel GetComments(string postId);
    Comment CreateComment(Comment comment);
}