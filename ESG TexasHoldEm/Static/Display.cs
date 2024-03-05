using TexasHoldEm.Models;

namespace TexasHoldEm.Static
{
  public static class Display
  {
    private static readonly Game Game = Game.GetGameClient;
    
    public static void ShowEntireTable()
    {
      ShowGameDetails();
      
      foreach(var player in Game.Dealer.Table.Players.Where(p => p.InHand))
      {
        Console.WriteLine("----------");
        Console.WriteLine($"{player.Name}");
        Console.WriteLine($"{player.Money:C2}\n");

        foreach (var card in player.Hand)
        {
          Console.WriteLine($"{card.Display}");
        }

        Console.WriteLine();
        Console.WriteLine("----------");
      }
    }

    public static void ShowPlayerTable(Player player)
    {
      ShowGameDetails();

      Console.WriteLine("----------");
      Console.WriteLine($"{player.Name}");
      Console.WriteLine($"{player.Money:C2}\n");

      foreach (var card in player.Hand)
      {
        Console.WriteLine($"{card.Display}");
      }

      Console.WriteLine();
      Console.WriteLine("----------");
    }

    private static void ShowGameDetails()
    {
      Console.Clear();
      Console.WriteLine($"Number of Players: {Game.Dealer.Table.Players.Count} --" +
                    $"Small/Big Blinds: {Game.Dealer.Table.SmallBlind:C2} / {Game.Dealer.Table.BigBlind:C2}\n");
    }
  }
}

