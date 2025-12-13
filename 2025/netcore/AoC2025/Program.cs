using AoC2025.Day01;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices(services =>
{
	services.AddTransient<IDayOneSolution, DayOneSolution>();
});

var app = builder.Build();

var solutionOne = app.Services.GetRequiredService<IDayOneSolution>();

// var password = await solutionOne.SolvePartOneAsync();
//
// Console.WriteLine($"Password to open the door: {password}");

var password = await solutionOne.SolvePartTwoAsync();

Console.WriteLine($"Using password method 0x434C49434B, Password to open the door: {password}");
