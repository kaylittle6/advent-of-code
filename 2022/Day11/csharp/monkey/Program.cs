public class Program
{
  public static void Main(string[] args)
  {
    string[] input = File.ReadAllLines("C:\\Users\\klittle\\source\\vscPractice\\AoC\\2022\\Day11\\csharp\\monkey\\input.txt");

    Monkey[] monkeys = new Monkey[8];

    for (int i = 0; i < 8; i++)
    {
      monkeys[i] = new Monkey();
    }

    ParseInstructions(input, monkeys);

    for (int round = 0; round < 20; round++)
    {
      for (int monkey = 0; monkey < monkeys.Length; monkey++)
      {
        int listCount = monkeys[monkey].Items.Count();

        for (int i = 0; i < listCount; i++ )
        {
          if (!monkeys[monkey].Items.Any())
          {
            continue;
          }
          else
          {
            monkeys[monkey].TotalInspections++;

            Int64 newWorryLevel = monkeys[monkey].WorryLevel / 3;
            bool test = newWorryLevel % monkeys[monkey].TestNumber == 0;

            if (test)
            {
              monkeys[monkeys[monkey].TrueMonkey].Items.Add(newWorryLevel);
              monkeys[monkey].Items.Remove(monkeys[monkey].Items.First());
            }
            else
            {
              monkeys[monkeys[monkey].FalseMonkey].Items.Add(newWorryLevel);
              monkeys[monkey].Items.Remove(monkeys[monkey].Items.First());
            }
          }
        }
      }
    }

    Monkey[] sortedMonkeys = monkeys.OrderByDescending(t => t.TotalInspections).ToArray();

    Console.WriteLine(sortedMonkeys[0].TotalInspections * sortedMonkeys[1].TotalInspections);
    Console.ReadLine();
  }

  private static void ParseInstructions(string[] _input, Monkey[] _monkeys)
  {
    for (int i = 0; i < 8; i++)
    {
      foreach (string instruction in _input)
      {
        string trimmedInstructions = instruction.TrimStart();
        string[] splitInstructions = trimmedInstructions.Split(" ");

        if (String.IsNullOrEmpty(instruction))
        {
          i++;
          continue;
        }
        else if (splitInstructions[0] == "Monkey")
        {
          continue;
        }
        else
        {
          switch (splitInstructions[0])
          {
            case "Starting":

              foreach (string line in splitInstructions)
              {
                string trimComma = line.Trim(',');

                int.TryParse(trimComma, out int number);

                _monkeys[i].Items.Add(number);
              }

              _monkeys[i].Items = _monkeys[i].Items.Where(x => x != 0).ToList();

              break;

            case "Operation:":

              _monkeys[i].OperationEquation = splitInstructions;

              break;

            case "Test:":

              foreach (string line in splitInstructions)
              {
                int.TryParse(line, out int number);

                _monkeys[i].TestNumber = number;
              }
              break;

            case "If":

              string trimmedLine = splitInstructions[1].Trim(':');

              if (trimmedLine == "true")
              {
                _monkeys[i].TrueMonkey = int.Parse(splitInstructions[5]);
              }
              else
              {
                _monkeys[i].FalseMonkey = int.Parse(splitInstructions[5]);
              }
              break;
          }
        }
      }
    }
  }

  public class Monkey
  {
    public List<Int64> Items { get; set; } = new List<Int64>();
    public string[] OperationEquation { get; set; } = new string[6];
    public Int64 WorryLevel => GetWorryLevel();
    public int TestNumber { get; set; }
    public int TrueMonkey { get; set; }
    public int FalseMonkey { get; set; }
    public int TotalInspections { get; set; } = 0;
    private Int64 OperationValue { get; set; }

    public int GetWorryLevel()
    {
      foreach (string line in OperationEquation)
      {
        bool success = int.TryParse(line, out int number);

        OperationValue = success ? number : Items.FirstOrDefault();
      }
      
      if (Items.Any())
      {
        if (OperationEquation[4] == "*")
        {
          return (int)(Items.First() * OperationValue);
        }
        else
        {
          return (int)(Items.First() + OperationValue);
        }
      }

      return 0;
    }
  }
}