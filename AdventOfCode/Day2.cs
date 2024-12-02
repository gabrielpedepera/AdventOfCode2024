namespace AdventOfCode;

public class Day_2 : BaseDay
{
    private readonly string _input;

    public Day_2()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart1()}");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart2()}");

    private string SolvePart1()
    {
        var lines = _input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        var reports = lines
                .Select(line =>
                    line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray()
                ).ToArray();

        int safeReportCount = reports.Count(IsSafeReport);
        return safeReportCount.ToString();
    }

    private string SolvePart2()
    {
        return "Not implemented";
    }

    private bool IsSafeReport(int[] report)
    {
        bool isIncreasing = report[1] > report[0];
        for (int i = 1; i < report.Length; i++)
        {
            int diff = report[i] - report[i - 1];
            if (Math.Abs(diff) < 1 || Math.Abs(diff) > 3)
            {
                return false; // Adjacent levels differ by less than 1 or more than 3
            }
            if ((isIncreasing && report[i] < report[i - 1]) || (!isIncreasing && report[i] > report[i - 1]))
            {
                return false; // Levels are not consistently increasing or decreasing
            }
        }

        return true;
    }
}
