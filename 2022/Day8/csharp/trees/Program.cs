public class Program
{
  public static void Main(string[] args)
  {
    string[] input = File.ReadAllLines("D:\\Programming\\repos\\aventOfCode\\AdventOfCode\\2022\\Day8\\csharp\\trees\\input.txt");
    int[][] index = SerializeInput(input);
    int totalVisible = 0;

    for (int i = 0; i < index.Length; i++)
    {
      for (int j = 0; j < index[0].Length; j++)
      {
        if (IsTreeVisible(index, i, j))
        {
          totalVisible += 1;
        }
      }
    }

    Console.WriteLine(totalVisible);
    Console.ReadLine();
  }

  private static int[][] SerializeInput(string[] _input)
  {
    int[][] index = new int[_input.Length][];
    char[] charArray;

    for (int i = 0; i < _input.Length; i++)
    {
      index[i] = new int[_input[i].Length];
    }

    for (int i = 0; i < _input.Length; i++)
    {
      charArray = _input[i].ToCharArray();

      for (int j = 0; j < charArray.Length; j++)
      {
        index[i][j] = int.Parse(charArray[j].ToString());
      }
    }

    return index;
  }

  private static bool IsTreeVisible(int[][] _index, int _yindex, int _xindex)
  {
    bool right = true;
    bool left = true;
    bool up = true;
    bool down = true;

    for (int i = _xindex + 1; i < _index[_xindex].Length; i++)
    {
      if (_index[_yindex][_xindex] <= _index[_yindex][i])
      {
        right = false;
      }
    }

    for (int i = _xindex - 1; i > -1; i--)
    {
      if (_index[_yindex][_xindex] <= _index[_yindex][i])
      {
        left = false;
      }
    }

    for (int i = _yindex - 1; i > -1; i--)
    {
      if (_index[_yindex][_xindex] <= _index[i][_xindex])
      {
        up = false;
      }
    }

    for (int i = _yindex + 1; i < _index[_yindex].Length; i++)
    {
      if (_index[_yindex][_xindex] <= _index[i][_xindex])
      {
        down = false;
      }
    }

    return right || left || up || down;
  }
}
