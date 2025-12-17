using AoC2025.Solutions.Abstractions;
using Microsoft.Extensions.Logging;

namespace AoC2025.Solutions.Day04;

public class PrintingDepartmentPartTwo : ISolution
{
    public PrintingDepartmentPartTwo(ILogger<PrintingDepartmentPartTwo> logger)
    {
        _logger = logger;
        Name = "--- Day 4: Printing Department Part Two---";
    }

    public int Day => 4;
    public int Part => 2;
    public string Name { get; set; }
    public string Test => "Data/Day04/tests.txt";
    public string Input => "Data/Day04/input.txt";

    public int Index => 8;
    private const char RollOfPaper = '@';
    private const char X = 'X';
    private const int MaximumRollPaper = 4;

    private readonly ILogger<PrintingDepartmentPartTwo> _logger;

    public async Task<object> InvokeAsync(bool runTest = false)
    {
        var result = 0;

        var currentGrids = new Dictionary<int, List<char>>();
        var newGrids = new Dictionary<int, List<char>>();

        var countLine = 0;
        await foreach (var line in File.ReadLinesAsync(Path.Combine(Directory.GetCurrentDirectory(),
                           runTest ? Test : Input)))
        {
            currentGrids.Add(countLine, new List<char>());
            newGrids.Add(countLine, new List<char>());
            foreach (var character in line)
            {
                currentGrids[countLine].Add(character);
                newGrids[countLine].Add(character);
            }

            countLine++;
        }

        while (true)
        {
            var countRun = 0;
            for (var y = 0; y < currentGrids.Count; y++)
            {
                for (var x = 0; x < currentGrids[y].Count; x++)
                {
                    var count = 0;

                    //(y,x)
                    if (currentGrids[y][x] != RollOfPaper) continue;

                    // (y-1,x)
                    if (y > 0 && currentGrids[y - 1][x] == RollOfPaper) count++;

                    // (y+1,x)
                    if (y + 1 < currentGrids.Count && currentGrids[y + 1][x] == RollOfPaper) count++;

                    if (x + 1 < currentGrids[y].Count)
                    {
                        //(y,x+1)
                        if (currentGrids[y][x + 1] == RollOfPaper) count++;

                        //(y-1,x+1)
                        if (y > 0 && currentGrids[y - 1][x + 1] == RollOfPaper) count++;

                        //(y+1,x+1)
                        if (y + 1 < currentGrids.Count && currentGrids[y + 1][x + 1] == RollOfPaper) count++;
                    }

                    if (x > 0)
                    {
                        //(y,x-1)
                        if (currentGrids[y][x - 1] == RollOfPaper) count++;

                        //(y-1,x-1)
                        if (y > 0 && currentGrids[y - 1][x - 1] == RollOfPaper) count++;

                        //(y+1,x-1)
                        if (y + 1 < currentGrids.Count && currentGrids[y + 1][x - 1] == RollOfPaper) count++;
                    }

                    if (count < MaximumRollPaper)
                    {
                        countRun++;
                        newGrids[y][x] = X;
                    }
                }
                _logger.LogDebug("Current Row {Y}: {Value}", y, string.Join("", currentGrids[y]));
                _logger.LogDebug("New Row {Y}: {Value}", y, string.Join("", newGrids[y]));
            }

            currentGrids = newGrids.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToList());
            if (countRun == 0) break;
            result += countRun;
        }

        return result;
    }
}
