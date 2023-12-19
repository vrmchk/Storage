using FluentValidation;
using Storage.BLL.Requests.Users;

namespace Storage.BLL.Validators.Users;

public class MakeManagerRequestValidator : AbstractValidator<MakeManagerRequest>
{
    public MakeManagerRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}