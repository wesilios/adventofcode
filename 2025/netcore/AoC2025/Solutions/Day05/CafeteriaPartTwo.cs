using AoC2025.Solutions.Abstractions;
using Microsoft.Extensions.Logging;

namespace AoC2025.Solutions.Day05;

public class CafeteriaPartTwo : ISolution
{
    public CafeteriaPartTwo(ILogger<CafeteriaPartTwo> logger)
    {
        _logger = logger;
        Name = "--- Day 5: Cafeteria Part Two---";
    }

    public int Day => 5;
    public int Part => 2;
    public string Name { get; set; }
    public string Test => "Data/Day05/tests.txt";
    public string Input => "Data/Day05/input.txt";

    public int Index => 10;

    private readonly ILogger<CafeteriaPartTwo> _logger;

    public async Task<object> InvokeAsync(bool runTest = false)
    {
        var ranges = new List<Range>();
        await foreach (var line in File.ReadLinesAsync(Path.Combine(Directory.GetCurrentDirectory(),
                           runTest ? Test : Input)))
        {
            if (string.IsNullOrEmpty(line)) break;

            var split = line.Split("-");
            ranges.Add(new() { Min = decimal.Parse(split[0]), Max = decimal.Parse(split[1]) });
        }

        ranges = ranges.OrderBy(x => x.Min).ToList();
        var result = decimal.Zero;

        var index = 0;
        var minIndex = 0;
        while (true)
        {
            if (index == ranges.Count)
            {
                result += ranges[minIndex].Max - ranges[minIndex].Min + 1;
                break;
            }

            if (ranges[minIndex].Max >= ranges[index].Min)
            {
                ranges[minIndex].Max =
                    ranges[minIndex].Max <= ranges[index].Max ? ranges[index].Max : ranges[minIndex].Max;
                index++;
                continue;
            }

            result += ranges[minIndex].Max - ranges[minIndex].Min + 1;
            if (minIndex != index)
            {
                minIndex = index;
                continue;
            }

            minIndex++;
            index++;
        }

        return result;
    }

    private class Range
    {
        public decimal Min { get; set; }
        public decimal Max { get; set; }
    }
}
