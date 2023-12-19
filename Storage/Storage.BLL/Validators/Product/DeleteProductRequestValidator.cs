using FluentValidation;
using Storage.BLL.Requests.Product;

namespace Storage.BLL.Validators.Product;

public class DeleteProductRequestValidator : AbstractValidator<DeleteProductRequest>
{
    public DeleteProductRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}