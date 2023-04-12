public class Program
{
  public static void Main(string[] args)
  {
    string[] input = File.ReadAllLines("C:\\Users\\klittle\\source\\vscPractice\\AoC\\2022\\Day10\\csharp\\signal\\input.txt");
    int registerX = 1;
    int[] sprite = new int[3];
    int internalIndex = 0;

    UpdateSpritePosition(sprite, registerX);

    for (int cycle = 0; cycle < 240; cycle++)
    {
      string instructions = input[internalIndex];
      string[] splitInstructions = instructions.Split(" ");

      if (splitInstructions[0] == "addx")
      {
        PrintNextCharacter(cycle, sprite);

        cycle++;

        PrintNextCharacter(cycle, sprite);

        registerX += int.Parse(splitInstructions[1]);
        UpdateSpritePosition(sprite, registerX);
      }
      else
      {
        PrintNextCharacter(cycle, sprite);
      }

      internalIndex++;
    }

    Console.ReadLine();
  }

  private static void PrintNextCharacter(int _cycle, int[] _sprite)
  {
    if (_cycle % 40 == 0 && _cycle != 0)
    {
      if (_sprite.Contains(_cycle % 40))
      {
        Console.Write("\r\n#");
      }
      else
      {
        Console.Write("\r\n.");
      }
    }
    else if (_sprite.Contains(_cycle % 40))
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