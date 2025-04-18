using Trigo.Mediator.Abstractions;

namespace Trigo.Mediator;

public sealed class Mediator(IServiceProvider serviceProvider) : IMediator
{
    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var requestType = request.GetType();
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));

        var handlerObj = serviceProvider.GetService(handlerType);
        ArgumentNullException.ThrowIfNull(handlerObj);

        var method = handlerType.GetMethod("HandleAsync");
        ArgumentNullException.ThrowIfNull(method);

        if (method.Invoke(handlerObj, [request, cancellationToken]) is not Task<TResponse> task)
            throw new InvalidOperationException(
                $"Handler {handlerObj.GetType().Name} returned null or an incompatible type.");
        
        return await task;
    }
}