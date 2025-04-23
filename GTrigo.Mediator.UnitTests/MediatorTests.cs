using System.Reflection;
using GTrigo.Mediator.Abstractions;
using GTrigo.Mediator.UnitTests.Handlers;
using GTrigo.Mediator.UnitTests.Requests;
using GTrigo.Mediator.UnitTests.Responses;
using Microsoft.Extensions.DependencyInjection;

namespace GTrigo.Mediator.UnitTests;

public class MediatorTests
{
    private readonly IServiceProvider _serviceProvider;

    public MediatorTests()
    {
        var services = new ServiceCollection();
        services.AddMediator();
        _serviceProvider = services.BuildServiceProvider();
    }

    [Fact]
    public async Task Send_PingRequest_ReturnsPong()
    {
        // Arrange
        var mediator = _serviceProvider.GetRequiredService<IMediator>();
        var request = new PingRequest();

        // Act
        var response = await mediator.SendAsync(request);

        // Assert
        Assert.Equal("Pong", response);
    }

    [Fact]
    public async Task Send_UserCreationRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var mediator = _serviceProvider.GetRequiredService<IMediator>();
        var request = new UserCreationRequest
        {
            Username = "john_doe",
            Email = "john@example.com"
        };

        // Act
        var response = await mediator.SendAsync(request);

        // Assert
        Assert.True(response.Success);
        Assert.Equal($"User {request.Username} created successfully", response.Message);
    }

    [Fact]
    public async Task Send_UnhandledRequest_ThrowsInvalidOperationException()
    {
        // Arrange
        var mediator = _serviceProvider.GetRequiredService<IMediator>();
        var request = new UnhandledRequest();

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => mediator.SendAsync(request));
    }

    [Fact]
    public async Task Send_NullRequest_ThrowsArgumentNullException()
    {
        // Arrange
        var mediator = _serviceProvider.GetRequiredService<IMediator>();
        PingRequest request = null!;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => mediator.SendAsync(request));
    }

    [Fact]
    public void AddMediator_RegistersAllHandlersInAssembly()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMediator(Assembly.GetExecutingAssembly());

        // Act
        var provider = services.BuildServiceProvider();
        var pingHandler = provider.GetService<IRequestHandler<PingRequest, string>>();
        var userCreationHandler = provider.GetService<IRequestHandler<UserCreationRequest, UserCreationResponse>>();

        // Assert
        Assert.NotNull(pingHandler);
        Assert.IsType<PingHandler>(pingHandler);
        Assert.NotNull(userCreationHandler);
        Assert.IsType<UserCreationHandler>(userCreationHandler);
    }
}