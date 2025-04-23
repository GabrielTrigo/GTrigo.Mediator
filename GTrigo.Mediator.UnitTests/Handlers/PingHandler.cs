using GTrigo.Mediator.Abstractions;
using GTrigo.Mediator.UnitTests.Requests;

namespace GTrigo.Mediator.UnitTests.Handlers;

public class PingHandler : IRequestHandler<PingRequest, string>
{
    public Task<string> HandleAsync(PingRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult("Pong");
    }
}