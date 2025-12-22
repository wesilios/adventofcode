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
        var tree = new Stack<TreeNode>();
        var rowIndex = 0;
        var columnIndex = 0;
        while (rowIndex < tachyonDiagram.Count - 1)
        {
            if (columnIndex == tachyonDiagram[rowIndex].Count) columnIndex = 0;
            while (columnIndex < tachyonDiagram[rowIndex].Count)
            {
                if ((rowIndex == 0 && tachyonDiagram[rowIndex][columnIndex] == Start) ||
                        tachyonDiagram[rowIndex][columnIndex] == TravelPath)
                {
                    if (tachyonDiagram[rowIndex + 1][columnIndex] != Splitter)
                    {
                        tachyonDiagram[rowIndex + 1][columnIndex] = TravelPath;
                        break;
                    }

                    var node = new TreeNode(rowIndex, columnIndex);
                    var isNew = true;
                    if (tree.Count > 0)
                    {
                        var peek = tree.Peek();
                        if (peek.RowIndex == rowIndex && peek.ColumnIndex == columnIndex)
                        {
                            node = peek;
                            isNew = false;
                        }
                    }

                    if (columnIndex - 1 >= 0 &&
                        !node.MoveLeft &&
                        tachyonDiagram[rowIndex + 1][columnIndex - 1] != Splitter)
                    {
                        tachyonDiagram[rowIndex + 1][columnIndex - 1] = TravelPath;
                        node.MoveLeft = true;
                        if (isNew) tree.Push(node);
                        columnIndex--;
                        break;
                    }

                    if (columnIndex + 1 <= tachyonDiagram[rowIndex + 1].Count &&
                        !node.MoveRight &&
                        tachyonDiagram[rowIndex + 1][columnIndex + 1] != Splitter)
                    {
                        tachyonDiagram[rowIndex + 1][columnIndex + 1] = TravelPath;
                        node.MoveRight = true;
                        if (isNew) tree.Push(node);
                        columnIndex++;
                        break;
                    }

                }

                columnIndex++;
            }

            rowIndex++;
            if (rowIndex == tachyonDiagram.Count - 1)
            {
                result++;
                while (tree.Count != 0 && tree.Peek().Done)
                {
                    tree.Pop();
                }
                if (tree.Count == 0) break;
                rowIndex = tree.Peek().RowIndex;
                columnIndex = tree.Peek().ColumnIndex;
            }
        }

        return result;
    }
}

internal class TreeNode
{
    public bool IsNullNode { get; set; }
    public int RowIndex { get; }
    public int ColumnIndex { get; }
    public bool MoveLeft { get; set; }
    public bool MoveRight { get; set; }
    public bool IsNew => !MoveLeft && !MoveRight;
    public bool Done => MoveLeft && MoveRight;

    public TreeNode(int rowIndex, int columnIndex)
    {
        RowIndex = rowIndex;
        ColumnIndex = columnIndex;
    }
}
