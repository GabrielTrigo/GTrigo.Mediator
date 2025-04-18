using Trigo.Mediator.Abstractions;
using Trigo.Mediator.UnitTests.Responses;

namespace Trigo.Mediator.UnitTests.Requests;

public class UserCreationRequest : IRequest<UserCreationResponse>
{
    public required string Username { get; set; }
    public required string Email { get; set; }
}