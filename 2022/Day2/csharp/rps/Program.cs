public class Program
{
  public static void Main(string[] args)
  {
    string[] input = File.ReadAllLines("C:\\Users\\klittle\\source\\vscPractice\\AoC\\rps\\rps\\input.txt");
    int totalScore = 0;

    Logic logic = new();

    foreach (string line in input)
    {
      string[] splitLines = line.Split(' ');
      string[] correctThrows = logic.CorrectThrow(splitLines[0], splitLines[1]);

      totalScore += logic.GetScore(correctThrows[0], correctThrows[1]);
    }

    Console.WriteLine(totalScore);
    Console.ReadLine();
  }

  public class Logic
  {
    public int GetScore(string _rival, string _yours)
    {
      int totalScore = 0;

      if ((_rival == "A" && _yours == "Y") || (_rival == "B" && _yours == "Z") || (_rival == "C" && _yours == "X"))
      {
        totalScore += 6;
      }

      else if ((_rival == "A" && _yours == "X") || (_rival == "B" && _yours == "Y") || (_rival == "C" && _yours == "Z"))
      {
        totalScore += 3;
      }

      switch (_yours)
      {
        case "X":
          totalScore += 1;
          break;

        case "Y":
          totalScore += 2;
          break;

        case "Z":
          totalScore += 3;
          break;
      }

      return totalScore;
    }

    public string[] CorrectThrow(string _rival, string _yours)
    {
      switch (_rival)
      {
        case "A":
          switch (_yours)
          {
            case "X":
              _yours = "Z";
              break;
            case "Y":
              _yours = "X";
              break;
            case "Z":
              _yours = "Y";
              break;
          }
          break;
        case "C":
          switch (_yours)
          {
            case "X":
              _yours = "Y";
              break;
            case "Y":
              _yours = "Z";
              break;
            case "Z":
              _yours = "X";
              break;
          }
          break;
      }

      string[] newThrows = { _rival, _yours };
      return newThrows;
    }
  }
}