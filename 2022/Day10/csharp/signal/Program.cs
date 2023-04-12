public class Program
{
  public static void Main(string[] args)
  {
    string[] input = File.ReadAllLines("C:\\Users\\klittle\\source\\vscPractice\\AoC\\2022\\Day10\\csharp\\signal\\input.txt");
    int registerX = 1;
    int[] sprite = new int[3];
    int internalIndex = 0;

    UpdateSpritePosition(sprite, registerX);

    for (int i = 0; i < 240; i++)
    {
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

      internalIndex++;
    }

    Console.ReadLine();
  }

  private static void PrintNextCharacter(int i, int[] _sprite)
  {
    if (i % 40 == 0 && i != 0)
    {
      if (_sprite.Contains(i % 40))
      {
        Console.Write("\r\n#");
      }
      else
      {
        Console.Write("\r\n.");
      }
    }
    else if (_sprite.Contains(i % 40))
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