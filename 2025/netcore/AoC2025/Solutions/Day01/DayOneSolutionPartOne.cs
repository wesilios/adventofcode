using AoC2025.Solutions.Abstractions;
using Microsoft.Extensions.Logging;

namespace AoC2025.Solutions.Day01;

public class DayOneSolutionPartOne : ISolution
{
    public DayOneSolutionPartOne(ILogger<DayOneSolutionPartOne> logger)
    {
        _logger = logger;
        Name = "--- Day 1: Secret Entrance ---";
    }

    private const int MaxNumber = 100;
    private const int MinNumber = 0;
    private const int StartingPoint = 50;
    private const string LeftRotation = "L";
    private const string RightRotation = "R";

    private readonly ILogger<DayOneSolutionPartOne> _logger;

    public string Name { get; set; }

    public int Index => 1;

    public async Task<int> InvokeAsync()
    {
        var position = StartingPoint;
        var count = 0;

        _logger.LogInformation("The dial starts by pointing at {Position}", position);

        await foreach (var line in File.ReadLinesAsync(Path.Combine(Directory.GetCurrentDirectory(), "input.txt")))
        {

            var moves = line.StartsWith(LeftRotation)
                ? -int.Parse(line[LeftRotation.Length..])
                : int.Parse(line[RightRotation.Length..]);

            var newPosition = position + moves;

            position = newPosition;
            position %= MaxNumber;

            if (position < MinNumber)
            {
                position = MaxNumber + position;
            }

            if (position == MinNumber) count++;

            _logger.LogInformation("The dial is rotated {Line} to point at {Position}", line, position);
        }

        return count;
    }
}
