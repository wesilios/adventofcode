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
        var tachyonDiagram = new List<List<string>>();
        await foreach (var line in File.ReadLinesAsync(Path.Combine(Directory.GetCurrentDirectory(),
                           runTest ? Test : Input)))
        {
            tachyonDiagram.Add(line.Select(c => c.ToString()).ToList());
        }

        var rowIndex = 0;
        while (rowIndex < tachyonDiagram[^1].Count - 1)
        {
            for (var columnIndex = 0; columnIndex < tachyonDiagram[rowIndex].Count; columnIndex++)
            {
                if ((rowIndex == 0 && tachyonDiagram[rowIndex][columnIndex] == Start) ||
                        tachyonDiagram[rowIndex][columnIndex] == TravelPath)
                {
                    if (tachyonDiagram[rowIndex + 1][columnIndex] != Splitter)
                    {
                        tachyonDiagram[rowIndex + 1][columnIndex] = TravelPath;
                        continue;
                    }

                    if (columnIndex - 1 > 0 && tachyonDiagram[rowIndex + 1][columnIndex - 1] != Splitter)
                    {
                        tachyonDiagram[rowIndex + 1][columnIndex - 1] = TravelPath;
                    }

                    if (columnIndex + 1 < tachyonDiagram[rowIndex + 1].Count && tachyonDiagram[rowIndex + 1][columnIndex + 1] != Splitter)
                    {
                        tachyonDiagram[rowIndex + 1][columnIndex + 1] = TravelPath;
                    }

                    result++;
                }

            }
            rowIndex++;
            _logger.LogDebug(string.Join("", tachyonDiagram[rowIndex]));
        }
        return result;
    }
}
