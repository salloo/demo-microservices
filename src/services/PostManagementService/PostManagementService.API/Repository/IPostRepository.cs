using PostService.Model;

namespace PostManagementService.Repository;

public interface IPostRepository
{
    Task<PostViewModel> GetPostsAsync(string postId);
}