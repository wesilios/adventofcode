using AoC2025.Solutions.Abstractions;
using AoC2025.Solutions.Day01;
using AoC2025.Solutions.Day02;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices(services =>
{
    services.AddTransient<ISolution, DayOneSolutionPartOne>();
    services.AddTransient<ISolution, DayOneSolutionPartTwo>();
    services.AddTransient<ISolution, DayTwoSolutionPartOne>();
});

var app = builder.Build();

var solutions = app.Services.GetRequiredService<IEnumerable<ISolution>>();

foreach (var solution in solutions)
{
    Console.WriteLine(solution.Name);
}

// var password = await solutionOne.SolvePartOneAsync();
//
// Console.WriteLine($"Password to open the door: {password}");

// var password = await solutionOne.SolvePartTwoAsync();

// Console.WriteLine($"Using password method 0x434C49434B, Password to open the door: {password}");

// var solutionTwo = app.Services.GetRequiredService<IDayTwoSolution>();

var test = await solutions.First(x => x.Index.Equals(3)).InvokeAsync();
