namespace GTrigo.Mediator.Abstractions;

/// <summary>
///     Marker interface to represent a request without response.
/// </summary>
public interface IRequest : IBaseRequest;

/// <summary>
///     Marker interface to represent a request with a response.
/// </summary>
/// <typeparam name="TResponse">Response type</typeparam>
public interface IRequest<out TResponse> : IBaseRequest;

/// <summary>
///     Allows for generic types contraint of objects implementing IRequest or IRequest{TResponse}.
/// </summary>
public interface IBaseRequest;