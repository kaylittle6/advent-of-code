using System.Globalization;
using System.Reflection.Metadata.Ecma335;

public class Program
{
  public static void Main(string[] args)
  {
    string[] input = File.ReadAllLines("D:\\Programming\\repos\\aventOfCode\\trees\\trees\\input.txt");

    int[][] xindex = new int[99][];

    foreach (string line in input)
    {
      var splitLines = line.Split();

      for (int i = 0; i < input.Length; i++)
      {
        xindex[i] = new int[100];

        foreach (string number in splitLines)
        {
          xindex[i][i] = int.Parse(number);
        }
      }
    }


    Console.WriteLine();
    Console.ReadLine();



  }
}