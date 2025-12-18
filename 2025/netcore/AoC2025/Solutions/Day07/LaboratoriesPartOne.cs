using Microsoft.Extensions.Logging;
using AoC2025.Solutions.Abstractions;

namespace AoC2025.Solutions.Day07;

public class LaboratoriesPartOne : ISolution
{
    public LaboratoriesPartOne(ILogger<LaboratoriesPartOne> logger)
    {
        _logger = logger;
        Name = "--- Day 7: Laboratories ---";
    }

    public int Day => 7;
    public int Part => 1;
    public string Name { get; set; }
    public string Test => "Data/Day07/tests.txt";
    public string Input => "Data/Day07/input.txt";

    public int Index => 13;
    private const string Splitter = "^";
    private const string Start = "S";
    private const string TravelPath = "|";

    private readonly ILogger<LaboratoriesPartOne> _logger;

    public async Task<object> InvokeAsync(bool runTest = false)
    {
        var result = 0;
        var tachyonDiagram = new List<string>();
        await foreach (var line in File.ReadLinesAsync(Path.Combine(Directory.GetCurrentDirectory(),
                           runTest ? Test : Input)))
        {
            tachyonDiagram.Add(line);
        }

        return result;
    }
}
