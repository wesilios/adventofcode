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
        var maxLength = 0;
        await foreach (var line in File.ReadLinesAsync(Path.Combine(Directory.GetCurrentDirectory(),
                           runTest ? Test : Input)))
        {
            worksheet.Add(line);
            maxLength = maxLength <= line.Length ? line.Length : maxLength;
        }

        worksheet[^1] += new string(' ', maxLength - worksheet[^1].Length);
        var grandTotal = decimal.Zero;

        var total = new List<decimal>();
        var operation = "";
        var x = maxLength - 1;
        while (x >= 0)
        {
            var totalItem = string.Empty;
            foreach (var row in worksheet)
            {
                if (row[x].ToString() == Added || row[x].ToString() == Multiplied)
                {
                    operation = row[x].ToString();
                    break;
                }

                totalItem += row[x];
            }

            total.Add(decimal.Parse(totalItem.Trim()));
            x--;

            if (string.IsNullOrEmpty(operation)) continue;

            switch (operation)
            {
                case Multiplied:
                    grandTotal = total.Aggregate(decimal.One, (current, item) => current * item);
                    break;

                case Added:
                    grandTotal += total.Sum();
                    break;
            }

            total.Clear();
            operation = string.Empty;
            x--;
        }

        return grandTotal;
    }
}
