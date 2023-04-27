public class Program
{
  public static void Main(string[] args)
  {
    string[] input = File.ReadAllLines("D:\\Programming\\repos\\aventOfCode\\AdventOfCode\\2022\\Day12\\csharp\\elevation\\input.txt");
    Node[][] indexField = ParseAndConvertInput(input);

    Node startingNode = new();
    Node goalNode = new();

    GetStartingNodes(indexField, startingNode, goalNode);

    Node currentNode = startingNode;

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

  public static void GetStartingNodes(Node[][] _indexField, Node _startingNode, Node _goalNode)
  {
    for (int i = 0; i < _indexField.Length; i++)
    {
      for (int j = 0; j < _indexField[i].Length; j++)
      {
        if (_indexField[i][j].LetterValue == 'S')
        {
          _startingNode.CurrentPosition[0] = j;
          _startingNode.CurrentPosition[1] = i;
        }
        else if (_indexField[i][j].LetterValue == 'E')
        {
          _goalNode.CurrentPosition[0] = j;
          _goalNode.CurrentPosition[1] = i;
        }
      }
    }
  }

  public static List<Node> GetNeighborNodesAndMovementCost(Node[][] _indexField, Node _currentNode)
  {
    for (int i = 0; i < _indexField.Length; i++)
    {
      foreach (Node node in _indexField[i])
      {
        if (node.CurrentPosition[0] == Math.Abs(_currentNode.CurrentPosition[0] + 1))
      }
    }
  }

  public class Node
  {
    public int LetterValue { get; set; }
    public int[] CurrentPosition { get; set; } = new int[2];
    public int FromStartNode { get; set; }
    public int FromEndNode { get; set; }
    public int MovementCost => FromStartNode + FromEndNode;

    public int GetFromStartNodeValue(Node[][] _indexField, Node _startingNode)
    {
      int xdiff = Math.Abs(_startingNode.CurrentPosition[0] - CurrentPosition[0]);
      int ydiff = Math.Abs(_startingNode.CurrentPosition[1] - CurrentPosition[1]);


    }
  }
}

