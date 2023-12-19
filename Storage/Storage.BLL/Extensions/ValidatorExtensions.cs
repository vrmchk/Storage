using FluentValidation;
using FluentValidation.Results;
using Storage.BLL.Requests.Order.Models;
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

    public static IRuleBuilderOptions<T, ICollection<TProperty>> NotEmptyCollection<T, TProperty>(
        this IRuleBuilder<T, ICollection<TProperty>> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .Must(x => x.Count > 0)
            .WithMessage("Collection must not be empty");
    }

    public static IRuleBuilderOptions<T, ICollection<TProperty>> UniqueCollection<T, TProperty>(
        this IRuleBuilder<T, ICollection<TProperty>> ruleBuilder)
    {
        return ruleBuilder
            .Must(x => x.Distinct().Count() == x.Count)
            .WithMessage("Collection must not be unique");
    }

    public static IRuleBuilderOptions<T, CreateOrderSelectionModel> OrderSelectionRules<T>(
        this IRuleBuilderInitialCollection<T, CreateOrderSelectionModel> ruleBuilder)
    {
        return ruleBuilder.ChildRules(v =>
        {
            v.RuleFor(x => x.ProductId)
                .NotEmpty();

            v.RuleFor(x => x.Quantity)
                .GreaterThan(0);
        });
    }
}