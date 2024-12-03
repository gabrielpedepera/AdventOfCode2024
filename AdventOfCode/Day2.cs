namespace AdventOfCode;

public class Day2 : BaseDay
{
    private readonly string _input;

    public Day2()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart1()}");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart2()}");

    private string SolvePart1()
    {
        var lines = _input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        var reports = GenerateReport(lines);

        int safeReportCount = reports.Count(IsSafeReport);
        return safeReportCount.ToString();
    }

    private string SolvePart2()
    {
        var lines = _input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        var reports = GenerateReport(lines);

        var safeReports = new List<int[]>();
        var unsafeReports = new List<int[]>();
        foreach (var report in reports)
        {
            if (IsSafeReport(report))
            {
                safeReports.Add(report);
            }
            else
            {
                unsafeReports.Add(report);
            }
        }

        var becomeSafeReports = unsafeReports.Where(BecomeSafeReport).ToArray();

        int totalSafeReportCount = safeReports.Count + becomeSafeReports.Length;

        return totalSafeReportCount.ToString();
    }

    private int[][] GenerateReport(string[] lines)
    {
        return lines
            .Select(line =>
                line.Split([' ', '\t'], StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray()
            ).ToArray();
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

    private bool BecomeSafeReport(int[] report)
    {
        for (int i = 0; i < report.Length; i++)
        {
            var modifiedReport = report.Where((_, index) => index != i).ToArray();
            if (IsSafeReport(modifiedReport))
            {
                return true;
            }
        }
        return false;
    }
}
