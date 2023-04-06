public class Program
{
  public static void Main(string[] args)
  {
    string input = File.ReadAllText("D:\\Programming\\repos\\aventOfCode\\code\\code\\code\\input.txt");
    List<string> fourSet = new List<string>();
    int targetNumber = 0;
    int index = 0;

    do
    {
      foreach (char letter in input)
      {
        string convertedLetter = "" + letter;
        fourSet.Add(convertedLetter);

        if (fourSet.Count > 14)
        {
          fourSet.RemoveAt(0);
        }

        index++;

        bool check = CheckFourSet(fourSet);

        if (check)
        {
          targetNumber = index;
          break;
        }
      }
    } while (targetNumber == 0 && index < input.Length);

    Console.WriteLine(targetNumber);
    Console.ReadLine();
  }

  public static bool CheckFourSet(List<string> _fourSet)
  {
    if (_fourSet.Count != 14)
    {
      return false;
    }

    return _fourSet.Distinct().Count() == 14;
  }
}