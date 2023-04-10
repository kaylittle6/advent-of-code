public class Program
{
  public static void Main(string[] args)
  {
    string[] input = File.ReadAllLines("C:\\Users\\klittle\\source\\vscPractice\\AoC\\2022\\Day10\\csharp\\signal\\input.txt");
    int registerX = 1;
    int cycleCount = 0;
    int[] signalValues = new int[6];

    foreach (string line in input)
    {
      var splitLines = line.Split(" ");

      CheckMilestoneAndUpdate(cycleCount, registerX, signalValues);

      switch (splitLines[0])
      {
        case "noop":
          cycleCount++;
          break;

        case "addx":
          cycleCount++;

          CheckMilestoneAndUpdate(cycleCount, registerX, signalValues);

          cycleCount++;
          registerX += int.Parse(splitLines[1]);
          break;
      }
    }

    Console.WriteLine(signalValues.Sum());
    Console.ReadLine();
  }

  private static void CheckMilestoneAndUpdate(int _cycleCount, int _registerX, int[] _signalValues)
  {
    if (_cycleCount == 20 || _cycleCount == 60 || _cycleCount == 100
      || _cycleCount == 140 || _cycleCount == 180 || _cycleCount == 220)
    {
      switch (_cycleCount)
      {
        case 20:
          _signalValues[0] = _cycleCount * _registerX;
          break;

        case 60:
          _signalValues[1] = _cycleCount * _registerX;
          break;

        case 100:
          _signalValues[2] = _cycleCount * _registerX;
          break;

        case 140:
          _signalValues[3] = _cycleCount * _registerX;
          break;

        case 180:
          _signalValues[4] = _cycleCount * _registerX;
          break;

        case 220:
          _signalValues[5] = _cycleCount * _registerX;
          break;
      }
    }
  }

}