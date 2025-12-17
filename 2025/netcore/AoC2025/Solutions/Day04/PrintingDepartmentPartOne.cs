using AoC2025.Solutions.Abstractions;
using Microsoft.Extensions.Logging;

namespace AoC2025.Solutions.Day04;

public class PrintingDepartmentPartOne : ISolution
{
    public PrintingDepartmentPartOne(ILogger<PrintingDepartmentPartOne> logger)
    {
        _logger = logger;
        Name = "--- Day 4: Printing Department ---";
    }

    public int Day => 4;
    public int Part => 1;
    public string Name { get; set; }
    public string Test => "Data/Day04/tests.txt";
    public string Input => "Data/Day04/input.txt";

    public int Index => 7;
    private const char RollOfPaper = '@';
    private const int MaximumRollPaper = 4;

    private readonly ILogger<PrintingDepartmentPartOne> _logger;

    public async Task<object> InvokeAsync(bool runTest = false)
    {
        var result = 0;

        var grids = new List<string>();
        await foreach (var line in File.ReadLinesAsync(Path.Combine(Directory.GetCurrentDirectory(),
                           runTest ? Test : Input)))
        {
            grids.Add(line);
        }

        for (var y = 0; y < grids.Count; y++)
        {
            for (var x = 0; x < grids[y].Length; x++)
            {
                var count = 0;
                //(y,x)
                if (grids[y][x] != RollOfPaper) continue;

                // (y-1,x)
                if (y > 0 && grids[y - 1][x] == RollOfPaper) count++;

                // (y+1,x)
                if (y + 1 < grids.Count && grids[y + 1][x] == RollOfPaper) count++;

                if (x + 1 < grids[y].Length)
                {
                    //(y,x+1)
                    if (grids[y][x + 1] == RollOfPaper) count++;

                    //(y-1,x+1)
                    if (y > 0 && grids[y - 1][x + 1] == RollOfPaper) count++;

                    //(y+1,x+1)
                    if (y + 1 < grids.Count && grids[y + 1][x + 1] == RollOfPaper) count++;
                }

                if (x > 0)
                {
                    //(y,x-1)
                    if (grids[y][x - 1] == RollOfPaper) count++;

                    //(y-1,x-1)
                    if (y > 0 && grids[y - 1][x - 1] == RollOfPaper) count++;

                    //(y+1,x-1)
                    if (y + 1 < grids.Count && grids[y + 1][x - 1] == RollOfPaper) count++;
                }

                if (count < MaximumRollPaper) result++;
            }
        }

        return result;
    }
}