public class Program
{
  public static void Main(string[] args)
  {
    string[] input = File.ReadAllLines("C:/Users/klittle/source/vscPractice/AoC/items/items/items.Data/input.txt");
    int totalPriority = 0;
    int groupCap = 3;

    Logic logic = new();
    List<string> groupList = new();

    for (int i = 0; i < input.Length; i += groupCap)
    {
      for (int j = 0; j < groupCap && i + j < input.Length; j++)
      {
        groupList.Add(input[i + j]);
      }

      string[] listArray = groupList.ToArray();

      char commonLetter = logic.GetCommonLetter(listArray);

      totalPriority += logic.GetPriorityValue(commonLetter);

      groupList.Clear();
    }

    Console.WriteLine(totalPriority);
    Console.ReadLine();
  }

  public class Logic
  {
    public string[] SplitLines(string _line)
    {
      int stringLength = _line.Length;
      int halfString = stringLength / 2;

      string firstString = _line.Substring(0, halfString);
      string secondString = _line.Substring(halfString);

      string[] stringParts = { firstString, secondString };

      return stringParts;
    }

    public char GetCommonLetter(string[] _strings)
    {
      foreach (char letter in _strings[0])
      {
        if (_strings[1].Contains(letter) && _strings[2].Contains(letter))
        {
          return letter;
        }
      }

      return 'Z';
    }

    public int GetPriorityValue(char _commonLetter)
    {
      int numberValue = 0;

      switch (_commonLetter)
      {
        case 'a':
          numberValue = 1;
          break;
        case 'b':
          numberValue = 2;
          break;
        case 'c':
          numberValue = 3;
          break;
        case 'd':
          numberValue = 4;
          break;
        case 'e':
          numberValue = 5;
          break;
        case 'f':
          numberValue = 6;
          break;
        case 'g':
          numberValue = 7;
          break;
        case 'h':
          numberValue = 8;
          break;
        case 'i':
          numberValue = 9;
          break;
        case 'j':
          numberValue = 10;
          break;
        case 'k':
          numberValue = 11;
          break;
        case 'l':
          numberValue = 12;
          break;
        case 'm':
          numberValue = 13;
          break;
        case 'n':
          numberValue = 14;
          break;
        case 'o':
          numberValue = 15;
          break;
        case 'p':
          numberValue = 16;
          break;
        case 'q':
          numberValue = 17;
          break;
        case 'r':
          numberValue = 18;
          break;
        case 's':
          numberValue = 19;
          break;
        case 't':
          numberValue = 20;
          break;
        case 'u':
          numberValue = 21;
          break;
        case 'v':
          numberValue = 22;
          break;
        case 'w':
          numberValue = 23;
          break;
        case 'x':
          numberValue = 24;
          break;
        case 'y':
          numberValue = 25;
          break;
        case 'z':
          numberValue = 26;
          break;
        case 'A':
          numberValue = 27;
          break;
        case 'B':
          numberValue = 28;
          break;
        case 'C':
          numberValue = 29;
          break;
        case 'D':
          numberValue = 30;
          break;
        case 'E':
          numberValue = 31;
          break;
        case 'F':
          numberValue = 32;
          break;
        case 'G':
          numberValue = 33;
          break;
        case 'H':
          numberValue = 34;
          break;
        case 'I':
          numberValue = 35;
          break;
        case 'J':
          numberValue = 36;
          break;
        case 'K':
          numberValue = 37;
          break;
        case 'L':
          numberValue = 38;
          break;
        case 'M':
          numberValue = 39;
          break;
        case 'N':
          numberValue = 40;
          break;
        case 'O':
          numberValue = 41;
          break;
        case 'P':
          numberValue = 42;
          break;
        case 'Q':
          numberValue = 43;
          break;
        case 'R':
          numberValue = 44;
          break;
        case 'S':
          numberValue = 45;
          break;
        case 'T':
          numberValue = 46;
          break;
        case 'U':
          numberValue = 47;
          break;
        case 'V':
          numberValue = 48;
          break;
        case 'W':
          numberValue = 49;
          break;
        case 'X':
          numberValue = 50;
          break;
        case 'Y':
          numberValue = 51;
          break;
        case 'Z':
          numberValue = 52;
          break;
      }

      return numberValue;
    }
  }
}