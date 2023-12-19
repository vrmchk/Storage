using FluentValidation;
using Storage.BLL.Requests.Order;

namespace Storage.BLL.Validators.Order;

public class SearchOrdersRequestValidator : AbstractValidator<SearchOrdersRequest>
{
    public SearchOrdersRequestValidator()
    {
        When(x => x.From != null, () =>
        {
            RuleFor(x => x.From)
                .LessThan(x => DateTime.UtcNow);
        });

        When(x => x.From != null && x.To != null, () =>
        {
            RuleFor(x => x.From)
                .LessThan(x => x.To);
        });
    }
}