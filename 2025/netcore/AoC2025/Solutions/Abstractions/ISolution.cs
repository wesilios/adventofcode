namespace AoC2025.Solutions.Abstractions;

public interface ISolution
{
    int Day { get; }
    int Part { get; }
    string Name { get; set; }
    string Test { get; }
    string Input { get; }
    int Index { get; }
    Task<object> InvokeAsync(bool runTest = false);
}