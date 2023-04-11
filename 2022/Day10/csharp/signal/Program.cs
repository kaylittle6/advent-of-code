public class Program
{
  public static void Main(string[] args)
  {
    string[] input = File.ReadAllLines("C:\\Users\\klittle\\source\\vscPractice\\AoC\\2022\\Day10\\csharp\\signal\\input.txt");
    int registerX = 1;
    int[] sprite = new int[3];
    int internalIndex = 0;
    int cursorPosition = 0;

    UpdateSpritePosition(sprite, registerX);

    for (int i = 0; i < 240; i++)
    {
      cursorPosition = cursorPosition > 39 ? 0 : cursorPosition;

      string instructions = input[internalIndex];
      string[] splitInstructions = instructions.Split(" ");

      if (splitInstructions[0] == "addx")
      {
        PrintNextCharacter(i, sprite);

        i++;

        PrintNextCharacter(i, sprite);

        registerX += int.Parse(splitInstructions[1]);
        UpdateSpritePosition(sprite, registerX);
      }
      else
      {
        PrintNextCharacter(i, sprite);
      }

      cursorPosition++;
      internalIndex++;
    }

    Console.ReadLine();
  }

  private static void PrintNextCharacter(int _index, int[] _sprite)
  {
    if (_index % 40 == 0 && _index != 0)
    {
      if (_sprite.Contains(_index))
      {
        Console.Write("\r\n#");
      }
      else
      {
        Console.Write("\r\n.");
      }
    }
    else if (_sprite.Contains(_index))
    {
      Console.Write("#");
    }
    else
    {
      Console.Write(".");
    }
  }

  private static void UpdateSpritePosition(int[] _sprite, int _registerX)
  {
    _sprite[1] = _registerX;
    _sprite[0] = _registerX - 1;
    _sprite[2] = _registerX + 1;
  }
}