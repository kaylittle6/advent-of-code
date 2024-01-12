namespace MiscPractice
{
  public class TableMap
  {
    string[] tableMap = File.ReadAllLines(
      "D:\\Programming\\repos\\advent-of-code\\MiscPractice\\table-map.txt");

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
        for (int j = 0; j < tableMap.Length; j++)
        {
          if (tableMap[j].Contains("{player" + i.ToString() + "}"))
          {
            tableMap[j] = tableMap[j].Replace("{player" + i.ToString() + "}", game.Players[i].Name);
          }
        }
      }

      DrawTable();
    }
  }
}
