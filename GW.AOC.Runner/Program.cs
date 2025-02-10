using GW.AOC.Contracts.Solvers;
using GW.AOC.Solvers.Models;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddApplicationDependencies()
    .BuildServiceProvider();

var cancellationTokenSource = new CancellationTokenSource();

const ushort year = 2024;
const ushort day = 7;

try
{
    var solver = serviceProvider.GetRequiredKeyedService<ISolver>(SolverKey.Get(year, day));

    await solver.SolvePartOneAsync(cancellationTokenSource.Token);

    await solver.SolvePartTwoAsync(cancellationTokenSource.Token);
}
catch (Exception ex)
{
    Console.WriteLine("Unfortunately, an unexpected error occurred: {0}", ex.Message);
}
