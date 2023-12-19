using MediatR;

namespace Storage.BLL.Notifications;

public class OrderCreatedNotification : INotification
{
    public Guid OrderId { get; set; }
}