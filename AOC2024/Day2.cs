namespace AOC2024;

public static class Day2
{
    public static async Task Part1()
    {
        var result = 0;

        var input = await Util.Read("day2");


        foreach (var report in input)
        {
            var levels = report.Split(" ");
            var success = true;

            var ascending = int.Parse(levels[0]) < int.Parse(levels[1]);

            if (Math.Abs(int.Parse(levels[0]) - int.Parse(levels[1])) < 1 ||
                Math.Abs(int.Parse(levels[0]) - int.Parse(levels[1])) > 3)
            {
                success = false;
            }

            for (var i = 2; i < levels.Length; i++)
            {
                var first = int.Parse(levels[i - 1]);
                var second = int.Parse(levels[i]);

                if (ascending)
                {
                    if (first >= second)
                    {
                        success = false;
                    }
                }
                else
                {
                    if (second > first)
                    {
                        success = false;
                    }
                }

                if (Math.Abs(first - second) < 1 || Math.Abs(first - second) > 3)
                {
                    success = false;
                }
            }

            if (success) result++;
        }


        Console.WriteLine(result);
    }

    public static async Task Part2()
    {
        var result = 0;

        var input = await Util.Read("day2");


        foreach (var report in input)
        {
            var levels = report.Split(" ");
            
            for (var i = 0; i < levels.Length + 1; i++)
            {
                var success = true;

                List<string> dampenedLevels = [];
                for (var k = 0; k < levels.Length; k++)
                {
                    if (k == i && i < levels.Length) continue;
                    dampenedLevels.Add(levels[k]);
                }
                
                var ascending = int.Parse(dampenedLevels[0]) < int.Parse(dampenedLevels[1]);

                if (Math.Abs(int.Parse(dampenedLevels[0]) - int.Parse(dampenedLevels[1])) < 1 ||
                    Math.Abs(int.Parse(dampenedLevels[0]) - int.Parse(dampenedLevels[1])) > 3)
                {
                    success = false;
                }

                for (var j = 2; j < dampenedLevels.Count; j++)
                {
                    var first = int.Parse(dampenedLevels[j - 1]);
                    var second = int.Parse(dampenedLevels[j]);

                    if (ascending)
                    {
                        if (first >= second)
                        {
                            success = false;
                        }
                    }
                    else
                    {
                        if (second > first)
                        {
                            success = false;
                        }
                    }

                    if (Math.Abs(first - second) < 1 || Math.Abs(first - second) > 3)
                    {
                        success = false;
                    }
                }

                if (success)
                {
                    result++;
                    break;
                }
            }
        }

        Console.WriteLine(result);
    }
}