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
        Console.WriteLine($"Bank: {player.Money:C2}");
        Console.WriteLine($"Current Bet:{player.CurrentBet:C2}\n");

        if (!player.IsNpc)
        {
          foreach (var card in player.Hand)
          {
            Console.WriteLine($"{card.Display}");
          }
        }
        else
        {
          Console.WriteLine("Card Hidden");
          Console.WriteLine("Card Hidden");
        }
        
        Console.WriteLine("----------\n\n");
      }
    }

    public static void ShowPlayerTable(Player player)
    {
      ShowGameDetails();

      Console.WriteLine("----------");
      Console.WriteLine($"{player.Name}");
      Console.WriteLine($"Bank: {player.Money:C2}");
      Console.WriteLine($"Current Bet:{player.CurrentBet:C2}\n");

      foreach (var card in player.Hand)
      {
        Console.WriteLine($"{card.Display}");
      }

      Console.WriteLine("----------\n\n");
    }

    private static void ShowGameDetails()
    {
      Console.Clear();
      Console.WriteLine($"Number of Players: {Game.Dealer.Table.Players.Count}    |--|   " +
                    $"Small/Big Blinds: {Game.Dealer.Table.SmallBlind:C2} / {Game.Dealer.Table.BigBlind:C2}\n");
    }
  }
}

