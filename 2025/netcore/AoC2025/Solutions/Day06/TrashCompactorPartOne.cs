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
    public string Test => "Data/Day06/tests.txt";
    public string Input => "Data/Day06/input.txt";

    public int Index => 10;

    private const string Added = "+";
    private const string Multiplied = "*";

    private readonly ILogger<TrashCompactorPartOne> _logger;

    public async Task<object> InvokeAsync(bool runTest = false)
    {
        var result = decimal.Zero;
        var worksheet = new List<List<string>>();
        await foreach (var line in File.ReadLinesAsync(Path.Combine(Directory.GetCurrentDirectory(),
                           runTest ? Test : Input)))
        {
            var rowItems = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            worksheet.Add(rowItems);
        }

        return result;
    }
}
