﻿public class Program
{
  public static void Main(string[] args)
  {
    string[] input = File.ReadAllLines("C:\\Users\\klittle\\source\\vscPractice\\AoC\\2022\\Day12\\csharp\\elevation\\input.txt");
    Node[][] indexField = ParseAndConvertInput(input);
    int stepCounter = 0;
    List<Node> openNodes = new();
    List<Node> closedNodes = new();
    Node currentNode = new()
    {
      LetterValue = 'a',
      CurrentPosition = new int[] { 0, 20 }
    };

    do
    {
      openNodes = GetNeighborNodes(indexField, currentNode, openNodes);

      currentNode = MoveCurrentNode(currentNode, openNodes, closedNodes);

      stepCounter++;

    } while (currentNode.CurrentPosition[0] != 55 || currentNode.CurrentPosition[1] != 20);

    Console.WriteLine(stepCounter);
    Console.ReadLine();
  }

  public static Node[][] ParseAndConvertInput(string[] _input)
  {
    Node[][] indexField = new Node[_input.Length][];

    for (int i = 0; i < indexField.Length; i++)
    {
      foreach (string line in _input)
      {
        indexField[i] = new Node[line.Length];

        for (int j = 0; j < line.Length; j++)
        {
          indexField[i][j] = new Node()
          {
            LetterValue = line[j],
            CurrentPosition = new int[] { j, i },
          };
        }

        i++;
      }
    }

    return indexField;
  }

  public static List<Node> GetNeighborNodes(Node[][] _indexField, Node _currentNode, List<Node> _openNodes)
  {
    for (int i = 0; i < _indexField.Length; i++)
    {
      foreach (Node node in _indexField[i])
      {
        if (Math.Abs(node.CurrentPosition[0] - _currentNode.CurrentPosition[0]) < 2
          && Math.Abs(node.CurrentPosition[1] - _currentNode.CurrentPosition[1]) < 2)
        {
          if (node.CurrentPosition[0] != _currentNode.CurrentPosition[0]
            || node.CurrentPosition[1] != _currentNode.CurrentPosition[1])
          {
            _openNodes.Add(node);
          }
        }
      }
    }

    return _openNodes;
  }

  public static Node MoveCurrentNode(Node _currentNode, List<Node> _openNodes, List<Node> _closedNodes)
  {
    List<Node> letterList = new();

    foreach (Node node in _openNodes)
    {
      if (_currentNode.LetterValue + 1 >= node.LetterValue 
        && !_closedNodes.Contains(node))
      {
        letterList.Add(node);
      }
    }

    if (letterList.Count == 0)
    {
      Console.WriteLine("No Moves left on step");
      Console.ReadLine();
    }

    var groupedList = letterList.GroupBy(gc => gc.GCost).ToList();
    var orderedList = groupedList.OrderBy(gc => gc.Key).First();

    if (orderedList.Count() == 1)
    {
      var flattenGroup = orderedList.Select(group => group).ToList();
      _currentNode = flattenGroup.First();
    }
    else
    {
      groupedList = letterList.GroupBy(mc => mc.MovementCost).ToList();
      orderedList = groupedList.OrderBy(mc => mc.Key).First();
      var flattenGroup = groupedList.SelectMany(_group => _group).ToList();

      _currentNode = flattenGroup.First();
    }

    _closedNodes.AddRange(_openNodes);
    _openNodes.Clear();

    return _currentNode;
  }

  public class Node
  {
    public int LetterValue { get; set; }
    public int[] CurrentPosition { get; set; } = new int[2];
    public int[] StartNode { get; set; } = new int[] { 0, 20 };
    public int[] GoalNode { get; set; } = new int[] { 55, 20 };
    public int SCost => GetSCost();
    public int GCost => GetGCost();
    public int MovementCost => SCost + GCost;

    private int GetSCost()
    {
      var xCost = Math.Abs(CurrentPosition[0] - StartNode[0]);
      var yCost = Math.Abs(CurrentPosition[1] - StartNode[1]);

      if (xCost < yCost)
      {
        var dCost = xCost * 14;
        var leftOver = Math.Abs(xCost - yCost);
        var lCost = leftOver * 10;

        return lCost + dCost;
      }
      else if (yCost < xCost)
      {
        var dCost = yCost * 14;
        var leftOver = Math.Abs(xCost - yCost);
        var lCost = leftOver * 10;

        return lCost + dCost;
      }
      else
      {
        return xCost * 14;
      }
    }

    private int GetGCost()
    {
      var xCost = Math.Abs(CurrentPosition[0] - GoalNode[0]);
      var yCost = Math.Abs(CurrentPosition[1] - GoalNode[1]);

      if (xCost < yCost)
      {
        var dCost = xCost * 14;
        var leftOver = Math.Abs(xCost - yCost);
        var lCost = leftOver * 10;

        return lCost + dCost;
      }
      else if (yCost < xCost)
      {
        var dCost = yCost * 14;
        var leftOver = Math.Abs(xCost - yCost);
        var lCost = leftOver * 10;

        return lCost + dCost;
      }
      else
      {
        return xCost * 14;
      }
    }
  }
}
