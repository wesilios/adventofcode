using AoC2025.Solutions.Abstractions;
using Microsoft.Extensions.Logging;

namespace AoC2025.Solutions.Day07;

public class LaboratoriesPartTwo : ISolution
{
    public LaboratoriesPartTwo(ILogger<LaboratoriesPartTwo> logger)
    {
        _logger = logger;
        Name = "--- Day 7: Laboratories Part Two---";
    }

    public int Day => 7;
    public string Name { get; set; }
    public int Part => 2;
    public string Test => "Data/Day07/tests.txt";
    public string Input => "Data/Day07/input.txt";

    public int Index => 14;
    private const string Start = "S";
    private const string Splitter = "^";
    private const string TravelPath = "|";

    private readonly ILogger<LaboratoriesPartTwo> _logger;

    public async Task<object> InvokeAsync(bool runTest = false)
    {
        var result = 0;

        return result;
    }
}
