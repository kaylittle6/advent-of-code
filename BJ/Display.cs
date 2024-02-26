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

          for (var i = 0; i < player.Hand.Count; i++)
          {
            Console.WriteLine($"Hand {i + 1}: {player.Hand[i].CurrentBet:C2}");
          }
          
          Console.Write("Doubled Down: ");
          Console.Write(player.DoubledDown ? "Yes" : "No");
        }

        if (player.HasInsurance)
        {
          for (var i = 0; i < player.Hand.Count; i++)
          {
            Console.WriteLine($"Hand {i + 1} Insurance Bet: {player.Hand[i].CurrentBet / 2:C2}");
          }
        }

        Console.WriteLine();
        Console.WriteLine();
        
        foreach (var hand in player.Hand)
        {
          foreach (var card in hand.Cards)
          {
            if (player.IsDealer && !dealerFlipped && card == hand.Cards[0])
            {
              card.IsFaceDown = true;
              Console.WriteLine("[Card Face Down]");
              continue;
            }

            Console.Write($"{card.Display}\n");
          }
        }

        foreach (var card in player.Hand.Where(hand => hand.Cards.Sum(cv => cv.CardValue) > 21 
                  && hand.Cards.Any(c => c.CardNumber == "Ace"))
                  .SelectMany(hand => hand.Cards
                  .Where(c => c.CardNumber == "Ace")))
        {
          card.ChangeAceValueToOne();
        }

        for (var i = 0; i < player.Hand.Count; i++)
        {
          Console.WriteLine($"Hand {i + 1} Total: {player.Hand[i].Value}");
        }
        
        Console.WriteLine("------------------------");
        Console.WriteLine();
      }

      Console.WriteLine("------------------------");
      Console.WriteLine($"Total Decks: {game.Dealer.DeckCount}");
      Console.WriteLine($"Cards Remaining: {game.Dealer.Deck.Count}");
      Console.WriteLine($"Minimum Bet: {game.MinimumBet:C2}");
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
          
          for (var i = 0; i < p.Hand.Count; i++)
          {
            Console.WriteLine($"Hand {i + 1}: {p.Hand[i].CurrentBet:C2}");
          }

          Console.Write("Doubled Down: ");
          Console.Write(player.DoubledDown ? "Yes" : "No");
          Console.WriteLine();
        }

        if (p is { HasInsurance: true, IsDealer: false })
        {
          for (var i = 0; i < p.Hand.Count; i++)
          {
            Console.WriteLine($"Hand {i + 1} Insurance Bet: {player.Hand[i].CurrentBet / 2:C2}");
          }
        }
        
        Console.WriteLine();
        Console.WriteLine();

        foreach (var hand in p.Hand)
        {
          foreach (var card in hand.Cards)
          {
            if (p.IsDealer && !dealerFlipped && card == hand.Cards[0])
            {
              card.IsFaceDown = true;
              Console.WriteLine("[Card Face Down]");
              continue;
            }

            Console.Write($"{card.Display}");
          }
        }

        foreach (var card in player.Hand.Where(hand => hand.Cards.Sum(cv => cv.CardValue) > 21
                  && hand.Cards.Any(c => c.CardNumber == "Ace"))
                  .SelectMany(hand => hand.Cards
                  .Where(c => c.CardNumber == "Ace")))
        {
          card.ChangeAceValueToOne();
        }

        for (var i = 0; i < p.Hand.Count; i++)
        {
          Console.WriteLine($"Hand {i + 1} Total: {player.Hand[i].Value}");
        }
        
        Console.WriteLine("------------------------");
        Console.WriteLine();
      }
    }

    public static void ShowDealersActions(Dealer dealer)
    {
      Console.WriteLine("------------------------");
      Console.WriteLine($"{dealer.Name}");
      Console.WriteLine();

      foreach (var card in dealer.Hand[0].Cards)
      {
        Console.WriteLine($"{card.Display}");
      }
      
      Console.WriteLine($"Total: {dealer.Hand[0].Value}");
      Console.WriteLine("------------------------");
    }
  }
}
