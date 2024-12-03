using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day3 : BaseDay
{
    private readonly string _input;

    public Day3()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart1()}");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart2()}");

    private string SolvePart1()
    {
        var matches = ExtractMulPatterns(_input);
        var result = MultiplyAndSum(matches);
        return result.ToString();
    }

    private List<(int, int)> ExtractMulPatterns(string input)
    {
        var pattern = @"mul\((\d{1,3}),(\d{1,3})\)";
        var matches = Regex.Matches(input, pattern);
        var result = new List<(int, int)>();

        foreach (Match match in matches)
        {
            int x = int.Parse(match.Groups[1].Value);
            int y = int.Parse(match.Groups[2].Value);
            result.Add((x, y));
        }

        return result;
    }

    private int MultiplyAndSum(List<(int, int)> pairs)
    {
        return pairs.Sum(pair => pair.Item1 * pair.Item2);
    }
    private string SolvePart2()
    {
        return "Not implemented";
    }
}
