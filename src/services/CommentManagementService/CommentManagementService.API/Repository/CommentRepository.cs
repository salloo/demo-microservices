
using CommentManagementService.Model;
using Microsoft.EntityFrameworkCore;

namespace CommentManagementService.Repository;


public class CommentRepository: ICommentRepository
{
    private readonly CommentContext _context;
    public IUnitOfWork UnitOfWork => _context;
    public CommentRepository(CommentContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context)); 
    }
    public Comment CreateComment(Comment comment)
    {
        return _context.Add(comment).Entity;
    }

    public CommentViewModel GetComments(string postId)
    {
        var result =  _context.Comments.Where(
            x => x.PostId.ToString() == postId)
            .Select(s => s.Comments).ToList();
        
        return new CommentViewModel
        {
            PostId = postId,
            Comments = result,
        };
    }
}