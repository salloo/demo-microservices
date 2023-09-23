

using CommentManagementService.Model;
using CommentManagementService.Repository;

public interface ICommentRepository: IRepository<Comment>
{

    Task<CommentViewModel> GetCommentsAsync(string postId);
    Comment CreateComment(Comment comment);

    Task<bool> UpdateCommentAsync(string postId, string comment);
}