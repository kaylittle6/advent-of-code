public class Program
{
  public static void Main(string[] args)
  {
    List<Directory> masterList = new List<Directory>();
    List<Directory> spaceNeededList = new List<Directory>();
    Directory activeDirectory = new("");

    string[] input = File.ReadAllLines("D:\\Programming\\repos\\aventOfCode\\AdventOfCode\\2022\\Day7\\csharp\\data\\input.txt");
    const int spaceNeeded = 4376732;

    foreach (string line in input)
    {
      string[] splitLines = line.Split(" ");

      //Case: if '$ cd'
      if (splitLines[0] + splitLines[1] == "$" + "cd" && splitLines[2] != "..")
      {
        //Case: for root Directory '$ cd /'
        if (line == "$ cd /")
        {
          activeDirectory = new Directory(splitLines[2]);
          masterList.Add(activeDirectory);
        }

        //Case: changing to a named directory
        else if (splitLines[2] != "/")
        {
          activeDirectory = masterList.FindLast(n => n.Name == splitLines[2]) ?? throw new InvalidOperationException();
        }
      }

      //Case: new Directory
      else if (splitLines[0] == "dir")
      {
        Directory newDirectory = new(splitLines[1]);
        activeDirectory.SubDirectories.Add(newDirectory);
        masterList.Add(newDirectory);
      }

      //Case: for Files
      else if (int.TryParse(splitLines[0], out int number))
      {
        activeDirectory.Files.Add(number);
      }
    }

    foreach (Directory directory in masterList)
    {
      if (directory.TotalDirectoryDataSize >= spaceNeeded)
      {
        spaceNeededList.Add(directory);
      }
    }

    Console.WriteLine(spaceNeededList.Min(d => d.TotalDirectoryDataSize));
    Console.ReadLine();
  }

  public class Directory
  {
    public string Name { get; set; }
    public List<int> Files { get; set; } = new List<int>();
    public List<Directory> SubDirectories { get; set; } = new List<Directory>();
    public int FilesTotalData => Files.Any() ? Files.Sum() : 0;
    public int SubFilesTotalData => SubDirectories.Sum(d => d.TotalDirectoryDataSize);
    public int TotalDirectoryDataSize => FilesTotalData + SubFilesTotalData;
    public bool UnderDataCap => TotalDirectoryDataSize <= 100000;

    public Directory(string _name)
    {
      Name = _name;
    }
  }
}