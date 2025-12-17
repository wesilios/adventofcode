using System.Text;
using AoC2025.Solutions.Abstractions;
using Microsoft.Extensions.Logging;

namespace AoC2025.Solutions.Day03;

public class LobbyPartTwo : ISolution
{
    public LobbyPartTwo(ILogger<LobbyPartTwo> logger)
    {
        _logger = logger;
        Name = "--- Day 3: Lobby Part Two ---";
    }

    public int Day => 3;
    public int Part => 2;
    public string Name { get; set; }
    public string Test => "Data/Day03/tests.txt";
    public string Input => "Data/Day03/input.txt";

    public int Index => 5;

    private readonly ILogger<LobbyPartTwo> _logger;

    public async Task<object> InvokeAsync(bool runTest = false)
    {
        const int maximumDigit = 12;
        const int maximumValue = 9;
        var totalOutput = decimal.Zero;

        await foreach (var bank in File.ReadLinesAsync(Path.Combine(Directory.GetCurrentDirectory(),
                           runTest ? Test : Input)))
        {
            var largestNumber = new StringBuilder();
            var minIndex = 0;
            for (var i = 0; i < maximumDigit; i++)
            {
                var highestNumber = 0;
                for (var j = minIndex; j <= bank.Length - maximumDigit + i; j++)
                {
                    var value = bank[j] - '0';
                    if (value > highestNumber)
                    {
                        highestNumber = value;
                        minIndex = j + 1;
                    }

                    if (value == maximumValue) break;
                }

                largestNumber.Append(highestNumber);
            }

            totalOutput += decimal.Parse(largestNumber.ToString());
        }

        return totalOutput;
    }
}