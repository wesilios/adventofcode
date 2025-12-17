using AoC2025.Solutions.Abstractions;
using Microsoft.Extensions.Logging;

namespace AoC2025.Solutions.Day05;

public class CafeteriaPartOne : ISolution
{
    public CafeteriaPartOne(ILogger<CafeteriaPartOne> logger)
    {
        _logger = logger;
        Name = "--- Day 5: Cafeteria Part One---";
    }

    public int Day => 5;
    public int Part => 1;
    public string Name { get; set; }
    public string Test => "Data/Day05/tests.txt";
    public string Input => "Data/Day05/input.txt";

    public int Index => 9;

    private readonly ILogger<CafeteriaPartOne> _logger;

    public async Task<object> InvokeAsync(bool runTest = false)
    {
        var ranges = new List<(decimal min, decimal max)>();
        var ingredients = new List<decimal>();
        var insertIngredient = false;
        await foreach (var line in File.ReadLinesAsync(Path.Combine(Directory.GetCurrentDirectory(), runTest ? Test : Input)))
        {
            if (string.IsNullOrEmpty(line))
            {
                insertIngredient = true;
                continue;
            }

            if (!insertIngredient)
            {
                var split = line.Split("-");
                ranges.Add(new ValueTuple<decimal, decimal>(decimal.Parse(split[0]), decimal.Parse(split[1])));
                continue;
            }

            ingredients.Add(decimal.Parse(line));
        }

        var count = 0;
        foreach (var ingredient in ingredients)
        {
            foreach (var (min, max) in ranges)
            {
                if (ingredient < min || ingredient > max) continue;
                count++;
                break;
            }
        }

        return count;
    }
}
