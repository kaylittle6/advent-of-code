public class Program
{
  public static string[] Input = File.ReadAllLines(
      "C:\\Users\\klittle\\Source\\advent-of-code\\2022\\Day12\\csharp\\elevation\\input.txt");

  public static List<Node> OpenNodes = new List<Node>();
  public static List<Node> ClosedNodes = new List<Node>();

  public static Node[][] MapNodes = new Node[Input.Length][];

  public static void Main(string[] args)
  {
    for (int array = 0; array < Input.Length; array++)
    {
      MapNodes[array] = new Node[Input[array].Length];

      for (int node = 0; node < Input[array].Length; node++)
      {
        MapNodes[array][node] = new Node(array, node)
        {
          Letter = Input[array][node],
          LetterElevation = Input[array][node]
        };
      }
    }

    GetSurroundingNodes(MapNodes[20][0]);
    //MapNodes[20][0].Letter = '-';
    ClosedNodes.Add(MapNodes[20][0]);
    MapNodes[20][55].LetterElevation = 123;

    do
    {
      CheckAndMove();
      //RefreshMap(MapNodes);

      //Thread.Sleep(500);

    } while (!ClosedNodes.Any(n => n.XPos == 20 && n.YPos == 55));

    var runnerNode = MapNodes[20][55];

    var pathCount = RetracePath(runnerNode);

    Console.WriteLine();
    Console.WriteLine(pathCount.Count);

    Console.ReadLine();
    Console.ReadLine();
    
  }

  // Create new List of Open Nodes and filter by FCost & HCost to find the next movement
  public static void CheckAndMove()
  {
    //var lowestHCostOpenNodes = OpenNodes.OrderBy(n => n.HCost).ToList();
    //var lowestHCost = lowestHCostOpenNodes[0].HCost;
    //var lowestHCostNodes = new List<Node>();

    //foreach (var node in lowestHCostOpenNodes)
    //{
    //  if (node.HCost == lowestHCost)
    //  {
    //    lowestHCostNodes.Add(node);
    //  }
    //}

    //var lowestFCostNodes = lowestHCostNodes.OrderBy(n => n.FCost).ToList();

    //GetSurroundingNodes(lowestFCostNodes[0]);
    //lowestFCostNodes[0].Letter = '-';
    //ClosedNodes.Add(lowestFCostNodes[0]);
    //OpenNodes.Remove(lowestFCostNodes[0]);


    var lowestFCostOpenNodes = OpenNodes.OrderBy(n => n.FCost).ToList();
    var lowestFCost = lowestFCostOpenNodes[0].FCost;
    var lowestFCostNodes = new List<Node>();

    foreach (var node in lowestFCostOpenNodes)
    {
      if (node.FCost == lowestFCost)
      {
        lowestFCostNodes.Add(node);
      }
    }

    var lowestHCostNodes = lowestFCostNodes.OrderBy(n => n.HCost).ToList();

    GetSurroundingNodes(lowestHCostNodes[0]);
    //lowestFCostNodes[0].Letter = '-';
    ClosedNodes.Add(lowestHCostNodes[0]);
    OpenNodes.Remove(lowestHCostNodes[0]);
  }

  // Uses the current Node to find the surrounding Nodes, filters out existing and < 0 value Nodes, assigns Node parent
  public static void GetSurroundingNodes(Node node)
  {
    var newNodes = new List<Node>();

    if (node.Letter == 'S')
    {
      node.LetterElevation = 97;
    }

    if (node.XPos - 1 >= 0)
    {
      newNodes.Add(MapNodes[node.XPos - 1][node.YPos]);
    }
    if (node.XPos + 1 < MapNodes.Length)
    {
      newNodes.Add(MapNodes[node.XPos + 1][node.YPos]);
    }
    if (node.YPos - 1 >= 0)
    {
      newNodes.Add(MapNodes[node.XPos][node.YPos - 1]);
    }
    if (node.YPos + 1 < MapNodes[0].Length)
    {
      newNodes.Add(MapNodes[node.XPos][node.YPos + 1]);
    }

    var filterNodes = newNodes
                        .Where(n => !OpenNodes
                        .Any(openNode => openNode.XPos == n.XPos && openNode.YPos == n.YPos)
                              && !ClosedNodes
                              .Any(closedNode => closedNode.XPos == n.XPos && closedNode.YPos == n.YPos)
                              && n.LetterElevation != 'a'
                              && node.LetterElevation - n.LetterElevation > -2)
                        .ToList();

    foreach (var n in filterNodes)
    {
      n.ParentNode = node;
      // n.Letter = 'X';
    }

    OpenNodes.AddRange(filterNodes);
  }

  // Creates a "Runner" Node to retrace the shortest path backwards
  public static List<Node> RetracePath(Node node)
  {
    var pathNodes = new List<Node>();
    var currentNode = node;

    while (currentNode.XPos != 20 || currentNode.YPos != 0)
    {
      currentNode.Letter = '-';
      pathNodes.Add(currentNode);
      currentNode = currentNode.ParentNode;

      RefreshMap(MapNodes);
      Thread.Sleep(500);
    }

    return pathNodes;
  }

  // Update Node Map
  public static void RefreshMap(Node[][] mapNodes)
  {
    Console.Clear();

    foreach (var array in mapNodes)
    {
      foreach (var node in array)
      {
        Console.Write(node.Letter.ToString());
      }

      Console.WriteLine();
    }
  }

  public class Node
  {
    private readonly int StartingXPos = 20;
    private readonly int StartingYPos = 0;
    private readonly int EndingXPos = 20;
    private readonly int EndingYPos = 55;
    public int XPos { get; set; }
    public int YPos { get; set; }
    public int GCost => Math.Abs((XPos - StartingXPos) * 10) + Math.Abs((YPos - StartingYPos) * 10);
    public int HCost => Math.Abs((XPos - EndingXPos) * 10) + Math.Abs((YPos - EndingYPos) * 10);
    public int FCost => GCost + HCost;
    public char Letter { get; set; }
    public int LetterElevation { get; set; }
    public Node? ParentNode { get; set; }

    public Node(int xPos, int yPos)
    {
      XPos = xPos;
      YPos = yPos;
    }
  }
}
