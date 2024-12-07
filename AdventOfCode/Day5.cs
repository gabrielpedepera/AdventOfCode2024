namespace AdventOfCode;

public class Day5 : BaseDay
{
    private readonly string _input;

    public Day5()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart1()}");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart2()}");

    private string SolvePart1()
    {
        var sections = _input.Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
        var rules = sections[0].Split('\n').Select(line => line.Split('|').Select(int.Parse).ToArray()).ToArray();
        var updates = sections[1].Split('\n').Select(line => line.Split(',').Select(int.Parse).ToArray()).ToArray();

        var ruleDict = new Dictionary<int, HashSet<int>>();
        foreach (var rule in rules)
        {
            if (!ruleDict.ContainsKey(rule[0]))
            {
                ruleDict[rule[0]] = new HashSet<int>();
            }
            ruleDict[rule[0]].Add(rule[1]);
        }

        int sumOfMiddlePages = 0;

        foreach (var update in updates)
        {
            if (IsCorrectlyOrdered(update, ruleDict))
            {
                int middleIndex = update.Length / 2;
                sumOfMiddlePages += update[middleIndex];
            }
        }

        return sumOfMiddlePages.ToString();
    }

    private bool IsCorrectlyOrdered(int[] update, Dictionary<int, HashSet<int>> ruleDict)
    {
        for (int i = 0; i < update.Length; i++)
        {
            for (int j = i + 1; j < update.Length; j++)
            {
                if (ruleDict.ContainsKey(update[j]) && ruleDict[update[j]].Contains(update[i]))
                {
                    return false;
                }
            }
        }
        return true;
    }

    private string SolvePart2()
    {
        return "Not implemented";
    }
}
