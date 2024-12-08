namespace AdventOfCode;

public class Day7 : BaseDay
{
    private readonly string _input;

    public Day7()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart1()}");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart2()}");

    private string SolvePart1()
    {
        var lines = _input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        long totalCalibrationResult = 0;

        foreach (var line in lines)
        {
            var parts = line.Split(':');
            long testValue = long.Parse(parts[0]);
            var numbers = parts[1].Trim().Split(' ').Select(long.Parse).ToArray();

            if (CanBeTrue(testValue, numbers))
            {
                totalCalibrationResult += testValue;
            }
        }

        return totalCalibrationResult.ToString();
    }

    private bool CanBeTrue(long testValue, long[] numbers)
    {
        return Evaluate(numbers, 1, numbers[0], testValue);
    }

    private bool Evaluate(long[] numbers, int index, long currentValue, long testValue)
    {
        if (index == numbers.Length)
        {
            return currentValue == testValue;
        }

        long nextNumber = numbers[index];

        return Evaluate(numbers, index + 1, currentValue + nextNumber, testValue) ||
               Evaluate(numbers, index + 1, currentValue * nextNumber, testValue);
    }

    private string SolvePart2()
    {
        var lines = _input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        long totalCalibrationResult = 0;

        foreach (var line in lines)
        {
            var parts = line.Split(':');
            long testValue = long.Parse(parts[0]);
            var numbers = parts[1].Trim().Split(' ').Select(long.Parse).ToArray();

            if (CanBeTrueWithConcatenation(testValue, numbers))
            {
                totalCalibrationResult += testValue;
            }
        }

        return totalCalibrationResult.ToString();
    }

    private bool CanBeTrueWithConcatenation(long testValue, long[] numbers)
    {
        return EvaluateWithConcatenation(numbers, 1, numbers[0], testValue);
    }

    private bool EvaluateWithConcatenation(long[] numbers, int index, long currentValue, long testValue)
    {
        if (index == numbers.Length)
        {
            return currentValue == testValue;
        }

        long nextNumber = numbers[index];

        return EvaluateWithConcatenation(numbers, index + 1, currentValue + nextNumber, testValue) ||
               EvaluateWithConcatenation(numbers, index + 1, currentValue * nextNumber, testValue) ||
               EvaluateWithConcatenation(numbers, index + 1, Concatenate(currentValue, nextNumber), testValue);
    }

    private long Concatenate(long left, long right)
    {
        return long.Parse($"{left}{right}");
    }
}