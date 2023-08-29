public class SecondAttempt
{
  public static void Main(string[] args)
  {
    string[] input = File.ReadAllLines("C:\\Users\\klittle\\source\\vscPractice\\AoC\\2022\\Day11\\csharp\\monkey\\input.txt");

    var monkeyList = ParseInput(input);

    Console.ReadLine();

  }

  public static List<Monkey> ParseInput(string[] input)
  {
    List<Monkey> monkeyList = new(8);
    
    for (int i = 0; i < monkeyList.Count; i++)
    {
      foreach (string line in input)
      {
        if (String.IsNullOrEmpty(line))
        {
          i++;
          continue;
        }

        var splitLine = line.Split();

        switch (splitLine[0])
        {
          case "Monkey":
            monkeyList[i].MonkeyNumber = splitLine[1];
            break;

          case "Starting":
            foreach (string editedLine in splitLine)
            {
              var newString = editedLine.Replace(",", "");

              bool number = int.TryParse(editedLine, out int result);

              if (number)
              {
                monkeyList[i].Items?.Add(result);
              }
            }
            break;

          case "Operation:":
            monkeyList[i].OperationOperator = splitLine[5];
            monkeyList[i].OperationValue = int.Parse(splitLine[6]);
            break;

          case "Test:":
            monkeyList[i].Test = int.Parse(splitLine[4]);
            break;

          case "If":
            if (splitLine[1] == "true")
            {
              monkeyList[i].TrueTestResult = int.Parse(splitLine[6]);
            }
            else
            {
              monkeyList[i].FalseTestResult = int.Parse(splitLine[6]);
            }
            break;
        }
      }
    }

    return monkeyList;
  }

  public class Monkey
  {
    public string MonkeyNumber { get; set; } = "";
    public List<int>? Items { get; set; }
    public string OperationOperator { get; set; } = "";
    public int OperationValue { get; set; }
    public int Test { get; set; }
    public int TrueTestResult { get; set; }
    public int FalseTestResult { get; set; }
    public int TimesInspected { get; set; }
  }
}