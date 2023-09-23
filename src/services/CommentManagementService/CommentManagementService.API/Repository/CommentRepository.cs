
using CommentManagementService.Model;
using Microsoft.EntityFrameworkCore;

namespace CommentManagementService.Repository;


public class CommentRepository : ICommentRepository
{
    private readonly ILogger<CommentRepository> _logger;
    private readonly CommentContext _context;
    public IUnitOfWork UnitOfWork => _context;
    public CommentRepository(CommentContext context, ILogger<CommentRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger;
    }
    public Comment CreateComment(Comment comment)
    {
        return _context.Add(comment).Entity;
    }

    public async Task<CommentViewModel> GetCommentsAsync(string postId)
    {
        var result = await _context.Comments.Where(
            x => x.PostId.ToString() == postId)
            .Select(s => s.Comments).ToListAsync();

        return new CommentViewModel
        {
            PostId = postId,
            Comments = result,
        };
    }

    public async Task<bool> UpdateCommentAsync(string postId, string comment)
    {
        // find
        var find = await _context.Comments.FirstOrDefaultAsync(x => x.PostId.ToString() == postId);

        if (find == null) return await Task.FromResult(false);

        find.Comments = comment;

        try{
            _context.Comments.Update(find);
            await _context.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            _logger.LogError("unable to update comment", ex);       
            return await Task.FromResult(false);
        }

        return await Task.FromResult(true);
    }
}