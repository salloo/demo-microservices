using PostService.Model;

namespace PostManagementService.Repository;

public interface IPostRepository: IRepository<Post>
{
    Task<Post> GetPostAsync(string Id);
    Post CreatePost(Post post);
    Task<bool> DeletePostAsync(string postId);
}