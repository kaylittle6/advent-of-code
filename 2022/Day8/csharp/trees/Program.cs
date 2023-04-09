public class Program
{
  public static void Main(string[] args)
  {
    string[] input = File.ReadAllLines("D:\\Programming\\repos\\aventOfCode\\AdventOfCode\\2022\\Day8\\csharp\\trees\\input.txt");
    int[][] index = SerializeInput(input);
    int bestScenicScore = 0;

    for (int i = 0; i < index.Length; i++)
    {
      for (int j = 0; j < index[0].Length; j++)
      {
        int tempScore = GetScenicScore(index, i, j);
        
        if (tempScore > bestScenicScore)
        {
          bestScenicScore = tempScore;
        }
      }
    }

    Console.WriteLine(bestScenicScore);
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

  private static int GetScenicScore(int[][] _index, int _yindex, int _xindex)
  {
    int right = 0;
    int left = 0;
    int up = 0;
    int down = 0;

    for (int i = _xindex + 1; i < _index[_xindex].Length; i++)
    {
      if (_index[_yindex][_xindex] > _index[_yindex][i])
      {
        right++;
      }
      else if (_index[_yindex][_xindex] <= _index[_yindex][i])
      {
        right++;
        break;
      }
    }

    for (int i = _xindex - 1; i > -1; i--)
    {
      if (_index[_yindex][_xindex] > _index[_yindex][i])
      {
        left++;
      }
      else if (_index[_yindex][_xindex] <= _index[_yindex][i])
      {
        left++;
        break;
      }
    }

    for (int i = _yindex - 1; i > -1; i--)
    {
      if (_index[_yindex][_xindex] > _index[i][_xindex])
      {
        up++;
      }
      else if (_index[_yindex][_xindex] <= _index[i][_xindex])
      {
        up++;
        break;
      }
    }

    for (int i = _yindex + 1; i < _index[_yindex].Length; i++)
    {
      if (_index[_yindex][_xindex] > _index[i][_xindex])
      {
        down++;
      }
      else if (_index[_yindex][_xindex] <= _index[i][_xindex])
      {
        down++;
        break;
      }
    }

    return right * left * up * down;
  }
}
