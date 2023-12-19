using MediatR;

namespace Storage.BLL.Notifications;

public class StocksAddedNotification : INotification
{
    public Guid ProductId { get; set; }
}