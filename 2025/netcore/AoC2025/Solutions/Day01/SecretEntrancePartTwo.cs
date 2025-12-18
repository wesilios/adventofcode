using AoC2025.Solutions.Abstractions;
using Microsoft.Extensions.Logging;

namespace AoC2025.Solutions.Day01;

public class SecretEntrancePartTwo : ISolution
{
    private const int MaxNumber = 100;
    private const int MinNumber = 0;
    private const int StartingPoint = 50;
    private const string LeftRotation = "L";
    private const string RightRotation = "R";

    private readonly ILogger<SecretEntrancePartOne> _logger;

    public int Day => 1;
    public int Part => 2;
    public string Name { get; set; }
    public string Test => "Data/Day01/tests.txt";
    public string Input => "Data/Day01/input.txt";

    public SecretEntrancePartTwo(ILogger<SecretEntrancePartOne> logger)
    {
        _logger = logger;
        Name = "--- Day 1: Secret Entrance Part Two ---";
    }

    public int Index => 1;

    public async Task<object> InvokeAsync(bool runTest = false)
    {
        var position = StartingPoint;
        var count = 0;

        _logger.LogDebug("The dial starts by pointing at {Position}", position);

        await foreach (var line in File.ReadLinesAsync(Path.Combine(Directory.GetCurrentDirectory(),
                           runTest ? Test : Input)))
        {
            var moves = line.StartsWith(LeftRotation)
                ? -int.Parse(line[LeftRotation.Length..])
                : int.Parse(line[RightRotation.Length..]);

            var newPosition = position + moves;

            var passes = (int)Math.Floor((double)newPosition / MaxNumber);

            if (newPosition < 0)
            {
                passes = position == 0 ? -(newPosition / MaxNumber) : 1 + -(newPosition / MaxNumber);
            }
            else if (newPosition > 0)
            {
                passes = newPosition / MaxNumber;
            }
            else
            {
                count++;
            }

            position = newPosition % MaxNumber;

            if (position < MinNumber)
            {
                position += MaxNumber;
            }

            count += passes;

            if (passes > 0)
            {
                _logger.LogDebug(
                    "The dial is rotated {Line} to point at {Position}; during this rotation, it points at {MinNumber} {Passes} times.",
                    line, position, MinNumber, passes);
            }
            else
            {
                _logger.LogDebug("The dial is rotated {Line} to point at {Position}", line, position);
            }
        }

        return count;
    }
}
