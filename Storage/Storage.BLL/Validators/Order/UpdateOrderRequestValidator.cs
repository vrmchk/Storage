using FluentValidation;
using Storage.BLL.Requests.Order;

namespace Storage.BLL.Validators.Order;

public class UpdateOrderRequestValidator : AbstractValidator<UpdateOrderRequest>
{
    public UpdateOrderRequestValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty();

        RuleFor(x => x.Status)
            .IsInEnum();
    }
}