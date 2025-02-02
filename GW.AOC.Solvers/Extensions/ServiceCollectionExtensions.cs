using System.Reflection;
using GW.AOC.Contracts.Models;
using GW.AOC.Contracts.Solvers;

// ReSharper disable UnusedMethodReturnValue.Local
// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSolverDependencies(this IServiceCollection services)
    {
        services.AddSolvers();

        return services;
    }

    private static IServiceCollection AddSolvers(this IServiceCollection services)
    {
        var solverBaseType = typeof(ISolver);

        var solvers = Assembly.GetExecutingAssembly()
            .ExportedTypes
            .Where(x => x is { IsClass: true, IsPublic: true } && solverBaseType.IsAssignableFrom(x));

        foreach (var solver in solvers)
        {
            services.AddKeyedTransient(typeof(ISolver), SolverKey.GetFromNamespace(solver.Namespace), solver);
        }

        return services;
    }
}
