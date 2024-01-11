namespace MiscPractice
{
  public class TableMap
  {
    string[] tableMap = File.ReadAllLines(
      "C:\\Users\\klittle\\source\\advent-of-code\\MiscPractice\\table-map.txt");

    public void DrawTable()
    {
      Console.Clear();

      foreach (var chars in tableMap)
      {
        foreach (var symbols in chars)
        {
          Console.Write(symbols.ToString());
        }

        Console.WriteLine();
      }
    }

    public void ReplacePlaceholders(Game game)
    {
      for (int i = 0; i < game.Players.Count(); i++)
      {
        foreach (var line in tableMap)
        {
          if (line.Contains("player" + i.ToString()))
          {
            line.Replace("player" + i.ToString(), game.Players[i].Name);
          }
        }
      }
    }
  }
}
