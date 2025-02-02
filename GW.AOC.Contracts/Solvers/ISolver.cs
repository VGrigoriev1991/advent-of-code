namespace GW.AOC.Contracts.Solvers;

public interface ISolver
{
    Task SolvePartOneAsync(CancellationToken cancellationToken);

    Task SolvePartTwoAsync(CancellationToken cancellationToken);
}
