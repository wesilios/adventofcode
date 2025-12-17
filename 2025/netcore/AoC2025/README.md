# 🎄 Advent of Code 2025 – .NET Core Solutions

This directory contains C# solutions for Advent of Code 2025 using .NET 8 with a modern, interactive terminal UI.

## 🏗️ Architecture

### Core Components

#### `ISolution` Interface

The foundation of the solution architecture, defining a contract for all puzzle solutions:

```csharp
public interface ISolution
{
    int Day { get; }           // Day number (1-25)
    int Part { get; }          // Part number (1-2)
    string Name { get; set; }  // Solution name/description
    string Test { get; }       // Path to test input
    string Input { get; }      // Path to actual input
    int Index { get; }         // Solution index
    Task<object> InvokeAsync(bool runTest = false);
}
```

#### Dependency Injection

Solutions are registered as transient services in `Program.cs`:

- Enables clean separation of concerns
- Facilitates testing and extensibility
- Automatic discovery and grouping by day

#### Terminal UI (Spectre.Console)

Interactive console application with:

- **Day Selection**: Choose which day to run
- **Part Selection**: Select Part 1 or Part 2
- **Input Toggle**: Switch between test and actual inputs
- **Spinner Animation**: Visual feedback during execution
- **Formatted Output**: Color-coded results

### Project Structure

```
AoC2025/
├── Program.cs                    # Entry point, DI setup, UI logic
├── AoC2025.csproj               # Project configuration
├── Solutions/
│   ├── Abstractions/
│   │   └── ISolution.cs         # Solution interface
│   ├── Day01/
│   │   ├── README.md            # Day 1 problem description
│   │   ├── SecretEntrancePartOne.cs
│   │   └── SecretEntrancePartTwo.cs
│   ├── Day02/
│   │   ├── README.md
│   │   ├── GiftShopPartOne.cs
│   │   └── GiftShopPartTwo.cs
│   └── ...
└── Data/
    ├── Day01/
    │   ├── input.txt            # Actual puzzle input
    │   └── test.txt             # Test input
    └── ...
```

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Running the Application

```bash
# Navigate to the project directory
cd 2025/netcore/AoC2025

# Run the application
dotnet run

# Or build and run separately
dotnet build
dotnet run --no-build
```

### Using the Interactive Menu

1. **Select Day**: Choose from available days (1-6 currently implemented)
2. **Select Part**: Choose Part 1 or Part 2
3. **Choose Input**: Decide between test input (for validation) or actual input
4. **View Results**: See the solution output with execution feedback

## 📦 Dependencies

- **Microsoft.Extensions.Hosting** (8.0.1): Dependency injection and hosting
- **Microsoft.Extensions.Hosting.Abstractions** (8.0.1): Hosting abstractions
- **Spectre.Console** (0.54.0): Rich terminal UI components

## 🎯 Design Patterns

### Strategy Pattern

Each solution implements `ISolution`, allowing runtime selection and execution.

### Dependency Injection

Solutions are registered and resolved through the DI container.

### Async/Await

All solutions use asynchronous execution for consistency and scalability.

## 📝 Adding a New Solution

1. **Create Solution Class**:

```csharp
public class DayXXPartOne : ISolution
{
    public int Day => XX;
    public int Part => 1;
    public string Name { get; set; } = "Problem Name";
    public string Test => Path.Combine("Data", "DayXX", "test.txt");
    public string Input => Path.Combine("Data", "DayXX", "input.txt");
    public int Index => 0;

    public async Task<object> InvokeAsync(bool runTest = false)
    {
        var input = await File.ReadAllTextAsync(runTest ? Test : Input);
        // Implement solution logic
        return result;
    }
}
```

2. **Register in Program.cs**:

```csharp
services.AddTransient<ISolution, DayXXPartOne>();
```

3. **Add Input Files**:
    - Create `Data/DayXX/input.txt`
    - Create `Data/DayXX/test.txt`

4. **Create README** (optional):
    - Add `Solutions/DayXX/README.md` with problem description

## 📊 Completed Solutions

1. ✅ [Day 01 - Secret Entrance](Solutions/Day01/README.md)
2. ✅ [Day 02 - Gift Shop](Solutions/Day02/README.md)
3. ✅ [Day 03 - Lobby](Solutions/Day03/README.md)
4. ✅ [Day 04 - Printing Department](Solutions/Day04/README.md)
5. ✅ [Day 05 - Cafeteria](Solutions/Day05/README.md)
6. ✅ [Day 06 - Trash Compactor](Solutions/Day06/README.md)

## 🔧 Development Tips

- **Test First**: Always validate with test input before running actual input
- **Async Pattern**: Keep solutions async for consistency
- **Error Handling**: Add try-catch blocks for file I/O and parsing
- **Performance**: Consider using `Span<T>` and `Memory<T>` for large inputs
- **Debugging**: Use the test input for easier debugging

## 📚 Learning Resources

- [Spectre.Console Documentation](https://spectreconsole.net/)
- [.NET Dependency Injection](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)
- [Async/Await Best Practices](https://docs.microsoft.com/en-us/archive/msdn-magazine/2013/march/async-await-best-practices-in-asynchronous-programming)

---

[← Back to Main README](../../../README.md)
