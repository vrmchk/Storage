using FluentValidation;
using FluentValidation.Results;
using Storage.BLL.Utility;

namespace Storage.BLL.Extensions;

public static class ValidatorExtensions
{
    public static string ToErrorString(this IEnumerable<ValidationFailure> errors)
    {
        return string.Join(Environment.NewLine, errors.Select(e => e.ErrorMessage));
    }
    
    public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder,
        int minimumLength = 8)
    {
        return ruleBuilder
            .MinimumLength(minimumLength)
            .Matches(Regexes.Password)
            .WithMessage(
                "Password must contain at least one uppercase letter, one lowercase letter, one digit and one special character");
    }
}