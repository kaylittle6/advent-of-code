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
    ClosedNodes.Add(MapNodes[20][0]);

    do
    {
      CheckAndMove();
      RefreshMap(MapNodes);

      //Thread.Sleep(1500);

    } while (!ClosedNodes.Any(n => n.XPos == 55 && n.YPos == 20));

    foreach (var node in ClosedNodes)
    {
      Console.WriteLine(node.ToString());
    }

    RefreshMap(MapNodes);

    Console.ReadLine();
    Console.ReadLine();
    
  }

  public static void CheckAndMove()
  {
    var sortedOpenNodes = OpenNodes.OrderBy(n => n.FCost).ToList();
    var lowestFCost = sortedOpenNodes[0].FCost;
    List<Node> lowestFCostNodes = new List<Node>();

    foreach (var node in sortedOpenNodes)
    {
      if (node.FCost == lowestFCost)
      {
        lowestFCostNodes.Add(node);
      }
    }

    foreach (var node in lowestFCostNodes)
    {
      GetSurroundingNodes(node);
      node.Letter = '0';
      ClosedNodes.Add(node);
      OpenNodes.Remove(node);
    }
  }

  public static void GetSurroundingNodes(Node node)
  {
    var newNodes = new List<Node>();

    if (node.Letter == 'S')
    {
      node.Letter = 'a';
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

    //var nonZeroNodes = newNodes.Where(n => n.XPos >= 0 && n.XPos <= 79 && n.YPos >= 0 && n.YPos <= 40).ToList();
    var filterNodes = newNodes
                        .Where(n => !OpenNodes
                        .Any(openNode => openNode.XPos == n.XPos && openNode.YPos == n.YPos)
                              && !ClosedNodes
                              .Any(closedNode => closedNode.XPos == n.XPos && closedNode.YPos == n.YPos)
                              && Math.Abs(node.LetterElevation - n.LetterElevation) <= 1)
                        .ToList();

    foreach (var n in filterNodes)
    {
      n.Letter = '1';
    }

    OpenNodes.AddRange(filterNodes);
  }

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
    private readonly int StartingXPos = 0;
    private readonly int StartingYPos = 20;
    private readonly int EndingXPos = 55;
    private readonly int EndingYPos = 20;
    public int XPos { get; set; }
    public int YPos { get; set; }
    public int GCost => Math.Abs((XPos - StartingXPos) * 10) + Math.Abs((YPos - StartingYPos) * 10);
    public int HCost => Math.Abs((XPos - EndingXPos) * 10) + Math.Abs((YPos - EndingYPos) * 10);
    public int FCost => GCost + HCost;
    public char Letter { get; set; }
    public int LetterElevation { get; set; }

    public Node(int xPos, int yPos)
    {
      XPos = xPos;
      YPos = yPos;
    }
  }
}
