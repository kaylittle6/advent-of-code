public class Program
{
  public static void Main(string[] args)
  {
    string[] input = File.ReadAllLines("C:\\Users\\klittle\\source\\vscPractice\\AoC\\2022\\Day9\\csharp\\ropes\\input.txt");

    Rope headRope = new();
    Rope tailRope = new();

    tailRope.SaveCoordinates();

    foreach (string line in input)
    {
      string[] splitLines = line.Split(' ');

      MoveHeadRope(headRope, tailRope, splitLines);
    }

    List<int[]> distinctCoordinates = tailRope.Coordinates.Distinct(new CompareInts()).ToList();

    Console.WriteLine(distinctCoordinates.Count());
    Console.ReadLine();
  }
  
  private static void MoveHeadRope(Rope _headRope, Rope _tailRope, string[] _splitLines)
  {
    for (int i = 0; i < int.Parse(_splitLines[1]); i++)
    {
      switch (_splitLines[0])
      {
        case "R":
          _headRope.XIndex++;
          if (TailRopeNeedsToMove(_headRope, _tailRope))
          {
            MoveTailRope(_headRope, _tailRope);
          }
          break;

        case "L":
          _headRope.XIndex--;
          if (TailRopeNeedsToMove(_headRope, _tailRope))
          {
            MoveTailRope(_headRope, _tailRope);
          }
          break;

        case "U":
          _headRope.YIndex++;
          if (TailRopeNeedsToMove(_headRope, _tailRope))
          {
            MoveTailRope(_headRope, _tailRope);
          }
          break;

        case "D":
          _headRope.YIndex--;
          if (TailRopeNeedsToMove(_headRope, _tailRope))
          {
            MoveTailRope(_headRope, _tailRope);
          }
          break;
      }
    }
  }

  private static bool TailRopeNeedsToMove(Rope _headRope, Rope _tailRope)
  {
    return (Math.Abs(_headRope.XIndex - _tailRope.XIndex) >= 2)
      || (Math.Abs(_headRope.YIndex - _tailRope.YIndex) >= 2);
  }

  private static void MoveTailRope(Rope _headRope, Rope _tailRope)
  {
    if (((Math.Abs(_headRope.YIndex - _tailRope.YIndex) >= 1) && (Math.Abs(_headRope.XIndex - _tailRope.XIndex) >= 2)) 
      || ((Math.Abs(_headRope.YIndex - _tailRope.YIndex) >= 2) && (Math.Abs(_headRope.XIndex - _tailRope.XIndex) >= 1)))
    {
      if (_headRope.XIndex > _tailRope.XIndex && _headRope.YIndex > _tailRope.YIndex)
      {
        _tailRope.XIndex++;
        _tailRope.YIndex++;
      }

      else if (_headRope.XIndex > _tailRope.XIndex && _headRope.YIndex < _tailRope.YIndex)
      {
        _tailRope.XIndex++;
        _tailRope.YIndex--;
      }

      else if (_headRope.XIndex < _tailRope.XIndex && _headRope.YIndex > _tailRope.YIndex)
      {
        _tailRope.XIndex--;
        _tailRope.YIndex++;
      }

      else if (_headRope.XIndex < _tailRope.XIndex && _headRope.YIndex < _tailRope.YIndex)
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

    else if (_headRope.YIndex - _tailRope.YIndex >= 2)
    {
      _tailRope.YIndex++;
    }

    else if (_headRope.YIndex - _tailRope.YIndex <= -2)
    {
      _tailRope.YIndex--;
    }

    _tailRope.SaveCoordinates();
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