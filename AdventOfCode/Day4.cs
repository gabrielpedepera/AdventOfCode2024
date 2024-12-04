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

    private int CountWordXmasOccurrences(string[] grid, string word)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;
        int count = 0;

        int[][] directions =
        [
            [0, 1],   // Right
            [0, -1],  // Left
            [1, 0],   // Down
            [-1, 0],  // Up
            [1, 1],   // Down-Right
            [1, -1],  // Down-Left
            [-1, 1],  // Up-Right
            [-1, -1]  // Up-Left
        ];

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

    private string SolvePart2()
    {
        var grid = _input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        int count = CountXMasOccurrences(grid);
        return count.ToString();
    }

    private int CountXMasOccurrences(string[] grid)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;
        int count = 0;

        // Iterate through each cell in the grid, excluding the borders
        for (int row = 1; row < rows - 1; row++)
        {
            for (int col = 1; col < cols - 1; col++)
            {
                // Check if the current cell is 'A'
                if (grid[row][col] == 'A')
                {
                    if (IsXMasPattern(grid, row, col))
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }

    private bool IsXMasPattern(string[] grid, int centerRow, int centerCol)
    {
        // Define the directions for the "X" shape
        int[][] directions =
        [
            [-1, -1], // Up-Left
            [-1, 1],  // Up-Right
            [1, -1],  // Down-Left
            [1, 1]    // Down-Right
        ];

        char tl = grid[centerRow + directions[0][0]][centerCol + directions[0][1]];
        char tr = grid[centerRow + directions[1][0]][centerCol + directions[1][1]];
        char dl = grid[centerRow + directions[2][0]][centerCol + directions[2][1]];
        char dr = grid[centerRow + directions[3][0]][centerCol + directions[3][1]];

        // Check for the "M-A-S" pattern in the four diagonal directions
        if ((tl == 'S' && tr == 'S' && dl == 'M' && dr == 'M') ||
            (tl == 'M' && tr == 'M' && dl == 'S' && dr == 'S') ||
            (tl == 'S' && dl == 'S' && dr == 'M' && tr == 'M') ||
            (tl == 'M' && dl == 'M' && dr == 'S' && tr == 'S'))
        {
            return true;
        }

        return false;
    }
}