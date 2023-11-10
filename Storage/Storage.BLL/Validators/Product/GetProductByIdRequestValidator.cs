using FluentValidation;
using Storage.BLL.Requests.Product;

namespace Storage.BLL.Validators.Product;

public class GetProductByIdRequestValidator : AbstractValidator<GetProductByIdRequest>
{
    public GetProductByIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}