namespace AdventOfCode;

public class Day6 : BaseDay
{
    private readonly string _input;

    public Day6()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart1()}");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, {SolvePart2()}");

    private enum FACING
    {
        UP,
        RIGHT,
        DOWN,
        LEFT
    }

    private class State
    {
        public int X { get; set; }
        public int Y { get; set; }
        public FACING Face { get; set; }
    }

    private string SolvePart1()
    {
        var positions = new List<(int, int)>();
        var data = _input.Split('\n').Select(x => x.ToCharArray().ToList()).ToList();

        var xPos = data.FindIndex(x => x.Contains('^'));
        var yPos = data[xPos].IndexOf('^');

        var facing = FACING.UP;

        try
        {
            while (true)
            {
                positions.Add((xPos, yPos));

                switch (facing)
                {
                    case FACING.UP:
                        if (data[xPos - 1][yPos] == '#')
                        {
                            facing = FACING.RIGHT;
                        }
                        else
                        {
                            xPos--;
                        }
                        break;
                    case FACING.DOWN:
                        if (data[xPos + 1][yPos] == '#')
                        {
                            facing = FACING.LEFT;
                        }
                        else
                        {
                            xPos++;
                        }
                        break;
                    case FACING.LEFT:
                        if (data[xPos][yPos - 1] == '#')
                        {
                            facing = FACING.UP;
                        }
                        else
                        {
                            yPos--;
                        }
                        break;
                    case FACING.RIGHT:
                        if (data[xPos][yPos + 1] == '#')
                        {
                            facing = FACING.DOWN;
                        }
                        else
                        {
                            yPos++;
                        }
                        break;
                    default:
                        throw new Exception();
                }
            }
        }
        catch (Exception)
        {
            // Guard has left the mapped area
        }

        return positions.Distinct().Count().ToString();
    }

    private string SolvePart2()
    {
        var data = _input.Split('\n').Select(x => x.ToCharArray().ToList()).ToList();
        var xPos = data.FindIndex(x => x.Contains('^'));
        var yPos = data[xPos].IndexOf('^');

        var initialX = xPos;
        var initialY = yPos;
        var initialFacing = FACING.UP;

        var positions = new List<(int, int)>();
        var possibleObstructionPositions = new List<(int, int)>();

        // Simulate the guard's movement without any new obstructions
        var facing = initialFacing;
        try
        {
            while (true)
            {
                positions.Add((xPos, yPos));

                switch (facing)
                {
                    case FACING.UP:
                        if (data[xPos - 1][yPos] == '#')
                        {
                            facing = FACING.RIGHT;
                        }
                        else
                        {
                            xPos--;
                        }
                        break;
                    case FACING.DOWN:
                        if (data[xPos + 1][yPos] == '#')
                        {
                            facing = FACING.LEFT;
                        }
                        else
                        {
                            xPos++;
                        }
                        break;
                    case FACING.LEFT:
                        if (data[xPos][yPos - 1] == '#')
                        {
                            facing = FACING.UP;
                        }
                        else
                        {
                            yPos--;
                        }
                        break;
                    case FACING.RIGHT:
                        if (data[xPos][yPos + 1] == '#')
                        {
                            facing = FACING.DOWN;
                        }
                        else
                        {
                            yPos++;
                        }
                        break;
                    default:
                        throw new Exception();
                }
            }
        }
        catch (Exception)
        {
            // Guard has left the mapped area
        }

        // Check each position visited by the guard
        foreach (var position in positions.Distinct())
        {
            if (position == (initialX, initialY))
                continue;

            var copy = data.Select(row => row.ToList()).ToList();
            copy[position.Item1][position.Item2] = '#';

            xPos = initialX;
            yPos = initialY;
            facing = initialFacing;

            var visited = new HashSet<(int, int, FACING)>();

            try
            {
                while (true)
                {
                    if (visited.Contains((xPos, yPos, facing)))
                    {
                        possibleObstructionPositions.Add(position);
                        break;
                    }

                    visited.Add((xPos, yPos, facing));

                    switch (facing)
                    {
                        case FACING.UP:
                            if (copy[xPos - 1][yPos] == '#')
                            {
                                facing = FACING.RIGHT;
                            }
                            else
                            {
                                xPos--;
                            }
                            break;
                        case FACING.DOWN:
                            if (copy[xPos + 1][yPos] == '#')
                            {
                                facing = FACING.LEFT;
                            }
                            else
                            {
                                xPos++;
                            }
                            break;
                        case FACING.LEFT:
                            if (copy[xPos][yPos - 1] == '#')
                            {
                                facing = FACING.UP;
                            }
                            else
                            {
                                yPos--;
                            }
                            break;
                        case FACING.RIGHT:
                            if (copy[xPos][yPos + 1] == '#')
                            {
                                facing = FACING.DOWN;
                            }
                            else
                            {
                                yPos++;
                            }
                            break;
                        default:
                            throw new InvalidOperationException("Invalid facing direction");
                    }

                    // Check if the guard has left the mapped area
                    if (xPos < 0 || xPos >= copy.Count || yPos < 0 || yPos >= copy[0].Count)
                    {
                        break;
                    }
                }
            }
            catch (Exception)
            {
                // Guard has left the mapped area
            }
        }

        return possibleObstructionPositions.Count.ToString();
    }
}