using AoC2025.Solutions.Abstractions;
using Microsoft.Extensions.Logging;

namespace AoC2025.Solutions.Day03;

public class LobbyPartOne : ISolution
{
    public LobbyPartOne(ILogger<LobbyPartOne> logger)
    {
        _logger = logger;
        Name = "--- Day 3: Lobby ---";
    }

    public int Day => 3;
    public int Part => 1;
    public string Name { get; set; }
    public string Test => "Data/Day03/tests.txt";
    public string Input => "Data/Day03/input.txt";

    public int Index => 5;

    private readonly ILogger<LobbyPartOne> _logger;

    public async Task<object> InvokeAsync(bool runTest = false)
    {
        var totalOutput = 0;
        await foreach (var bank in File.ReadLinesAsync(Path.Combine(Directory.GetCurrentDirectory(),
                           runTest ? Test : Input)))
        {
            var bankLength = bank.Length;
            var largestNumber = 0;
            for (var i = 0; i < bankLength - 1; i++)
            {
                for (var j = i + 1; j < bankLength; j++)
                {
                    var current = int.Parse($"{bank[i]}{bank[j]}");
                    if (largestNumber < current)
                    {
                        largestNumber = current;
                    }
                }
            }

            totalOutput += largestNumber;
        }

        return totalOutput;
    }
}