using MediatR;
using Microsoft.EntityFrameworkCore;
using Storage.BLL.Notifications;
using Storage.DAL.Repositories.Interfaces;
using Storage.Email.Models;
using Storage.Email.Services.Interfaces;
using E = Storage.DAL.Entities;

namespace Storage.BLL.NotificationHandlers;

public class OrderProcessingNotificationHandler : INotificationHandler<OrderProcessingNotification>
{
    private readonly IRepository<E.Order> _orderRepository;
    private readonly IEmailSender _emailSender;

    public OrderProcessingNotificationHandler(IRepository<E.Order> orderRepository, IEmailSender emailSender)
    {
        _orderRepository = orderRepository;
        _emailSender = emailSender;
    }

    public async Task Handle(OrderProcessingNotification notification, CancellationToken cancellationToken)
    {
        var order = await _orderRepository
            .Include(o => o.User)
            .FirstOrDefaultAsync(o => o.Id == notification.OrderId, cancellationToken);

        if (order == null)
            return;
     
        await _emailSender.SendEmailAsync(order.User.Email!, new OrderProcessingMessage
        {
            UserName = order.User.DisplayName,
            OrderId = order.Id.ToString()
        });
    }
}