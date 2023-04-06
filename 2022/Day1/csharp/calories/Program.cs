public class Program
{
  public static void Main(string[] args)
  {
    string[] input = File.ReadAllLines("D:\\Programming\\repos\\aventOfCode\\AdventOfCode\\2022\\Day1\\csharp\\calories\\input.txt");
    int topThreeTotal = 0;
    int currentTotal = 0;
    int highest = 0;
    int secondHighest = 0;
    int thirdHighest = 0;

    foreach (string line in input)
    {
      if (string.IsNullOrEmpty(line))
      {
        if (currentTotal >= highest)
        {
          thirdHighest = secondHighest;
          secondHighest = highest;
          highest = currentTotal;
        }
        else if (currentTotal >= secondHighest)
        {
          thirdHighest = secondHighest;
          secondHighest = currentTotal;
        }
        else if (currentTotal >= thirdHighest)
        {
          thirdHighest = currentTotal;
        }

        currentTotal = 0;
      }
      else
      {
        int number = int.Parse(line);
        currentTotal += number;
      }
    }

    if (currentTotal >= highest)
    {
      thirdHighest = secondHighest;
      secondHighest = highest;
      highest = currentTotal;
    }
    else if (currentTotal >= secondHighest)
    {
      thirdHighest = secondHighest;
      secondHighest = currentTotal;
    }
    else if (currentTotal >= thirdHighest)
    {
      thirdHighest = currentTotal;
    }

    topThreeTotal = highest + secondHighest + thirdHighest;

    Console.WriteLine(topThreeTotal);
    Console.ReadLine();
  }
}