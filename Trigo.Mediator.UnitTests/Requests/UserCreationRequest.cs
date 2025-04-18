using Trigo.Mediator.Abstractions;
using Trigo.Mediator.UnitTests.Responses;

namespace Trigo.Mediator.UnitTests.Requests;

public class UserCreationRequest : IRequest<UserCreationResponse>
{
    public string Username { get; set; }
    public string Email { get; set; }
}