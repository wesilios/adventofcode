namespace AoC2025.Solutions.Abstractions;

public interface ISolution
{
    string Name { get; set; }
    int Index { get; }
    Task<int> InvokeAsync();
}