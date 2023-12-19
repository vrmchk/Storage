using FluentValidation;
using Storage.BLL.Extensions;
using Storage.BLL.Requests.Order;

namespace Storage.BLL.Validators.Order;

public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.OrderSelections)
            .NotEmptyCollection()
            .UniqueCollection();

        RuleForEach(x => x.OrderSelections)
            .OrderSelectionRules();
    }
}