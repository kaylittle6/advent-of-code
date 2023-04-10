public class Program
{
  public static void Main(string[] args)
  {
    string[] input = File.ReadAllLines("D:\\Programming\\repos\\aventOfCode\\AdventOfCode\\2022\\Day9\\csharp\\ropes\\input.txt");

    Rope headRope = new();
    Rope tailRope = new();

    foreach (string line in input)
    {
      string[] splitLines = line.Split(' ');

      tailRope.SaveCoordinates();
      MoveHeadRope(headRope, splitLines);
      
      if (TailRopeNeedsToMove(headRope, tailRope))
      {
        MoveTailRope(headRope, tailRope);
      }
    }

    List<int[]> distinctCoordinates = tailRope.Coordinates.Distinct(new CompareInts()).ToList();

    Console.WriteLine(distinctCoordinates.Count());
    Console.ReadLine();
  }
  
  private static void MoveHeadRope(Rope _headRope, string[] _splitLines)
  {
    switch (_splitLines[0])
    {
      case "R":
        _headRope.XIndex += int.Parse(_splitLines[1]);
        break;

      case "L":
        _headRope.XIndex -= int.Parse(_splitLines[1]);
        break;

      case "U":
        _headRope.YIndex += int.Parse(_splitLines[1]);
        break;

      case "D":
        _headRope.YIndex -= int.Parse(_splitLines[1]);
        break;
    }
  }

  private static bool TailRopeNeedsToMove(Rope _headRope, Rope _tailRope)
  {
    return (_headRope.XIndex - _tailRope.XIndex >= 2 || _headRope.XIndex - _tailRope.YIndex <= 2
      || _headRope.YIndex - _tailRope.YIndex >= 2 || _headRope.YIndex - _tailRope.YIndex <= -2);
  }

  private static void MoveTailRope(Rope _headRope, Rope _tailRope)
  {
    if (((_headRope.YIndex - _tailRope.YIndex >= 1 || _headRope.YIndex - _tailRope.YIndex <= -1)
      && (_headRope.XIndex - _tailRope.XIndex >= 2 || _headRope.XIndex - _tailRope.XIndex <= -2))
      || ((_headRope.YIndex - _tailRope.YIndex >= 2 || _headRope.YIndex - _tailRope.YIndex <= -2) 
      && (_headRope.XIndex - _tailRope.YIndex >= 1 || _headRope.XIndex - _tailRope.YIndex <= -1)))
    {
      if (_headRope.XIndex > _tailRope.XIndex && _headRope.YIndex > _tailRope.YIndex)
      {
        _tailRope.XIndex++;
        _tailRope.YIndex++;
      }

      else if (_headRope.XIndex > _tailRope.XIndex && _headRope.YIndex < _tailRope.XIndex)
      {
        _tailRope.XIndex++;
        _tailRope.YIndex--;
      }

      else if (_headRope.XIndex < _tailRope.XIndex && _headRope.YIndex > _tailRope.YIndex)
      {
        _tailRope.XIndex--;
        _tailRope.YIndex++;
      }

      else
      {
        _tailRope.XIndex--;
        _tailRope.YIndex--;
      }
    }

    else if (_headRope.XIndex - _tailRope.XIndex >= 2)
    {
      _tailRope.XIndex++;
    }

    else if (_headRope.XIndex - _tailRope.XIndex <= -2)
    {
      _tailRope.XIndex--;
    }

    else if (_headRope.YIndex - _tailRope.XIndex >= 2)
    {
      _tailRope.YIndex++;
    }

    else if (_headRope.YIndex - _tailRope.YIndex <= -2)
    {
      _tailRope.YIndex--;
    }
  }

  public class Rope
  {
    public int XIndex { get; set; } = 0;
    public int YIndex { get; set; } = 0;
    public List<int[]> Coordinates { get; set; } = new List<int[]>();

    public void SaveCoordinates()
    {
      int[] tempArray = { XIndex, YIndex };
      Coordinates.Add(tempArray);
    }
  }

  public class CompareInts : IEqualityComparer<int[]>
  {
    public bool Equals(int[]? _x, int[]? _y)
    {
      return _x?[0] == _y?[0] && _x?[1] == _y?[1];
    }

    public int GetHashCode(int[] obj)
    {
      return obj[0].GetHashCode() ^ obj[1].GetHashCode();
    }
  }
}