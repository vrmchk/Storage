using FluentValidation;
using Storage.BLL.Requests.Order;

namespace Storage.BLL.Validators.Order;

public class GetOrderByIdRequestValidator : AbstractValidator<GetOrderByIdRequest>
{
    public GetOrderByIdRequestValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty();

        RuleFor(x => x.UserId)
            .NotEmpty();
    }
}