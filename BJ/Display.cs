namespace BJ
{
  public static class Display
  {
    // Overload method to show table for all Players
    public static void ShowTable(bool dealerFlipped)
    {
      var game = Game.GetGameClient;

      Console.Clear();

      foreach (var player in game.Players.Where(player => player.InHand))
      {
        Console.WriteLine("------------------------");
        Console.WriteLine($"Player: {player.Name}");
        Console.WriteLine();

        if (!player.IsDealer)
        {
          Console.WriteLine($"Money: {player.CurrentMoney:C2}");
          Console.WriteLine($"Current Bet: {player.CurrentBet:C2}");
          Console.Write("Doubled Down: ");
          Console.Write(player.DoubledDown ? "Yes" : "No");
        }

        if (player.HasInsurance)
        {
          Console.WriteLine($"Insurance Bet: {player.CurrentBet / 2:C2}");
        }

        Console.WriteLine();
        Console.WriteLine();

        foreach (var card in player.Cards)
        {
          if (player.IsDealer && !dealerFlipped && card == player.Cards[0])
          {
            card.IsFaceDown = true;
            Console.WriteLine("[Card Face Down]");
            continue;
          }

          Console.Write($"{card.Display}\n");
        }

        if (player.Cards.Sum(cv => cv.CardValue) > 21 && player.Cards.Any(c => c.CardNumber == "Ace"))
        {
          foreach (var card in player.Cards.Where(c => c.CardNumber == "Ace"))
          {
            card.ChangeAceValueToOne();
          }
        }

        Console.WriteLine($"Total: {player.HandValue}");
        Console.WriteLine("------------------------");
        Console.WriteLine();
      }

      Console.WriteLine("------------------------");
      Console.WriteLine($"Total Decks: {game.Dealer.DeckCount}");
      Console.WriteLine($"Cards Remaining: {game.Dealer.Deck.Count}");
      Console.WriteLine($"Minimum Bet: {game.Dealer.MinimumBet:C2}");
      Console.WriteLine("------------------------");
    }

    // Overload method to show table for a Player & Dealer
    public static void ShowTable(Player player, bool dealerFlipped)
    {
      var game = Game.GetGameClient;

      Console.Clear();

      foreach (var p in new[] { game.Dealer, player })
      {
        Console.WriteLine("------------------------");
        Console.WriteLine($"Player: {p.Name}");

        if (!p.IsDealer)
        {
          Console.WriteLine($"Money: {p.CurrentMoney:C2}");
          Console.WriteLine($"Current Bet: {player.CurrentBet:C2}");
          Console.Write("Doubled Down: ");
          Console.Write(player.DoubledDown ? "Yes" : "No");
          Console.WriteLine();
        }

        if (p is { HasInsurance: true, IsDealer: false })
        {
          Console.WriteLine($"Insurance Bet: {player.CurrentBet / 2:C2}");
        }
        
        Console.WriteLine();
        
        foreach (var card in p.Cards)
        {
          if (p.IsDealer && !dealerFlipped && card == p.Cards[0])
          {
            card.IsFaceDown = true;

            Console.WriteLine();
            Console.WriteLine("[Card Face Down]");

            continue;
          }

          Console.Write($"{card.Display}");
          Console.WriteLine();
        }

        if (p.HandValue > 21 && p.Cards.Any(c => c.CardValue == 11))
        {
          foreach (var card in player.Cards.Where(c => c.CardNumber == "Ace"))
          {
            card.ChangeAceValueToOne();
          }
        }

        Console.WriteLine($"Total: {p.HandValue}");
        Console.WriteLine("------------------------");

        Console.WriteLine();
      }
    }

    public static void ShowDealersActions(Dealer dealer)
    {
      Console.WriteLine("------------------------");
      Console.WriteLine($"{dealer.Name}");
      Console.WriteLine();

      foreach (var card in dealer.Cards)
      {
        Console.WriteLine($"{card.Display}");
      }
      
      Console.WriteLine($"Total: {dealer.HandValue}");
      
      Console.WriteLine("------------------------");
    }
  }
}
