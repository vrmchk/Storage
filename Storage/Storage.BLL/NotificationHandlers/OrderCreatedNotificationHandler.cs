using MediatR;
using Microsoft.EntityFrameworkCore;
using Storage.BLL.Notifications;
using Storage.Common.Enums;
using Storage.DAL.Repositories.Interfaces;
using E = Storage.DAL.Entities;

namespace Storage.BLL.NotificationHandlers;

public class OrderCreatedNotificationHandler : INotificationHandler<OrderCreatedNotification>
{
    private readonly IRepository<E.Order> _orderRepository;
    private readonly IRepository<E.OrderSelection> _orderSelectionRepository;
    private readonly IMediator _mediator;

    public OrderCreatedNotificationHandler(IRepository<E.Order> orderRepository,
        IRepository<E.OrderSelection> orderSelectionRepository,
        IMediator mediator)
    {
        _orderRepository = orderRepository;
        _orderSelectionRepository = orderSelectionRepository;
        _mediator = mediator;
    }

    public async Task Handle(OrderCreatedNotification notification, CancellationToken cancellationToken)
    {
        var order = await _orderRepository
            .Include(o => o.OrderSelections)
            .ThenInclude(os => os.Product)
            .ThenInclude(p => p.Stocks.Where(s => s.OrderSelectionId == null))
            .SingleOrDefaultAsync(o => o.Id == notification.OrderId, cancellationToken);

        if (order == null)
            return;

        var enoughStocks = order.OrderSelections.All(os => os.Product.Stocks.Count >= os.Quantity);
        if (enoughStocks)
            await ProcessOrder(order, cancellationToken);
    }

    private async Task ProcessOrder(E.Order order, CancellationToken cancellationToken)
    {
        order.Status = OrderStatus.Processing;
        foreach (var selection in order.OrderSelections)
        {
            selection.Stocks = selection.Product.Stocks.Take(selection.Quantity).ToList();
        }

        await _orderSelectionRepository.UpdateManyAsync(order.OrderSelections, cancellationToken);
        await _orderRepository.UpdateAsync(order, cancellationToken);
        await _mediator.Publish(new OrderProcessingNotification { OrderId = order.Id }, cancellationToken);
    }
}