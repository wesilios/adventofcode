using AoC2025.Solutions.Abstractions;
using Microsoft.Extensions.Logging;

namespace AoC2025.Solutions.Day02;

public class GiftShopPartTwo : ISolution
{
    public GiftShopPartTwo(ILogger<GiftShopPartTwo> logger)
    {
        _logger = logger;
        Name = "--- Day 2: Gift Shop Part Two ---";
    }

    public int Day => 2;
    public int Part => 2;
    public string Name { get; set; }
    public string Test => "Data/Day02/tests.txt";
    public string Input => "Data/Day02/input.txt";

    public int Index => 4;

    private readonly ILogger<GiftShopPartTwo> _logger;

    public async Task<object> InvokeAsync(bool runTest = false)
    {
        var fileContent =
            await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), runTest ? Test : Input));
        var ranges = fileContent.Split(",");
        var result = decimal.Zero;
        foreach (var range in ranges)
        {
            var start = range.Split("-")[0];
            var end = range.Split("-")[1];
            for (var i = decimal.Parse(start); i <= decimal.Parse(end); i++)
            {
                // calculate the total characters in number
                var stringNumber = i.ToString();

                var haftLength = stringNumber.Length / 2;
                var startIndex = 0;
                while (startIndex < haftLength)
                {
                    if (stringNumber.Split(stringNumber[..(haftLength - startIndex)]).GroupBy(x => x).Count() == 1)
                    {
                        result += i;
                        break;
                    }

                    startIndex++;
                }
            }
        }

        return result;
    }
}