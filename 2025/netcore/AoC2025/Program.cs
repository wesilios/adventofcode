using AoC2025.Solutions.Abstractions;
using AoC2025.Solutions.Day01;
using AoC2025.Solutions.Day02;
using AoC2025.Solutions.Day03;
using AoC2025.Solutions.Day04;
using AoC2025.Solutions.Day05;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices(services =>
{
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
});

var app = builder.Build();

var solutionsByDay = app.Services
    .GetRequiredService<IEnumerable<ISolution>>()
    .GroupBy(s => s.Day)
    .OrderBy(s => s.Key)
    .ToDictionary(s => s.Key, s => s.ToList());

AnsiConsole.MarkupLine("[bold green]🎄 Advent of Code 2025 🎄[/]");
AnsiConsole.WriteLine();

while (true)
{
    var daysSelection = solutionsByDay.Select(x => x.Key).ToList();

    daysSelection.Add(0);

    var selectedDay = AnsiConsole.Prompt(
        new SelectionPrompt<int>()
            .Title("Select a [yellow]solution[/]:")
            .PageSize(10)
            .UseConverter(s => s == 0 ? "Exit" : $"{s}. Day {s}")
            .AddChoices(daysSelection)
    );

    if (selectedDay == 0) break;

    var solutions = solutionsByDay[selectedDay];

    var selectedSolution = AnsiConsole.Prompt(
            new SelectionPrompt<ISolution>()
                .Title($"Day {selectedDay:00} – select [yellow]part[/]:")
                .UseConverter(s => $"Part {s.Part}: {s.Name}")
                .AddChoices(solutions)
        );

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
            AnsiConsole.MarkupLine("[bold green]Result:[/]");
            AnsiConsole.WriteLine(result.ToString());
        });

    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine("[grey]Press any key to exit...[/]");
    Console.ReadKey();
    AnsiConsole.Clear();
}


AnsiConsole.MarkupLine("[bold red]🎄 Exitting Advent of Code 2025! Enjoy the holiday 🎄[/]");
