namespace Trigo.Mediator.Abstractions;

/// <summary>
/// 
/// </summary>
public interface IMediator
{
    Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
}