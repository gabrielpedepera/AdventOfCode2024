namespace AdventOfCode;

public class Day8 : BaseDay
{
    private readonly string _input;

    public Day8()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart1()}");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart2()}");

    private string SolvePart1()
    {
        var inputLines = _input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        var grid = new char[inputLines.Length, inputLines[0].Length];
        for (int i = 0; i < inputLines.Length; i++)
        {
            for (int j = 0; j < inputLines[i].Length; j++)
            {
                grid[i, j] = inputLines[i][j];
            }
        }

        var antennaGroups = new List<AntennaGroup>();

        for (var x = 0; x < grid.GetLength(0); x++)
        {
            for (var y = 0; y < grid.GetLength(1); y++)
            {
                if (grid[x, y] == '.' || grid[x, y] == '#')
                {
                    continue;
                }

                var antenna = antennaGroups.SingleOrDefault(p => p.Char == grid[x, y]);

                if (antenna == null)
                {
                    antenna = new AntennaGroup(grid[x, y]);
                    antennaGroups.Add(antenna);
                }

                antenna.Positions.Add(new GridEntry
                {
                    X = x,
                    Y = y,
                    Value = grid[x, y]
                });
            }
        }

        var hashSetPartOne = new HashSet<(int, int)>();
        var hashSetPartTwo = new HashSet<(int, int)>();

        foreach (var antennaGroup in antennaGroups)
        {
            foreach (var antenna in antennaGroup.Positions)
            {
                foreach (var otherAntenna in antennaGroup.Positions)
                {
                    if (antenna == otherAntenna)
                    {
                        continue;
                    }

                    var xDiff = Math.Abs(antenna.X - otherAntenna.X);
                    var yDiff = Math.Abs(antenna.Y - otherAntenna.Y);

                    if (antenna.X < otherAntenna.X)
                    {
                        xDiff = -xDiff;
                    }
                    if (antenna.Y < otherAntenna.Y)
                    {
                        yDiff = -yDiff;
                    }

                    var rollingDiffX = antenna.X;
                    var rollingDiffY = antenna.Y;

                    if (IsInBounds(grid, antenna.X + xDiff, antenna.Y + yDiff))
                    {
                        hashSetPartOne.Add((antenna.X + xDiff, antenna.Y + yDiff));
                    }

                    while (true)
                    {
                        if (IsInBounds(grid, rollingDiffX, rollingDiffY))
                        {
                            hashSetPartTwo.Add((rollingDiffX, rollingDiffY));
                        }
                        else
                        {
                            break;
                        }

                        rollingDiffX += xDiff;
                        rollingDiffY += yDiff;
                    }
                }
            }
        }

        return hashSetPartOne.Count.ToString();
    }

    private string SolvePart2()
    {
        var inputLines = _input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        var grid = new char[inputLines.Length, inputLines[0].Length];
        for (int i = 0; i < inputLines.Length; i++)
        {
            for (int j = 0; j < inputLines[i].Length; j++)
            {
                grid[i, j] = inputLines[i][j];
            }
        }

        var antennaGroups = new List<AntennaGroup>();

        for (var x = 0; x < grid.GetLength(0); x++)
        {
            for (var y = 0; y < grid.GetLength(1); y++)
            {
                if (grid[x, y] == '.' || grid[x, y] == '#')
                {
                    continue;
                }

                var antenna = antennaGroups.SingleOrDefault(p => p.Char == grid[x, y]);

                if (antenna == null)
                {
                    antenna = new AntennaGroup(grid[x, y]);
                    antennaGroups.Add(antenna);
                }

                antenna.Positions.Add(new GridEntry
                {
                    X = x,
                    Y = y,
                    Value = grid[x, y]
                });
            }
        }

        var antinodes = new HashSet<(int, int)>();

        foreach (var antennaGroup in antennaGroups)
        {
            if (antennaGroup.Positions.Count < 2)
            {
                continue;
            }

            foreach (var antenna in antennaGroup.Positions)
            {
                antinodes.Add((antenna.X, antenna.Y));

                foreach (var otherAntenna in antennaGroup.Positions)
                {
                    if (antenna == otherAntenna)
                    {
                        continue;
                    }

                    if (antenna.X == otherAntenna.X)
                    {
                        for (int y = Math.Min(antenna.Y, otherAntenna.Y); y <= Math.Max(antenna.Y, otherAntenna.Y); y++)
                        {
                            antinodes.Add((antenna.X, y));
                        }
                    }

                    if (antenna.Y == otherAntenna.Y)
                    {
                        for (int x = Math.Min(antenna.X, otherAntenna.X); x <= Math.Max(antenna.X, otherAntenna.X); x++)
                        {
                            antinodes.Add((x, antenna.Y));
                        }
                    }

                    var dr = antenna.X - otherAntenna.X;
                    var dc = antenna.Y - otherAntenna.Y;

                    var row = antenna.X + dr;
                    var col = antenna.Y + dc;
                    while (IsInBounds(grid, row, col))
                    {
                        antinodes.Add((row, col));
                        row += dr;
                        col += dc;
                    }

                    row = otherAntenna.X - dr;
                    col = otherAntenna.Y - dc;
                    while (IsInBounds(grid, row, col))
                    {
                        antinodes.Add((row, col));
                        row -= dr;
                        col -= dc;
                    }
                }
            }
        }

        return antinodes.Count.ToString();
    }

    private bool IsInBounds(char[,] grid, int x, int y)
    {
        return x >= 0 && x < grid.GetLength(0) && y >= 0 && y < grid.GetLength(1);
    }

    private record AntennaGroup(char Char)
    {
        public List<GridEntry> Positions { get; } = new();
    }

    private record GridEntry
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Value { get; set; }
    }
}