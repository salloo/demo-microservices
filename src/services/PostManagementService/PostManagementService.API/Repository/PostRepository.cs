using PostService.Model;

namespace PostManagementService.Repository;

public class PostRepository : IPostRepository
{
    private readonly ILogger<PostRepository> _logger;
    public PostRepository(ILogger<PostRepository> logger)
    {
        _logger = logger;
    }

    public Task<PostViewModel> GetPostsAsync(string postId)
    {
        return Task.FromResult<PostViewModel>( new PostViewModel
        {
            PostId = Guid.Parse(postId),
            Name = "Very important post",
            Content = "loving this",

        });
    }
}