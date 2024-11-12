using MediatR;

namespace Consumer;

public class NotificationExample : INotification
{
    public string BrandId { get; set; }
}
