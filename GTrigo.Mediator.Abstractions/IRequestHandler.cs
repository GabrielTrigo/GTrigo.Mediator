namespace GTrigo.Mediator.Abstractions;

/// <summary>
///     Defines a handler for a request.
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public interface IRequestHandler<in TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
}