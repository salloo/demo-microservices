using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MediatR;

namespace CommentManagementService.Commands;

public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, bool>
{
    private readonly ILogger<UpdateCommentCommandHandler> _logger;
    private readonly ICommentRepository _repository;
    public UpdateCommentCommandHandler(ILogger<UpdateCommentCommandHandler> logger,
        ICommentRepository repository)
    {
        _logger = logger; 
        _repository = repository;
    }
    public async Task<bool> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        // call the repository
        var result = await _repository.UpdateCommentAsync(request.PostId, request.CommentText);
        if (!result)
        {
            _logger.LogError("unable to update comment"); 
        }

        return await Task.FromResult(true);
    }
}

