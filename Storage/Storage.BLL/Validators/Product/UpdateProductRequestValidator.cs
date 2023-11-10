using FluentValidation;
using Storage.BLL.Requests.Product;

namespace Storage.BLL.Validators.Product;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Description)
            .NotNull();

        RuleFor(x => x.Price)
            .NotEmpty();
    }
}