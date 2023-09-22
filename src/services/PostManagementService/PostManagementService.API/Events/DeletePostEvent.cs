using Empower.Infrastructure.EventBus.Events;

namespace PostManagementService.Events;

public record DeletePostEvent: IntegrationEvent
{
    public DeletePostEvent(string postId)
    {
        PostId = postId;
    }

    public string PostId { get; set ;} 
}