using System.Linq;

public class Program
{
  public static void Main(string[] args)
  {
    string[] input = File.ReadAllLines("C:\\Users\\klittle\\source\\vscPractice\\AoC\\2022\\Day11\\csharp\\monkey\\input.txt");

    Monkey[] monkeys = new Monkey[8];

    ParseInstructions(input, monkeys);



  }

  private static void ParseInstructions(string[] _input, Monkey[] _monkeys)
  {
    for (int i = 0; i < 9; i++)
    {
      foreach (string instruction in _input)
      {
        string[] splitInstructions = instruction.Split(" ");

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
                int.TryParse(line, out int number);

                _monkeys[i].Items.Add(number);
              }
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

              foreach (string line in splitInstructions)
              {
                int.TryParse(line, out int number);

                _monkeys[i].TrueMonkey = splitInstructions[1] == "true" ? number : 0;
                _monkeys[i].FalseMonkey = splitInstructions[1] == "false" ? number : 0;
              }
              break;
          }
        }

        i++;
      }
    }
  }




  public class Monkey
  {
    public List<int> Items { get; set; } = new List<int>();
    public string[] OperationEquation { get; set; } = new string[6];
    public int OperationValue { get; set; }
    public int WorryLevel => GetWorryLevel();
    public int TestNumber { get; set; }
    public int TrueMonkey { get; set; }
    public int FalseMonkey { get; set; }

    private int GetWorryLevel()
    {
      foreach (string line in OperationEquation)
      {
        bool success = int.TryParse(line, out int number);

        OperationValue = success ? number : Items.FirstOrDefault();
      }
      
      if (Items.Any() && Items.First() != 0)
      {
        if (OperationEquation[4] == "*")
        {
          return Items.First() * OperationValue;
        }
        else
        {
          return Items.First() + OperationValue;
        }
      }

      return 0;
    }
  }
}