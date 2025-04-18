using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Trigo.Mediator.Abstractions;

namespace Trigo.Mediator;

public static class Extensions
{
    public static void AddMediator(this IServiceCollection services, params Assembly[]? assemblies)
    {
        services.AddTransient<IMediator, Mediator>();

        if (assemblies is null || assemblies.Length == 0)
            assemblies = [Assembly.GetCallingAssembly()];

        var handleInterfaceType = typeof(IRequestHandler<,>);

        foreach (var assembly in assemblies)
        {
            var handles = assembly
                .GetTypes()
                .Where(t => t is { IsClass: true, IsAbstract: false })
                .SelectMany(t => t.GetInterfaces(), (t, i) => new { Type = t, Interface = i })
                .Where(ti =>
                    ti.Interface.IsGenericType && ti.Interface.GetGenericTypeDefinition() == handleInterfaceType);

            foreach (var handler in handles)
                services.AddTransient(handler.Interface, handler.Type);
        }
    }
}