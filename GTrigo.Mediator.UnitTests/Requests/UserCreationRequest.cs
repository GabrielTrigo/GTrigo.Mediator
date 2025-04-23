using GTrigo.Mediator.Abstractions;
using GTrigo.Mediator.UnitTests.Responses;

namespace GTrigo.Mediator.UnitTests.Requests;

public class UserCreationRequest : IRequest<UserCreationResponse>
{
    public required string Username { get; set; }
    public required string Email { get; set; }
}