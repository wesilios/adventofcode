using AoC2025.Solutions.Abstractions;
using Microsoft.Extensions.Logging;

namespace AoC2025.Solutions.Day07;

public class LaboratoriesPartTwo : ISolution
{
    public LaboratoriesPartTwo(ILogger<LaboratoriesPartTwo> logger)
    {
        _logger = logger;
        Name = "--- Day 7: Laboratories Part Two---";
    }

    public int Day => 7;
    public string Name { get; set; }
    public int Part => 2;
    public string Test => "Data/Day07/tests.txt";
    public string Input => "Data/Day07/input.txt";

    public int Index => 14;
    private const string Start = "S";
    private const string Splitter = "^";
    private const string TravelPath = "|";

    private readonly ILogger<LaboratoriesPartTwo> _logger;

    public async Task<object> InvokeAsync(bool runTest = false)
    {
        var tachyonDiagram = new List<List<string>>();
        await foreach (var line in File.ReadLinesAsync(Path.Combine(Directory.GetCurrentDirectory(),
                           runTest ? Test : Input)))
        {
            tachyonDiagram.Add(line.Select(c => c.ToString()).ToList());
        }

        var result = 0;
        var yAxis = 0;
        var tree = new Dictionary<string, TreeNode>();
        while (yAxis < tachyonDiagram.Count - 1)
        {
            for (var xAxis = 0; xAxis < tachyonDiagram[yAxis].Count; xAxis++)
            {
                TreeNode? previousNode = null;
                if ((yAxis == 0 && tachyonDiagram[yAxis][xAxis] == Start) ||
                        tree.TryGetValue($"{xAxis},{yAxis}", out previousNode))
                //tachyonDiagram[yAxis][xAxis] == TravelPath)
                {
                    if (tachyonDiagram[yAxis + 1][xAxis] != Splitter)
                    {
                        //tachyonDiagram[yAxis + 1][xAxis] = TravelPath;
                        if (!tree.TryGetValue($"{xAxis},{yAxis + 1}", out var node))
                        {
                            node = new TreeNode(xAxis, yAxis + 1);
                            tree.Add(node.Axes, node);
                        }

                        if (previousNode is not null)
                        {
                            previousNode.AddNode(node);
                        }

                        continue;
                    }

                    if (xAxis - 1 >= 0 && tachyonDiagram[yAxis + 1][xAxis - 1] != Splitter)
                    {
                        //tachyonDiagram[yAxis + 1][xAxis - 1] = TravelPath;
                        if (!tree.TryGetValue($"{xAxis - 1},{yAxis + 1}", out var node))
                        {
                            node = new TreeNode(xAxis - 1, yAxis + 1);
                            tree.Add(node.Axes, node);
                        }

                        previousNode.AddNode(node);
                    }

                    if (xAxis + 1 < tachyonDiagram[yAxis + 1].Count &&
                            tachyonDiagram[yAxis + 1][xAxis + 1] != Splitter)
                    {
                        // tachyonDiagram[yAxis + 1][xAxis + 1] = TravelPath;
                        if (!tree.TryGetValue($"{xAxis + 1},{yAxis + 1}", out var node))
                        {
                            node = new TreeNode(xAxis + 1, yAxis + 1);
                            tree.Add(node.Axes, node);
                        }

                        previousNode.AddNode(node);
                    }

                    result++;
                }
            }

            yAxis++;
        }

        // return result;
        return tree.First().Value.GetPaths().Count();
    }
}

internal class TreeNode
{
    public Guid Id { get; set; }
    public int? YAxis { get; }
    public int? XAxis { get; }
    public string Axes => $"{XAxis},{YAxis}";
    public TreeNode? LeftNode { get; private set; }
    public TreeNode? RightNode { get; private set; }

    public TreeNode(int xAxis, int yAxis)
    {
        Id = Guid.NewGuid();
        YAxis = yAxis;
        XAxis = xAxis;
    }

    public void AddNode(TreeNode node)
    {
        if (XAxis < node.XAxis)
        {
            RightNode = node;
            return;
        }

        LeftNode = node;
    }

    public List<string> GetPaths()
    {
        var results = new List<string>();
        if (LeftNode is null && RightNode is null)
        {
            results.Add(Axes);
            // return results;
        }

        if (LeftNode is not null)
        {
            foreach (var path in LeftNode.GetPaths())
            {
                results.Add(Axes + $"-{path}");
            }
        }

        if (RightNode is not null)
        {
            foreach (var path in RightNode.GetPaths())
            {
                results.Add(Axes + $"-{path}");
            }
        }

        return results;
    }
}

