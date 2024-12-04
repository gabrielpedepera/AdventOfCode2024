namespace AdventOfCode;

public class Day4 : BaseDay
{
    private readonly string _input;

    public Day4()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart1()}");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart2()}");

    private string SolvePart1()
    {
        var grid = _input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        var word = "XMAS";
        int count = CountWordXmasOccurrences(grid, word);
        return count.ToString();
    }

    private string SolvePart2()
    {
        return "Not implemented";
    }

    private int CountWordXmasOccurrences(string[] grid, string word)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;
        int count = 0;

        // Define all 8 possible directions
        int[][] directions = new int[][]
        {
            new int[] { 0, 1 },   // Right
            new int[] { 0, -1 },  // Left
            new int[] { 1, 0 },   // Down
            new int[] { -1, 0 },  // Up
            new int[] { 1, 1 },   // Down-Right
            new int[] { 1, -1 },  // Down-Left
            new int[] { -1, 1 },  // Up-Right
            new int[] { -1, -1 }  // Up-Left
        };

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                foreach (var direction in directions)
                {
                    if (IsWordInDirection(grid, word, row, col, direction))
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }

    private bool IsWordInDirection(string[] grid, string word, int startRow, int startCol, int[] direction)
    {
        int wordLength = word.Length;
        int rows = grid.Length;
        int cols = grid[0].Length;

        for (int i = 0; i < wordLength; i++)
        {
            int newRow = startRow + i * direction[0];
            int newCol = startCol + i * direction[1];

            if (newRow < 0 || newRow >= rows || newCol < 0 || newCol >= cols || grid[newRow][newCol] != word[i])
            {
                return false;
            }
        }

        return true;
    }
}