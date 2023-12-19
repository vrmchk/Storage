using FluentValidation;
using Storage.BLL.Requests.Stock;

namespace Storage.BLL.Validators.Stock;

public class DeleteStockRequestValidator : AbstractValidator<DeleteStockRequest>
{
    public DeleteStockRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}