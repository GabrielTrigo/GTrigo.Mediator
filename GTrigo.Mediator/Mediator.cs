using System.Collections.Concurrent;
using GTrigo.Mediator.Abstractions;

namespace GTrigo.Mediator;

/// <summary>
/// </summary>
/// <param name="serviceProvider"></param>
public sealed class Mediator(IServiceProvider serviceProvider) : IMediator
{
    private static readonly ConcurrentDictionary<Type, Type> RequestHandlers = new();

    /// <summary>
    ///     Dispatch event to handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TResponse"></typeparam>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var handlerType = RequestHandlers.GetOrAdd(request.GetType(),
            static requestType => typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse)));
        ArgumentNullException.ThrowIfNull(handlerType);

        var handler = serviceProvider.GetService(handlerType);
        ArgumentNullException.ThrowIfNull(handler);

        var method = handlerType.GetMethod("HandleAsync");
        ArgumentNullException.ThrowIfNull(method);

        if (method.Invoke(handler, [request, cancellationToken]) is not Task<TResponse> task)
            throw new InvalidOperationException(
                $"Handler {handler.GetType().Name} returned null or an incompatible type.");

        return task;
    }
}