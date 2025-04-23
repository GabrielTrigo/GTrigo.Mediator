namespace GTrigo.Mediator.Abstractions;

/// <summary>
/// </summary>
public interface ISender
{
    /// <summary>
    ///     Asynchronously send a request to a single handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TResponse"></typeparam>
    /// <returns></returns>
    Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
}