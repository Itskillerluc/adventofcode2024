using System.Text.RegularExpressions;

namespace AOC2024;

public static partial class Day3
{
    public static async Task Part1()
    {
        var input = await Util.Read("day3");
        var singleLineInput = input.Aggregate("", (a, b) => a + b);
        var regex = RegexPart1();
        var matches = regex.Matches(singleLineInput).ToList();

        var result = matches
            .Select(match => match.Value.Split(','))
            .Select(split => int.Parse(split.First()) * int.Parse(split.Last()))
            .Sum();
        
        Console.WriteLine(result);
    }

    public static async Task Part2()
    {
        var result = 0;
        var input = await Util.Read("day3");
        var singleLineInput = input.Aggregate("", (a, b) => a + b);
        var regex = RegexPart2();
        var matches = regex.Matches(singleLineInput).ToList();
        var enabled = true;
        
        foreach (var match in matches)
        {
            switch (match.Value)
            {
                case "do()":
                    enabled = true;
                    continue;
                case "don't()":
                    enabled = false;
                    continue;
            }

            if (!enabled) continue;
            
            var split = match.Value.Split(',');
            result += int.Parse(split[0]) * int.Parse(split[1]);
        }
        
        Console.WriteLine(result);
    }

    [GeneratedRegex(@"(?<=mul\()\d{1,3},\d{1,3}(?=\))")]
    private static partial Regex RegexPart1();
    
    [GeneratedRegex(@"(?<=mul\()\d{1,3},\d{1,3}(?=\))|do\(\)|don't\(\)")]
    private static partial Regex RegexPart2();
}