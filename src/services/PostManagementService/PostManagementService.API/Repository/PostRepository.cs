using PostService.Data;
using PostService.Model;

namespace PostManagementService.Repository;

public class PostRepository : IPostRepository
{
    private readonly PostContext _context;
    private readonly ILogger<PostRepository> _logger;

    public IUnitOfWork UnitOfWork => _context;

    public PostRepository(PostContext context, 
        ILogger<PostRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger;
    }

    public async Task<Post> GetPostAsync(string Id)
    {
        return await _context.Posts.SingleOrDefaultAsync(x => x.PostId.ToString() == Id);
    }

    public Post CreatePost(Post post)
    {
        return _context.Add(post).Entity;
    }

    public async Task<bool> DeletePostAsync(string postId)
    {
        var find  = await _context.Posts
            .SingleOrDefaultAsync(x => x.PostId.ToString() == postId);

        if(find == null)
        {
            _logger.LogError("unable to find the post");
            return false;
        }

        try 
        {
            _context.Posts.Remove(find); 
            await _context.SaveChangesAsync();

        }
        catch(Exception ex)
        {
            _logger.LogError("Unable to delete", ex);
            return false;
        }

        return true;
    }
}


   