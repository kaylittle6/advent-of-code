public class Program
{
  public int CurrentTotal { get; set; } = 0;
  public int Highest { get; set; } = 0;
  public int SecondHighest { get; set; } = 0;
  public int ThirdHighest { get; set; } = 0;
  public int TopThreeTotal => Highest + SecondHighest + ThirdHighest;

  public static void Main(string[] args)
  {
    string[] input = File.ReadAllLines("D:\\Programming\\repos\\aventOfCode\\AdventOfCode\\2022\\Day1\\csharp\\calories\\input.txt");

    Program totals = new();

    foreach (string line in input)
    {
      if (string.IsNullOrEmpty(line))
      {
        UpdateLeaderBoard(totals);

        totals.CurrentTotal = 0;
      }
      else
      {
        totals.CurrentTotal += int.Parse(line);
      }
    }

    UpdateLeaderBoard(totals);

    Console.WriteLine(totals.TopThreeTotal);
    Console.ReadLine();
  }

  public static void UpdateLeaderBoard(Program _totals)
  {
    if (_totals.CurrentTotal >= _totals.Highest)
    {
      _totals.ThirdHighest = _totals.SecondHighest;
      _totals.SecondHighest = _totals.Highest;
      _totals.Highest = _totals.CurrentTotal;
    }
    else if (_totals.CurrentTotal >= _totals.SecondHighest)
    {
      _totals.ThirdHighest = _totals.SecondHighest;
      _totals.SecondHighest = _totals.CurrentTotal;
    }
    else if (_totals.CurrentTotal >= _totals.ThirdHighest)
    {
      _totals.ThirdHighest = _totals.CurrentTotal;
    }
  }
}