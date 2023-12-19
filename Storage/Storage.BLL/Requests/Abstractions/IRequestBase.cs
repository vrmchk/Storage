using ErrorOr;
using MediatR;

namespace Storage.BLL.Requests.Abstractions;

public interface IRequestBase<TResponse> : IRequest<ErrorOr<TResponse>> { }