public class Program
{
  public static void Main(string[] args)
  {
    string[] input = File.ReadAllLines("D:\\Programming\\repos\\aventOfCode\\crates\\crates\\input.txt").Skip(10).ToArray();
    var instructions = ParseInstructions(input);
    Stacks masterStack = new();

    foreach (int[] numbers in instructions)
    {
      var amountToMove = numbers[0];
      var fromStack = numbers[1] - 1;
      var toStack = numbers[2] - 1;

      List<string> tempString = new List<string>();

      if (amountToMove == masterStack.StackList[fromStack].Count())
      {
        masterStack.StackList[toStack].AddRange(masterStack.StackList[fromStack]);
        masterStack.StackList[fromStack].RemoveRange(masterStack.StackList[fromStack].Count - amountToMove, amountToMove);
      }
      else
      {
        for (int i = 0; i < amountToMove; i++)
        {
          var lastLetter = masterStack.StackList[fromStack].Last();
          tempString.Insert(0, lastLetter);
          masterStack.StackList[fromStack].RemoveAt(masterStack.StackList[fromStack].Count - 1);
        }

        masterStack.StackList[toStack].AddRange(tempString);
        tempString.Clear();
      }
    }

    foreach (List<string> letters in masterStack.StackList)
    {
      Console.WriteLine(letters.Last());
    }

    Console.ReadLine();
  }

  public static List<int[]> ParseInstructions(string[] _lines)
  {
    List<int[]> finalNumbers = new List<int[]>();

    foreach (string line in _lines)
    {
      var removeWords = line.Replace("move ", "");
      removeWords = removeWords.Replace(" from ", " ");
      removeWords = removeWords.Replace(" to ", " ");
      string[] wordsRemoved = removeWords.Split(" ");

      int[] parsedInput = { Int32.Parse(wordsRemoved[0]), Int32.Parse(wordsRemoved[1]), Int32.Parse(wordsRemoved[2]) };

      finalNumbers.Add(parsedInput);
    }

    return finalNumbers;
  }

  public class Stacks
  {
    public List<string>[] StackList { get; set; }

    public Stacks()
    {
      StackList = new List<string>[9];
      StackList[0] = new List<string>() { "W", "M", "L", "F" };
      StackList[1] = new List<string>() { "B", "Z", "V", "M", "F" };
      StackList[2] = new List<string>() { "H", "V", "R", "S", "L", "Q" };
      StackList[3] = new List<string>() { "F", "S", "V", "Q", "P", "M", "T", "J" };
      StackList[4] = new List<string>() { "L", "S", "W" };
      StackList[5] = new List<string>() { "F", "V", "P", "M", "R", "J", "W" };
      StackList[6] = new List<string>() { "J", "Q", "C", "P", "N", "R", "F" };
      StackList[7] = new List<string>() { "V", "H", "P", "S", "Z", "W", "R", "B" };
      StackList[8] = new List<string>() { "B", "M", "J", "C", "G", "H", "Z", "W" };
    }
  }
}