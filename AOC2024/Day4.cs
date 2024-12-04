namespace AOC2024;

public static class Day4
{
    public static async Task Part1()
    {
        var input = await Util.Read("Day4");
        char[][] puzzle = input.Select(i => i.ToCharArray()).ToArray();
        List<HashSet<(int, int)>> results = [];

        for (var y = 0; y < puzzle.Length; y++)
        {
            var chars = puzzle[y];
            for (var x = 0; x < chars.Length; x++)
            {
                var c = chars[x];
                List<((int, int), char)> matches = GetAllCharactersNextTo((y, x), puzzle);
                
                foreach (var valueTuple in matches)
                {
                    if (IsCharacterAdjacent(valueTuple.Item2, c))
                    {
                        HashSet<((int, int),char)> line = [((y, x), c), valueTuple];
                        if (CheckNextInLine(((y, x), c), valueTuple, puzzle, ref line))
                        {
                            results.Add(line.Select(tuple => tuple.Item1).ToHashSet());
                        }
                    }
                }
            }
        }
        Console.WriteLine(results.Count / 2);
    }

    private static bool CheckNextInLine(((int, int), char) first, ((int, int), char) second, char[][] puzzle, ref HashSet<((int, int), char)> line)
    {
        var xDiff = first.Item1.Item1 - second.Item1.Item1;
        var yDiff = first.Item1.Item2 - second.Item1.Item2;
        
        switch (first.Item2)
        {
            case 'X' when second.Item2 == 'M':
                try
                {
                    var thirdX = second.Item1.Item1 - xDiff;
                    var thirdY = second.Item1.Item2 - yDiff;

                    if (puzzle[thirdX][thirdY] != 'A') return false;

                    var fourthX = thirdX - xDiff;
                    var fourthY = thirdY - yDiff;

                    if (puzzle[fourthX][fourthY] != 'S') return false;

                    line.Add(((thirdX, thirdY), puzzle[thirdX][thirdY]));
                    line.Add(((fourthX, fourthY), puzzle[fourthX][fourthY]));

                    return true;
                }
                catch (IndexOutOfRangeException)
                {
                    return false;
                }

                break;
            case 'S' when second.Item2 == 'A':
                try
                {
                    var thirdX = second.Item1.Item1 - xDiff;
                    var thirdY = second.Item1.Item2 - yDiff;

                    if (puzzle[thirdX][thirdY] != 'M') return false;

                    var fourthX = thirdX - xDiff;
                    var fourthY = thirdY - yDiff;

                    if (puzzle[fourthX][fourthY] != 'X') return false;

                    line.Add(((thirdX, thirdY), puzzle[thirdX][thirdY]));
                    line.Add(((fourthX, fourthY), puzzle[fourthX][fourthY]));

                    return true;
                }
                catch (IndexOutOfRangeException)
                {
                    return false;
                }

                break;
            case 'A' when second.Item2 == 'N':
                try
                {
                    var thirdX = second.Item1.Item1 + xDiff;
                    var thirdY = second.Item1.Item2 + yDiff;
                
                    if (puzzle[thirdX][thirdY] != 'X') return false;
                
                    var fourthX = second.Item1.Item1 - xDiff;
                    var fourthY = second.Item1.Item2 - yDiff;
                
                    if (puzzle[fourthX][fourthY] != 'S') return false;
                
                    line.Add(((thirdX, thirdY), puzzle[thirdX][thirdY]));
                    line.Add(((fourthX, fourthY), puzzle[fourthX][fourthY]));
                }
                catch (IndexOutOfRangeException)
                {
                    return false;
                }

                break;
        }


        return false;
    }

    private static bool IsCharacterAdjacent(char adjacent, char original)
    {
        return original switch
        {
            'X' => adjacent == 'M',
            'M' => adjacent is 'M' or 'A',
            'A' => adjacent is 'M' or 'S',
            'S' => adjacent == 'A',
            _ => false
        };
    }

    private static List<((int, int), char)> GetAllCharactersNextTo((int, int) coordinates, char[][] puzzle)
    {
        List<((int, int), char)> result = [];
        
        var (row, col) = GetRowAndColumn(coordinates, puzzle);
        var yStart = coordinates.Item1 == 0 ? 0 : -1;
        var yEnd = coordinates.Item1 == row.Length - 1 ? 0 : 1;
        var xStart = coordinates.Item2 == 0 ? 0 : -1;
        var xEnd = coordinates.Item2 == col.Length - 1 ? 0 : 1;

        for (var rowIndex = yStart; rowIndex <= yEnd; rowIndex++)
        {
            for (var colIndex = xStart; colIndex <= xEnd; colIndex++)
            {
                if (rowIndex == 0 && colIndex == 0)
                {
                    continue;
                }
                result.Add(((rowIndex + coordinates.Item1, colIndex + coordinates.Item2), puzzle[rowIndex + coordinates.Item1][colIndex + coordinates.Item2]));
            }
        }

        return result;
    }

    private static (char[] row, char[] column) GetRowAndColumn((int, int) coordinates, char[][] puzzle)
    {
        var row = puzzle[coordinates.Item2];
        var column = new char[puzzle.Length];
        for (var index = 0; index < puzzle.Length; index++)
        {
            var chars = puzzle[index];
            column[index] = chars[coordinates.Item1];
        }
        return (row, column);
    }
    
    public static async Task Part2()
    {
        var input = await Util.Read("Day4");
        char[][] puzzle = input.Select(i => i.ToCharArray()).ToArray();
        int totalMatches = 0;

        for (var y = 0; y < puzzle.Length; y++)
        {
            var chars = puzzle[y];
            for (var x = 0; x < chars.Length; x++)
            {
                var c = chars[x];

                if (c == 'A')
                {
                    if (CheckCross((y, x), puzzle))
                    {
                        totalMatches++;
                    }
                }
            }
        }
        Console.WriteLine(totalMatches);
    }
    
    private static bool CheckCross((int, int) middle, char[][] puzzle)
    {
        try
        {
            return (puzzle[middle.Item1 - 1][middle.Item2 - 1] == 'M' &&
                    puzzle[middle.Item1 + 1][middle.Item2 + 1] == 'S' ||
                    puzzle[middle.Item1 - 1][middle.Item2 - 1] == 'S' &&
                    puzzle[middle.Item1 + 1][middle.Item2 + 1] == 'M') &&
                   (puzzle[middle.Item1 + 1][middle.Item2 - 1] == 'M' &&
                    puzzle[middle.Item1 - 1][middle.Item2 + 1] == 'S' ||
                    puzzle[middle.Item1 + 1][middle.Item2 - 1] == 'S' &&
                    puzzle[middle.Item1 - 1][middle.Item2 + 1] == 'M');
        }
        catch (IndexOutOfRangeException)
        {
            return false;
        }
    }
}