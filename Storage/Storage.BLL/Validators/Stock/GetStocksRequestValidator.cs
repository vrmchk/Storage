using FluentValidation;
using Storage.BLL.Requests.Stock;

namespace Storage.BLL.Validators.Stock;

public class GetStocksRequestValidator : AbstractValidator<GetStocksRequest>
{
    public GetStocksRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();
    }
}