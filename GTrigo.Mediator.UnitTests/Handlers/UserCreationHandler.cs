using GTrigo.Mediator.Abstractions;
using GTrigo.Mediator.UnitTests.Requests;
using GTrigo.Mediator.UnitTests.Responses;

namespace GTrigo.Mediator.UnitTests.Handlers;

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