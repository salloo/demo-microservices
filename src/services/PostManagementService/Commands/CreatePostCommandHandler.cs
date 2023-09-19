// TODO: Inject repository

using MediatR;
using PostManagementService.Commands;
using PostService.Model;

public class CreatePostCommandHandler: IRequestHandler<CreatePostCommand, bool>
{
    private readonly ILogger<CreatePostCommandHandler> _logger;
    public CreatePostCommandHandler( ILogger<CreatePostCommandHandler> logger/* repository inject */)
    {
        _logger = logger;
        
    }
    
    public async Task<bool> Handle(CreatePostCommand message, CancellationToken token)
    {
        var post = new Post {
            Name = message.Name,
            Content = message.Content,
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,

        };

        _logger.LogInformation("saving post data");

        // TODO: Add post to database

        return await Task.FromResult<bool>(true);        
    }
}