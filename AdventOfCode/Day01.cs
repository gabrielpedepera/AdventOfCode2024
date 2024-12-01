namespace AdventOfCode;

public class Day01 : BaseDay
{
    private readonly string _input;

    public Day01()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart1()}");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2");

    private string SolvePart1()
    {
        var lines = _input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        // Parse the input into two arrays of integers
        var leftNumbers = new int[lines.Length];
        var rightNumbers = new int[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            var cleanedLine = lines[i].Trim();
            var parts = cleanedLine.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            leftNumbers[i] = int.Parse(parts[0]);
            rightNumbers[i] = int.Parse(parts[1]);
        }

        // Sort the arrays
        Array.Sort(leftNumbers);
        Array.Sort(rightNumbers);

        // Calculate the distance between the two arrays
        var distanceValues = new int[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            distanceValues[i] = Math.Abs(rightNumbers[i] - leftNumbers[i]);
        }

        // Sum the distance values
        var sum = distanceValues.Sum();

        return sum.ToString();
    }
}