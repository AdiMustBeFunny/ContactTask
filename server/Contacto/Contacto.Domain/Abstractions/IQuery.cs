using MediatR;

namespace Contacto.Domain.Abstractions;

//internal interface IQuery : IRequest { }
public interface IQuery<TResponse> : IRequest<TResponse> { }
public interface IQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IQuery<TResponse> { }
