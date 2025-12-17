using AoC2025.Solutions.Abstractions;
using Microsoft.Extensions.Logging;

namespace AoC2025.Solutions.Day06;

public class TrashCompactorPartOne : ISolution
{
    public TrashCompactorPartOne(ILogger<TrashCompactorPartOne> logger)
    {
        _logger = logger;
        Name = "--- Day 6: Trash Compactor ---";
    }

    public int Day => 6;
    public int Part => 1;
    public string Name { get; set; }
    public string Description => "Solve cephalopod math worksheet";
    public string Test => "Data/Day06/tests.txt";
    public string Input => "Data/Day06/input.txt";

    public int Index => 10;

    private readonly ILogger<TrashCompactorPartOne> _logger;

    public async Task<object> InvokeAsync(bool runTest = false)
    {
        return 0;
    }
}