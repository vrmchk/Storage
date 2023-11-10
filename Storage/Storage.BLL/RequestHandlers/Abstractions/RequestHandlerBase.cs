using ErrorOr;
using FluentValidation;
using MediatR;
using Storage.BLL.Extensions;
using Storage.BLL.Requests.Abstractions;

namespace Storage.BLL.RequestHandlers.Abstractions;

public abstract class RequestHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, ErrorOr<TResponse>>
    where TRequest : IRequestBase<TResponse>
{
    private readonly IValidator<TRequest> _validator;

    protected RequestHandlerBase(IValidator<TRequest> validator)
    {
        _validator = validator;
    }

    public async Task<ErrorOr<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Error.Validation(validationResult.Errors.ToErrorString());

        return await HandleInternal(request, cancellationToken);
    }

    protected abstract Task<ErrorOr<TResponse>> HandleInternal(TRequest request, CancellationToken cancellationToken);
}