
using Empower.Infrastructure.EventBus.Interfaces;
using MediatR;
using PostManagementService.Commands;
using PostManagementService.Events;
using PostService.Model;

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, bool>
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<DeletePostCommandHandler> _logger;
    
    public DeletePostCommandHandler(IEventBus eventBus, 
        ILogger<DeletePostCommandHandler> logger)
    {
        _eventBus = eventBus; 
        _logger = logger;
    }
    public Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        // TODO: delete from database
        
        // send to eventbus
        var eventMessage = new DeletePostEvent(request.PostId);
        
        try
        {
            _eventBus.Publish(eventMessage);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error Publishing integration event: {IntegrationEventId}", eventMessage.Id);

            return Task.FromResult(false);
        }

        return Task.FromResult(true);
    }
}