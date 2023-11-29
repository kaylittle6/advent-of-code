public class Program
{
  public static string[] Input = File.ReadAllLines(
      "C:\\Users\\klittle\\Source\\advent-of-code\\2022\\Day12\\csharp\\elevation\\input.txt");

  public static List<Node> OpenNodes = new List<Node>();
  public static List<Node> ClosedNodes = new List<Node>();

  public static void Main(string[] args)
  {
    var starterNode = new Node(0, 20) { LetterElevation = 'a' };

    ClosedNodes.Add(starterNode);
    GetSurroundingNodes(starterNode);

    do
    {
      CheckAndMove();
    

    } while (ClosedNodes.Any(n => n.XPos == 55 && n.YPos == 20));

    Console.WriteLine();

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
      ClosedNodes.Add(node);
      OpenNodes.Remove(node);
    }
  }

  public static void GetSurroundingNodes(Node node)
  {
    var newNodes = new List<Node>()
    {
      new Node(node.XPos - 1, node.YPos),
      new Node(node.XPos + 1, node.YPos),
      new Node(node.XPos, node.YPos - 1),
      new Node(node.XPos, node.YPos + 1)
    };

    var nonZeroNodes = newNodes.Where(n => n.XPos >= 0 && n.XPos <= 79 && n.YPos >= 0 && n.YPos <= 79).ToList();
    var filterNodes = nonZeroNodes
                        .Where(node => !OpenNodes
                        .Any(openNode => openNode.XPos == node.XPos && openNode.YPos == node.YPos)
                              && !ClosedNodes
                              .Any(closedNode => closedNode.XPos == node.XPos && closedNode.YPos == node.YPos)).ToList();

    foreach (var n in filterNodes)
    {
      n.LetterElevation = Input[n.XPos][n.YPos];
      n.PreviousElevation = node.LetterElevation;
    }

    OpenNodes.AddRange(filterNodes);
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
    public int LetterElevation { get; set; }
    public int PreviousElevation { get; set; }

    public Node(int xPos, int yPos)
    {
      XPos = xPos;
      YPos = yPos;
    }
  }
}
