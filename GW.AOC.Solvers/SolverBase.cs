using GW.AOC.Solvers.Models;

namespace GW.AOC.Solvers;

public abstract class SolverBase
{
    protected string PuzzleDataFilePath => PuzzleDataFileInfo.GetFilePathBySolverNamespace(GetType().Namespace!);
}
