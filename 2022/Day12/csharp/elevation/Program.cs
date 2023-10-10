public class Program
{
  public string[] Input = File.ReadAllLines(
      "C:\\Users\\klittle\\Source\\advent-of-code\\2022\\Day11\\csharp\\monkey\\input.txt");

  public List<Node> OpenNodes = new List<Node>();
  public List<Node> ClosedNodes = new List<Node>();

  public void Main(string[] args)
  {
    

    var currentNode = new Node(0, 20) { LetterElevation = 'a' };
    



  }

  public void GetSurroundingNodes(Node currentNode)
  {
    var newNodes = new List<Node>()
    {
      new Node(currentNode.XPos - 1, currentNode.YPos),
      new Node(currentNode.XPos + 1, currentNode.YPos),
      new Node(currentNode.XPos, currentNode.YPos - 1),
      new Node(currentNode.XPos, currentNode.YPos + 1)
    };

    var nonZeroNodes = newNodes.Where(n => n.XPos >= 0 && n.XPos <= 79 && n.YPos >= 0 && n.YPos <= 79).ToList();
    var filterNodes = nonZeroNodes.
                        Where(node => !ClosedNodes.
                        Any(closedNode => closedNode.XPos == node.XPos && closedNode.YPos == node.YPos)).
                        ToList();

    foreach (var node in newNodes)
    {
      node.LetterElevation = Input[node.XPos][node.YPos];
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
    public int LetterElevation { get; set; }

    public Node(int xPos, int yPos)
    {
      XPos = xPos;
      YPos = yPos;
    }
  }
}
