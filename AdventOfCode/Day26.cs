namespace AdventOfCode;

public class Day26 : BaseDay
{
    private readonly string _input;

    public Day26()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart1()}");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart2()}");

    private string SolvePart1()
    {
        return "Not implemented";
    }

    private string SolvePart2()
    {
        return "Not implemented";
    }
}
