using CommentManagementService.Commands;
using MediatR;
using CommentManagementService.Model;


public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, bool>
{
    private readonly ILogger<CreateCommentCommandHandler> _logger;
    public CreateCommentCommandHandler(ILogger<CreateCommentCommandHandler> logger)
    {
       _logger = logger; 
    }
    public Task<bool> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {

        // save new record in database

        return Task.FromResult<bool>(true);
    }
}
