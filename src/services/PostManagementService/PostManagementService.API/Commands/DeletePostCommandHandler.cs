
using Empower.Infrastructure.EventBus.Interfaces;
using MediatR;
using PostManagementService.Commands;
using PostManagementService.Events;
using PostManagementService.Repository;
using PostService.Model;

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, bool>
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<DeletePostCommandHandler> _logger;
    private readonly IPostRepository _repository;
    
    public DeletePostCommandHandler(IEventBus eventBus, 
        ILogger<DeletePostCommandHandler> logger,
        IPostRepository repository)
    {
        _eventBus = eventBus; 
        _logger = logger;
        _repository = repository;
    }
    public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // delete from db
           bool result =  await _repository.DeletePostAsync(request.PostId);

           if (!result) 
           {
                _logger.LogInformation("something went wrong with delete");
                return false;
           }

            _logger.LogInformation("publishing to eventbus");
            // send to eventbus
            var eventMessage = new DeletePostEvent(request.PostId);
        
            _eventBus.Publish(eventMessage);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error Publishing integration event: {IntegrationEventId}", request.PostId);

            return false;
        }

        return true;
    }
}