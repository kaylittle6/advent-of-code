using System.Globalization;

public class Program
{
  public static void Main(string[] args)
  {
    string[] input = File.ReadAllLines("C:\\Users\\klittle\\source\\vscPractice\\AoC\\2022\\Day9\\csharp\\ropes\\input.txt");

    Rope[] ropes = new Rope[10];

    for (int i = 0; i < ropes.Length; i++)
    {
      ropes[i] = new(i.ToString());
    }

    ropes[9].ID = "Tail";
    ropes[9].SaveCoordinates();

    foreach (string line in input)
    {
      string[] splitLines = line.Split(" ");

      MoveHeadRope(ropes, splitLines);
    }

    List<int[]> distinctCoordinates = ropes[9].Coordinates.Distinct(new CompareInts()).ToList();

    Console.WriteLine(distinctCoordinates.Count());
    Console.ReadLine();
  }
  
  private static void MoveHeadRope(Rope[] _ropes, string[] _splitLines)
  {
    for (int i = 0; i < int.Parse(_splitLines[1]); i++)
    {
      switch (_splitLines[0])
      {
        case "R":
          _ropes[0].XIndex++;
          CheckAndMove(_ropes, _splitLines);
          break;

        case "L":
          _ropes[0].XIndex--;
          CheckAndMove(_ropes, _splitLines);
          break;

        case "U":
          _ropes[0].YIndex++;
          CheckAndMove(_ropes, _splitLines);
          break;

        case "D":
          _ropes[0].YIndex--;
          CheckAndMove(_ropes, _splitLines);
          break;
      }
    }
  }

  private static void CheckAndMove(Rope[] _ropes, string[] _splitLines)
  {
    for (int i = 1; i < _ropes.Length; i++)
    {
      if (NextRopeNeedsToMove(_ropes[i - 1], _ropes[i]))
      {
        MoveNextRope(_ropes[i - 1], _ropes[i]);
      }
    }
  }

  private static bool NextRopeNeedsToMove(Rope _previousRope, Rope _nextRope)
  {
    return (Math.Abs(_previousRope.XIndex - _nextRope.XIndex) >= 2)
      || (Math.Abs(_previousRope.YIndex - _nextRope.YIndex) >= 2);
  }

  private static void MoveNextRope(Rope _previousRope, Rope _nextRope)
  {
    if (((Math.Abs(_previousRope.YIndex - _nextRope.YIndex) >= 1) && (Math.Abs(_previousRope.XIndex - _nextRope.XIndex) >= 2)) 
      || ((Math.Abs(_previousRope.YIndex - _nextRope.YIndex) >= 2) && (Math.Abs(_previousRope.XIndex - _nextRope.XIndex) >= 1)))
    {
      if (_previousRope.XIndex > _nextRope.XIndex && _previousRope.YIndex > _nextRope.YIndex)
      {
        _nextRope.XIndex++;
        _nextRope.YIndex++;
      }

      else if (_previousRope.XIndex > _nextRope.XIndex && _previousRope.YIndex < _nextRope.YIndex)
      {
        _nextRope.XIndex++;
        _nextRope.YIndex--;
      }

      else if (_previousRope.XIndex < _nextRope.XIndex && _previousRope.YIndex > _nextRope.YIndex)
      {
        _nextRope.XIndex--;
        _nextRope.YIndex++;
      }

      else if (_previousRope.XIndex < _nextRope.XIndex && _previousRope.YIndex < _nextRope.YIndex)
      {
        _nextRope.XIndex--;
        _nextRope.YIndex--;
      }
    }

    else if (_previousRope.XIndex - _nextRope.XIndex >= 2)
    {
      _nextRope.XIndex++;
    }

    else if (_previousRope.XIndex - _nextRope.XIndex <= -2)
    {
      _nextRope.XIndex--;
    }

    else if (_previousRope.YIndex - _nextRope.YIndex >= 2)
    {
      _nextRope.YIndex++;
    }

    else if (_previousRope.YIndex - _nextRope.YIndex <= -2)
    {
      _nextRope.YIndex--;
    }

    if (_nextRope.ID == "Tail")
    {
      _nextRope.SaveCoordinates();
    }
  }

  public class Rope
  {
    public string ID { get; set; }
    public int XIndex { get; set; } = 0;
    public int YIndex { get; set; } = 0;
    public List<int[]> Coordinates { get; set; } = new List<int[]>();

    public Rope(string _id)
    {
      ID = _id;
    }

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