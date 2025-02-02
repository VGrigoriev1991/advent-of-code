namespace GW.AOC.Solvers.Models;

public static class PuzzleDataFileInfo
{
    private const string RootDirectory = "Data";
    private const string DataFileName = "puzzle_input.txt";

    public static string GetFilePathBySolverNamespace(string solverNamespace) =>
        Path.Combine(
            RootDirectory,
            SolverKey.GetNamespacePart(solverNamespace, SolverKey.Year, false),
            SolverKey.GetNamespacePart(solverNamespace, SolverKey.Day, false),
            DataFileName);
}
