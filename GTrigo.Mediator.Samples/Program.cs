using GTrigo.Mediator;
using GTrigo.Mediator.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Trigo.Mediator.Samples;

public abstract class Program
{
    public static async Task Main()
    {
        var services = new ServiceCollection();
        services.AddMediator();

        var serviceProvider = services.BuildServiceProvider();
        var mediator = serviceProvider.GetRequiredService<IMediator>();

        var userCreationRequest = new UserCreationRequest("john_doe", "john@example.com");

        var userCreationResponse = await mediator.SendAsync(userCreationRequest);
        Console.WriteLine($"{userCreationResponse.Message} - Success: {userCreationResponse.Success}");
    }

    private record UserCreationRequest(string Username, string Email) : IRequest<UserCreationResponse>;

    private record UserCreationResponse(string Message, bool Success);

    private class UserCreationHandler : IRequestHandler<UserCreationRequest, UserCreationResponse>
    {
        public Task<UserCreationResponse> HandleAsync(UserCreationRequest request, CancellationToken cancellationToken)
        {
            var response = new UserCreationResponse($"User {request.Username} created successfully", true);
            return Task.FromResult(response);
        }
    }
}