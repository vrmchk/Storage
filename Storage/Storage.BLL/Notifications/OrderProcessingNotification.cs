using MediatR;

namespace Storage.BLL.Notifications;

public class OrderProcessingNotification : INotification
{
    public Guid OrderId { get; set; }
}