public class Program
{
  public static void Main(string[] args)
  {
    string[] input = File.ReadAllLines("C:\\Users\\klittle\\source\\vscPractice\\AoC\\2022\\Day12\\csharp\\elevation\\input.txt");
    Node[][] indexField = ParseAndConvertInput(input);

    List<Node> openNodes = new();
    List<Node> closedNoses = new();



    Console.WriteLine(indexField);
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

  //public static List<Node> GetNeighborNodes(Node[][] _indexField, Node _currentNode)
  //{
  //  for (int i = 0; i < _indexField.Length; i++)
  //  {
  //    foreach (Node node in _indexField[i])
  //    {
  //      if ((node.CurrentPosition[0] == _currentNode.CurrentPosition[0])
  //        || (node.CurrentPosition[1] == _currentNode.CurrentPosition[1]))
  //      {

  //      }
  //    }
  //  }
  //}

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
