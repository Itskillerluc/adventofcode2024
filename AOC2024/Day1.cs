namespace AOC2024;

public static class Day1
{
    public static async Task Part1()
    {
        var input = await Util.Read("day1");
        
        var subLists = input.Select(line => line.Split("   ")).ToList();
        var firstList = subLists.Select(list => int.Parse(list.First())).ToList();
        var secondList = subLists.Select(list => int.Parse(list.Last())).ToList();

        List<int> differenceList = [];

        var count = firstList.Count;
        
        for (var i = 0; i < count; i++)
        {
            var smallestNum1 = firstList.Min();
            firstList.Remove(smallestNum1);
            
            var smallestNum2 = secondList.Min();
            secondList.Remove(smallestNum2);
            
            differenceList.Add(Math.Abs(smallestNum1 - smallestNum2));
        }
        
        Console.WriteLine(differenceList.Sum());
    }

    public static async Task Part2()
    { 
        var input = await Util.Read("day1");
        
        var subLists = input.Select(line => line.Split("   ")).ToList();
        var firstList = subLists.Select(list => int.Parse(list.First())).ToList();
        var secondList = subLists.Select(list => int.Parse(list.Last())).ToList();

        Console.WriteLine(firstList.Sum(i => secondList.Count(num => num == i) * i));
    }
}