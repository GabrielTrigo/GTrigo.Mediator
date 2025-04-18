using Trigo.Mediator.Abstractions;
using Trigo.Mediator.UnitTests.Requests;
using Trigo.Mediator.UnitTests.Responses;

namespace Trigo.Mediator.UnitTests.Handlers;

public class UserCreationHandler : IRequestHandler<UserCreationRequest, UserCreationResponse>
{
    public Task<UserCreationResponse> HandleAsync(UserCreationRequest request, CancellationToken cancellationToken)
    {
        var response = new UserCreationResponse
        {
            Success = true,
            Message = $"User {request.Username} created successfully"
        };
        return Task.FromResult(response);
    }
}
