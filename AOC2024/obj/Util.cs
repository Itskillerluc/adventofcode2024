﻿using System.Reflection;

namespace AOC2024.obj;

public static class Util
{
    public static async Task<string[]> Read(string day)
    {
        var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, @$"inputs\{day}.txt");
        return await File.ReadAllLinesAsync(path);
    }
}