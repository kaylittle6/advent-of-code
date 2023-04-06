public class Program
{
  public static void Main(string[] args)
  {
    string[] input = File.ReadAllLines("C:\\Users\\klittle\\source\\vscPractice\\AoC\\ranges\\ranges\\ranges\\input.txt");
    int matchingPairs = 0;
    Logic logic = new();

    foreach (string line in input)
    {
      var parsedNumbers = logic.SplitLines(line);
      var covers = logic.DoesItOverlap(parsedNumbers);

      if (covers)
      {
        matchingPairs++;
      }
    }

    Console.WriteLine(matchingPairs.ToString());
    Console.ReadLine();
  }

  public class Logic
  {
    public int[][] SplitLines(string _line)
    {
      var splitNumbers = _line.Split(',');
      var firstNumbers = splitNumbers[0].Split('-');
      var secondNumbers = splitNumbers[1].Split('-');

      int[] parsedFirst = { Int32.Parse(firstNumbers[0]), Int32.Parse(firstNumbers[1]) };
      int[] parsedSecond = { Int32.Parse(secondNumbers[0]), Int32.Parse(secondNumbers[1]) };

      int[][] finalArray = { parsedFirst, parsedSecond };

      return finalArray;
    }

    public bool IsOneInRange(int[][] _numbers)
    {
      var result = ((_numbers[0].Min() >= _numbers[1].Min()) && (_numbers[0].Max() <= _numbers[1].Max()))
        || ((_numbers[1].Min() >= _numbers[0].Min()) && (_numbers[1].Max() <= _numbers[0].Max()));

      return result;
    }

    public bool DoesItOverlap(int[][] _numbers)
    {
      var result = (_numbers[0].Min() >= _numbers[1].Min() && _numbers[0].Min() <= _numbers[1].Max())
        || (_numbers[0].Max() <= _numbers[1].Max() && _numbers[0].Max() >= _numbers[1].Min())
        || (_numbers[1].Min() >= _numbers[0].Min() && _numbers[1].Min() <= _numbers[0].Max())
        || (_numbers[1].Max() <= _numbers[0].Max() && _numbers[1].Max() >= _numbers[0].Min());

      return result;
    }
  }
}