using Empower.Infrastructure.EventBus.Interfaces;
using CommentManagementService.Events;

public class DeletePostEventHandler : IIntegrationEventHandler<DeletePostEvent>
{
    private readonly ILogger<DeletePostEventHandler> _logger;
    public DeletePostEventHandler(ILogger<DeletePostEventHandler> logger)
    {
        _logger = logger; 
    }
    public Task Handle(DeletePostEvent @event)
    {
        _logger.LogInformation("handling events like a boss");

        // delete comment from the database

        return Task.CompletedTask;
    }
}