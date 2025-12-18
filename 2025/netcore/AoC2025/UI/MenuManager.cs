using AoC2025.Solutions.Abstractions;
using Spectre.Console;

namespace AoC2025.UI;

public class MenuManager
{
    private const int TotalDays = 12;

    private readonly Dictionary<int, List<ISolution>> _solutionsByDay;

    public MenuManager(IEnumerable<ISolution> solutions)
    {
        _solutionsByDay = solutions
            .GroupBy(s => s.Day)
            .OrderBy(s => s.Key)
            .ToDictionary(s => s.Key, s => s.ToList());
    }

    public IEnumerable<ISolution> GetSolutions(int day)
    {
        return _solutionsByDay[day];
    }

    public void ShowHeader()
    {
        AnsiConsole.Clear();

        var figlet = new FigletText("AoC 2025")
            .Centered()
            .Color(Color.Green);
        AnsiConsole.Write(figlet);
        AnsiConsole.WriteLine();
    }

    public void ShowProgressTable()
    {
        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Green)
            .Title("[bold yellow]🎄 Advent of Code 2025 Progress 🎄[/]");

        table.AddColumn(new TableColumn("[bold]Day[/]").Centered());
        table.AddColumn(new TableColumn("[bold]Part 1[/]").Centered());
        table.AddColumn(new TableColumn("[bold]Part 2[/]").Centered());
        table.AddColumn(new TableColumn("[bold]Stars[/]").Centered());
        table.AddColumn(new TableColumn("[bold]Title[/]"));

        for (int day = 1; day <= TotalDays; day++)
        {
            if (_solutionsByDay.TryGetValue(day, out var solutions))
            {
                var part1 = solutions.FirstOrDefault(s => s.Part == 1);
                var part2 = solutions.FirstOrDefault(s => s.Part == 2);

                var part1Status = part1 != null ? "[green]✓[/]" : "[grey]○[/]";
                var part2Status = part2 != null ? "[green]✓[/]" : "[grey]○[/]";
                var stars = (part1 != null ? 1 : 0) + (part2 != null ? 1 : 0);
                var starDisplay = stars > 0 ? $"[yellow]{new string('⭐', stars)}[/]" : "[grey]○○[/]";
                var title = part1?.Name ?? part2?.Name ?? "[grey]Not Started[/]";

                table.AddRow(
                    $"[cyan]{day:D2}[/]",
                    part1Status,
                    part2Status,
                    starDisplay,
                    title
                );
            }
            else
            {
                table.AddRow(
                    $"[grey]{day:D2}[/]",
                    "[grey]○[/]",
                    "[grey]○[/]",
                    "[grey]○○[/]",
                    "[grey]Not Started[/]"
                );
            }
        }

        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();
    }

    public string ShowMainMenu()
    {
        var menuOptions = new List<string> { "📊 View Progress Table" };
        menuOptions.AddRange(_solutionsByDay.Keys.Select(d => $"🎄 Day {d:D2}"));
        menuOptions.Add("❌ Exit");

        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]What would you like to do?[/]")
                .PageSize(15)
                .AddChoices(menuOptions)
        );
    }

    public string ShowDayMenu(int day, IEnumerable<ISolution> solutions)
    {
        AnsiConsole.Clear();

        var actions = new List<string> { "📖 View Puzzle Description" };
        actions.AddRange(solutions.Select(s => $"▶️  Run Part {s.Part}: {s.Name}"));
        actions.Add("⬅️  Back to Menu");

        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"[yellow]Day {day:D2} - What would you like to do?[/]")
                .PageSize(10)
                .AddChoices(actions)
        );
    }

    public void ShowPuzzleDescription(ISolution solution)
    {
        var readmePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "Solutions",
            $"Day{solution.Day:D2}",
            "README.md"
        );

        if (File.Exists(readmePath))
        {
            var content = File.ReadAllText(readmePath);

            // Limit content length for display
            var displayContent = content.Length > 2000
                ? content.Substring(0, 2000) + "\n\n[grey]... (truncated)[/]"
                : content;

            var panel = new Panel(new Markup(displayContent.EscapeMarkup()))
            {
                Header = new PanelHeader($"[bold yellow]Day {solution.Day:D2} - {solution.Name}[/]"),
                Border = BoxBorder.Rounded,
                BorderStyle = new Style(Color.Yellow),
                Padding = new Padding(2, 1)
            };

            AnsiConsole.Write(panel);
        }
        else
        {
            AnsiConsole.MarkupLine("[yellow]No description available for this puzzle.[/]");
        }
    }

    public void ShowResultPanel(object result)
    {
        var resultPanel = new Panel(
            new Markup($"[bold green]{result}[/]")
        )
        {
            Header = new PanelHeader("[bold yellow]✨ Solution Result ✨[/]"),
            Border = BoxBorder.Double,
            BorderStyle = new Style(Color.Green),
            Padding = new Padding(2, 1)
        };

        AnsiConsole.Write(resultPanel);
    }
}
