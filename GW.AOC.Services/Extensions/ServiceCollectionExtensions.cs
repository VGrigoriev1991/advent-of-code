using GW.AOC.Contracts.Services;
using GW.AOC.Services;

// ReSharper disable UnusedMethodReturnValue.Local
// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services) =>
        services.AddTransient<IPuzzleDataReader, PuzzleDataFileReader>();
}
