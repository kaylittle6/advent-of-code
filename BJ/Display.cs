namespace BJ
{
  public class Display
  {
    public void ShowTable(List<Player> players)
    {
      Console.Clear();

      foreach (var player in players)
      {
        if (player.Name == "Dealer")
        {
          Console.WriteLine($"{player.Name}");
        }
        else
        {
          Console.WriteLine($"{player.Name}  {player.CurrentMoney}  ");
        }
        
        foreach (var card in player.Cards)
        {
          Console.Write($"{card.Display}  ");
        }
        Console.WriteLine();
        Console.WriteLine();
      }
    }
  }
}
