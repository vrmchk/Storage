using FluentValidation;
using Storage.BLL.Requests.Product;

namespace Storage.BLL.Validators.Product;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Description)
            .NotNull();

        RuleFor(x => x.Price)
            .NotEmpty();
    }
}