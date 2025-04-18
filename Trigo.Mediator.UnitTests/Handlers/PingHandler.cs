using Trigo.Mediator.Abstractions;
using Trigo.Mediator.UnitTests.Requests;

namespace Trigo.Mediator.UnitTests.Handlers;

public class PingHandler : IRequestHandler<PingRequest, string>
{
    public Task<string> HandleAsync(PingRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult("Pong");
    }
}