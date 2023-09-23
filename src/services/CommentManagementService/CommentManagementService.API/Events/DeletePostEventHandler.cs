using Empower.Infrastructure.EventBus.Interfaces;
using CommentManagementService.Events;
using Microsoft.EntityFrameworkCore;

public class DeletePostEventHandler : IIntegrationEventHandler<DeletePostEvent>
{
    private readonly ILogger<DeletePostEventHandler> _logger;
    private readonly CommentContext _context;
    public DeletePostEventHandler(ILogger<DeletePostEventHandler> logger,
        CommentContext context)
    {
        _logger = logger; 
        _context = context;
    }
    public async Task Handle(DeletePostEvent @event)
    {
        _logger.LogInformation("handling events like a boss");

        try{
            // delete all the comments
            var comments = await _context.Comments.Where(x => x.PostId.ToString() == @event.PostId).ToListAsync();

            if (!comments.Any()) throw new Exception($"cannot find the comments: {@event.PostId}");

            //_context.Entry(entity).State = EntityState.Deleted;
            _logger.LogInformation("saving comment");

            foreach(var c in comments)
            {
                _context.Comments.Remove(c);
            }

            await _context.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            _logger.LogError("unable to delete comment", ex);
        }
    }
}