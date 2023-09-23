using CommentManagementService.Commands;
using MediatR;
using CommentManagementService.Model;


public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, bool>
{
    private readonly ILogger<CreateCommentCommandHandler> _logger;
    private readonly ICommentRepository _repository;
    public CreateCommentCommandHandler(
        ILogger<CreateCommentCommandHandler> logger,
        ICommentRepository repository)
    {
       _logger = logger; 
       _repository = repository;
    }
    public async Task<bool> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {

        // save new record in database
        var comment = new Comment 
        {
            PostId = Guid.Parse(request.PostId),
            Comments = request.Comment,
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,

        };

        try{
            var created = _repository.CreateComment(comment);
            await _repository.UnitOfWork.SaveChangesAsync();
            return await Task.FromResult<bool>(true);
        }
        catch(Exception ex)
        {
            _logger.LogError("Cannot create comment", ex);
            return await Task.FromResult(false);
        }

    }
}
