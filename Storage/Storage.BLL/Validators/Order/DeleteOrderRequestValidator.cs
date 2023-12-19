using FluentValidation;
using Storage.BLL.Requests.Order;

namespace Storage.BLL.Validators.Order;

public class DeleteOrderRequestValidator : AbstractValidator<DeleteOrderRequest>
{
    public DeleteOrderRequestValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty();
    }
}