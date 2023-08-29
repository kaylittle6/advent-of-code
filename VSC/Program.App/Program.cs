public class Program
{
  public static void Main(string[] args)
  {
    string[] input = File.ReadAllLines(
    "C:\\Users\\klittle\\source\\vscPractice\\AoC\\VSC\\Program.App\\input.txt");

    var monkeyList = ParseInstructions(input);

    for (int round = 0; round < 20; round++)
    {
      for (int monkey = 0; monkey < monkeyList.Count; monkey++)
      {
        for (int item = 0; item < monkeyList[monkey].Items.Count; item++)
        {
          var newWorryLevel = monkeyList[monkey].InspectItem(item);
          var throwMonkey = monkeyList[monkey].TestAndFindNextMonkey(newWorryLevel);
          var dividedWorryLevel = newWorryLevel / 3;

          monkeyList[throwMonkey].Items.Add(dividedWorryLevel);
        }

        monkeyList[monkey].Items.Clear();
      }
    }

    var orderedList = monkeyList.OrderByDescending(i => i.Inspections).ToList();
    var monkeyBusiness = orderedList[0].Inspections * orderedList[1].Inspections;

    Console.WriteLine(monkeyBusiness);

    Console.ReadLine();
  }


  public static List<Monkey> ParseInstructions(string[] input)
  {
    List<Monkey> monkeyList = new List<Monkey>();
    List<string> trimmedList = new List<string>();

    for (int i = 0; i < 8; i++)
    {
      monkeyList.Add(new Monkey(i));
    }

    foreach (string line in input)
    {
      var replaceLine = line.Replace(",", "").Replace(":", "");
      var trimmedLine = replaceLine.TrimStart();

      trimmedList.Add(trimmedLine);
    }

    for (int i = 0; i < monkeyList.Count; i++)
    {
      foreach (string line in trimmedList)
      {
        if (String.IsNullOrEmpty(line))
        {
          i++;
          continue;
        }

        var splitLine = line.Split(" ");

        AssignValue(splitLine[0], line, monkeyList, i);
      }
    }

    return monkeyList;
  }

  public static void AssignValue(string keyword, string line, List<Monkey> monkeyList, int index)
  {
    if (keyword == "Starting")
    {
      var parts = line.Split(" ");

      foreach (string part in parts)
      {
        if (int.TryParse(part, out int value))
        {
          monkeyList[index].Items.Add(value);
        }
      }
    }

    else if (keyword == "Operation")
    {
      var splitLines = line.Split(" ");

      monkeyList[index].OperationOperator = splitLines[4];

      if (int.TryParse(splitLines[5], out int result))
      {
        monkeyList[index].OperationNumber = result;
      }
      
      else
      {
        monkeyList[index].OperationNumber = 0;
      }
    }

    else if (keyword == "Test")
    {
      var splitLines = line.Split(" ");

      monkeyList[index].TestNumber = int.Parse(splitLines[3]);
    }

    else if (keyword == "If")
    {
      var splitLines = line.Split(" ");

      if (splitLines[1] == "true")
      {
        monkeyList[index].TrueMonkey = int.Parse(splitLines[5]);
      }

      else
      {
        monkeyList[index].FalseMonkey = int.Parse(splitLines[5]);
      }
    }
  }
}

public class Monkey
{
  public int MonkeyId { get; set; }
  public List<int> Items { get; set; }
  public string? OperationOperator { get; set; }
  public int OperationNumber { get; set; }
  public int TestNumber { get; set; }
  public int TrueMonkey { get; set; }
  public int FalseMonkey { get; set; }
  public int Inspections { get; set; } = 0;

  public Monkey(int monkeyId)
  {
    MonkeyId = monkeyId;
    Items = new List<int>();
  }

  public int InspectItem(int index)
  {
    Inspections++;

    if (OperationOperator == "+")
    {
      return Items[index] + OperationNumber;
    }

    else if (OperationOperator == "*")
    {
      if (OperationNumber == 0)
      {
        return Items[index] * Items[index];
      }

      else
      {
        return Items[index] * OperationNumber;
      }
    }

    return 0;
  }

  public int TestAndFindNextMonkey(int newWorryLevel)
  {
    int updatedWorryLevel = newWorryLevel / 3;
    var testWorryLevel = updatedWorryLevel % TestNumber;

    // hiya = updatedWorryLevel;

    if (testWorryLevel == 0)
    {
      return TrueMonkey;
    }
    else
    {
      return FalseMonkey;
    }
  }
}
