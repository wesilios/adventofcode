using AoC2025.Solutions.Abstractions;
using Microsoft.Extensions.Logging;

namespace AoC2025.Solutions.Day06;

public class TrashCompactorPartTwo : ISolution
{
    public TrashCompactorPartTwo(ILogger<TrashCompactorPartTwo> logger)
    {
        _logger = logger;
        Name = "--- Day 6: Trash Compactor Part Two---";
    }

    public int Day => 6;
    public int Part => 2;
    public string Name { get; set; }
    public string Test => "Data/Day06/tests.txt";
    public string Input => "Data/Day06/input.txt";

    public int Index => 12;
    private const string Added = "+";
    private const string Multiplied = "*";

    private readonly ILogger<TrashCompactorPartTwo> _logger;

    public async Task<object> InvokeAsync(bool runTest = false)
    {
        var worksheet = new List<string>();
        await foreach (var line in File.ReadLinesAsync(Path.Combine(Directory.GetCurrentDirectory(),
                           runTest ? Test : Input)))
        {
            //var rowItems = line.Split(' ').ToList();
            worksheet.Add(line);
        }

        var grandTotal = decimal.Zero;

        return grandTotal;
    }
}
