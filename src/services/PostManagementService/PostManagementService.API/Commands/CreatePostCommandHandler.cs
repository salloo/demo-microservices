// TODO: Inject repository

using MediatR;
using PostManagementService.Commands;
using PostManagementService.Repository;
using PostService.Model;

public class CreatePostCommandHandler: IRequestHandler<CreatePostCommand, Post>
{
    private readonly ILogger<CreatePostCommandHandler> _logger;
    private readonly IPostRepository _repository;
    public CreatePostCommandHandler( ILogger<CreatePostCommandHandler> logger, 
        IPostRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }
    
    public async Task<Post> Handle(CreatePostCommand message, CancellationToken token)
    {
        var post = new Post {
            Name = message.Name,
            PostId = Guid.NewGuid(),
            Content = message.Content,
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,

        };
        _logger.LogInformation("saving post data");
        
        try {

            var createdEntity = _repository.CreatePost(post);
            await _repository.UnitOfWork.SaveChangesAsync(token);
            return await Task.FromResult<Post>(createdEntity);
        }
        catch(Exception ex) {
            _logger.LogError("Cannot create post", ex);
            return await Task.FromResult<Post>(null);
        }
    }
}