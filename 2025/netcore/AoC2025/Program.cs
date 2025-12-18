using AoC2025.Solutions.Abstractions;
using AoC2025.Solutions.Day01;
using AoC2025.Solutions.Day02;
using AoC2025.Solutions.Day03;
using AoC2025.Solutions.Day04;
using AoC2025.Solutions.Day05;
using AoC2025.Solutions.Day06;
using AoC2025.Solutions.Day07;
using AoC2025.UI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices(services =>
{
    services.AddTransient<MenuManager>();
    services.AddTransient<ISolution, SecretEntrancePartOne>();
    services.AddTransient<ISolution, SecretEntrancePartTwo>();
    services.AddTransient<ISolution, GiftShopPartOne>();
    services.AddTransient<ISolution, GiftShopPartTwo>();
    services.AddTransient<ISolution, LobbyPartOne>();
    services.AddTransient<ISolution, LobbyPartTwo>();
    services.AddTransient<ISolution, PrintingDepartmentPartOne>();
    services.AddTransient<ISolution, PrintingDepartmentPartTwo>();
    services.AddTransient<ISolution, CafeteriaPartOne>();
    services.AddTransient<ISolution, CafeteriaPartTwo>();
    services.AddTransient<ISolution, TrashCompactorPartOne>();
    services.AddTransient<ISolution, TrashCompactorPartTwo>();
    services.AddTransient<ISolution, LaboratoriesPartOne>();
});

var app = builder.Build();

var menu = app.Services.GetRequiredService<MenuManager>();

while (true)
{
    // Show header and progress table
    menu.ShowHeader();
    menu.ShowProgressTable();

    // Show main menu
    var selection = menu.ShowMainMenu();

    if (selection == "❌ Exit") break;
    if (selection == "📊 View Progress Table") continue;

    // Extract day number
    var selectedDay = int.Parse(selection.Split(' ')[2]);
    var solutions = menu.GetSolutions(selectedDay);

    // Show day menu
    var action = menu.ShowDayMenu(selectedDay, solutions);

    if (action == "⬅️  Back to Menu") continue;

    if (action == "📖 View Puzzle Description")
    {
        AnsiConsole.Clear();
        menu.ShowPuzzleDescription(solutions.First());
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[grey]Press any key to continue...[/]");
        Console.ReadKey();
        continue;
    }

    // Extract part number and find solution
    var partNumber = int.Parse(action.Split("Part ")[1].Split(':')[0]);
    var selectedSolution = solutions.First(s => s.Part == partNumber);

    // ---------- Input Mode ----------
    var useTestInput = AnsiConsole.Confirm(
        "Use [yellow]test input[/]?",
        defaultValue: false
    );

    AnsiConsole.Clear();

    AnsiConsole.MarkupLine(
        $"[bold cyan]Running Day {selectedSolution.Day:00} " +
        $"Part {selectedSolution.Part}[/]"
    );

    AnsiConsole.MarkupLine(
        $"Input: [yellow]{(useTestInput ? "TEST" : "ACTUAL")}[/]\n"
    );

    AnsiConsole.WriteLine();

    await AnsiConsole.Status()
        .Spinner(Spinner.Known.Dots)
        .SpinnerStyle(Style.Parse("green"))
        .StartAsync("Solving puzzle...", async _ =>
        {
            var result = await selectedSolution.InvokeAsync(useTestInput);

            AnsiConsole.WriteLine();
            menu.ShowResultPanel(result);
        });

    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine("[grey]Press any key to continue...[/]");
    Console.ReadKey();
}


AnsiConsole.MarkupLine("[bold red]🎄 Exiting Advent of Code 2025! Enjoy the holiday 🎄[/]");
