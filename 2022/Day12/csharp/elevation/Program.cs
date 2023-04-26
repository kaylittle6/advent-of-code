public class Program
{
  public static void Main(string[] args)
  {
    string[] input = File.ReadAllLines("C:\\Users\\klittle\\source\\vscPractice\\AoC\\2022\\Day12\\csharp\\elevation\\input.txt");
    char[][] indexField = ParseAndConvertInput(input);
    Node currentNode = new Node()
    {
      CurrentX = indexField[20][0],
      //CurrentY
    };
      
      
    //var = indexField[20][0];
    //var goalNode = indexField[20][54];








    Console.WriteLine(indexField);
    Console.ReadLine();
  }

  public static char[][] ParseAndConvertInput(string[] _input)
  {
    char[][] indexField = new char[41][];

    for (int i = 0; i < indexField.Length; i++)
    {
      foreach (string line in _input)
      {
        indexField[i] = new char[80];

        for (int j = 0; j < line.Length; j++)
        {
          indexField[i][j] = line[j];
        }

        i++;
      }
    }

    return indexField;
  }

  public class Node
  {
    public int CurrentX { get; set; }
    public int CurrentY { get; set; }
    public int FromStartNode { get; set; }
    public int FromEndNode { get; set; }
    public int MovementCost => FromStartNode + FromEndNode;


  }
}




