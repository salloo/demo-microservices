using PostService.Data;
using PostService.Model;

namespace PostManagementService.Repository;

public class PostRepository : IPostRepository
{
    private readonly PostContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public PostRepository(PostContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Post> GetPostAsync(string Id)
    {
        return await _context.Posts.SingleOrDefaultAsync(x => x.PostId.ToString() == Id);
    }

    public Post CreatePost(Post post)
    {
        return _context.Add(post).Entity;
    }
}


   