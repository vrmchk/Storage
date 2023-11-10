using FluentValidation;
using Storage.BLL.Requests.Stock;

namespace Storage.BLL.Validators.Stock;

public class GetStockByIdValidator : AbstractValidator<GetStockByIdRequest>
{
    public GetStockByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}