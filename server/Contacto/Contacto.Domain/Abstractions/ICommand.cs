using MediatR;

namespace Contacto.Domain.Abstractions;

public interface ICommand<TResponse> : IRequest<TResponse> { }
public interface ICommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : ICommand<TResponse> { }
