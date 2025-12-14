using AoC2025.Solutions.Abstractions;
using Microsoft.Extensions.Logging;

namespace AoC2025.Solutions.Day02;


public class DayTwoSolutionPartOne : ISolution
{
    public DayTwoSolutionPartOne(ILogger<DayTwoSolutionPartOne> logger)
    {
        _logger = logger;
        Name = "--- Day 2: Gift Shop ---";
    }

    public string Name { get; set; }

    public int Index => 3;
    
    private readonly ILogger<DayTwoSolutionPartOne> _logger;

    public async Task<int> InvokeAsync()
    {
        var fileContent = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "tests.txt"));
        var ranges = fileContent.Split(",");
        return 0;
    }
}
