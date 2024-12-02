namespace AdventOfCode;

public class Day1 : BaseDay
{
    private readonly string _input;

    public Day1()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart1()}");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart2()}");

    private string SolvePart1()
    {
        var lines = _input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        var (leftNumbers, rightNumbers) = ParseAndSortInput(lines);

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

    private string SolvePart2()
    {
        var lines = _input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        var (leftNumbers, rightNumbers) = ParseAndSortInput(lines);

        var similarityScore = new int[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            similarityScore[i] = leftNumbers[i] * CountOccurrences(leftNumbers[i], rightNumbers);
        }

        // Sum the similarity scores
        var sum = similarityScore.Sum();

        return sum.ToString();
    }

    private int CountOccurrences(int number, int[] array)
    {
        return array.Count(n => n == number);
    }

    private (int[] leftNumbers, int[] rightNumbers) ParseAndSortInput(string[] lines)
    {
        var leftNumbers = new int[lines.Length];
        var rightNumbers = new int[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            var cleanedLine = lines[i].Trim();
            var parts = cleanedLine.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            leftNumbers[i] = int.Parse(parts[0]);
            rightNumbers[i] = int.Parse(parts[1]);
        }

        Array.Sort(leftNumbers);
        Array.Sort(rightNumbers);

        return (leftNumbers, rightNumbers);
    }
}