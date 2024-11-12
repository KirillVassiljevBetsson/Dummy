using MediatR;
using Microsoft.Extensions.Logging;

namespace Consumer;

public class NotificationExampleHandler : INotificationHandler<NotificationExample>
{
    private readonly ILogger<NotificationExampleHandler> _logger;

    public NotificationExampleHandler(ILogger<NotificationExampleHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(NotificationExample notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Handled message: {notification.BrandId}");
        return Task.CompletedTask;
    }
}
