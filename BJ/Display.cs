namespace BJ
{
  public class Display
  {
    // Overload method to show table for all Players
    public void ShowTable(bool dealerFlipped)
    {
      Game game = Game.GetGameClient;

      Console.Clear();

      foreach (var player in game.Players)
      {
        if (player.InHand)
        {
          // Write Name
          Console.WriteLine("------------------------");
          Console.WriteLine($"Player: {player.Name}");
          Console.WriteLine();

          // Write Money & Current Bet if not Dealer
          if (!player.IsDealer)
          {
            Console.WriteLine($"Money: {player.CurrentMoney.ToString("C2")}");
            Console.WriteLine($"Current Bet: {player.CurrentBet.ToString("C2")}");

            Console.Write("Doubled Down: ");

            if (player.DoubledDown)
            {
              Console.Write("Yes");
            }
            else
            {
              Console.Write("No");
            }
          }

          if (player.HasInsurance)
          {
            Console.WriteLine($"Insurance Bet: {player.CurrentBet / 2}");
          }

          Console.WriteLine();
          Console.WriteLine();

          // Write Cards & Hide Dealer Card
          foreach (var card in player.Cards)
          {
            if (player.IsDealer && !dealerFlipped && card == player.Cards[0])
            {
              card.IsFaceDown = true;

              Console.WriteLine("[Card Face Down]");

              continue;
            }
            Console.Write($"{card.Display}");
            Console.WriteLine();
          }

          // Change Ace value if Player's hand > 21
          if (player.Cards.Sum(cv => cv.CardValue) > 21 && player.Cards.Any(c => c.CardNumber == "Ace"))
          {
            foreach (var card in player.Cards.Where(c => c.CardNumber == "Ace"))
            {
              card.ChangeAceValueToOne();
            }
          }

          // Write total hand value
          Console.WriteLine($"Total: {player.Cards.Where(c => !c.IsFaceDown).Sum(cv => cv.CardValue)}");
          Console.WriteLine("------------------------");

          Console.WriteLine();
          Console.WriteLine();
        }
      }
      Console.WriteLine("------------------------");
      Console.WriteLine($"Total Decks: {game.Dealer.DeckCount}");
      Console.WriteLine($"Cards Remaining: {game.Dealer.Deck.Count}");
      Console.WriteLine($"Minimum Bet: {game.Dealer.MinimumBet.ToString("C2")}");
      Console.WriteLine("------------------------");
    }

    // Overload method to show table for a Player & Dealer
    public void ShowTable(Player player, bool dealerFlipped)
    {
      Game game = Game.GetGameClient;

      Console.Clear(); 

      Player[] pAD = { game.Dealer, player };

      for (int i = 0; i < pAD.Length; i++)
      {
        Console.WriteLine("------------------------");
        Console.WriteLine($"Player: {pAD[i].Name}");

        if (!pAD[i].IsDealer)
        {
          Console.WriteLine($"Money: {pAD[i].CurrentMoney.ToString("C2")}");
          Console.WriteLine($"Current Bet: {player.CurrentBet.ToString("C2")}");
          Console.WriteLine();
        }

        if (player.HasInsurance)
        {
          Console.WriteLine($"Insurance Bet: {player.CurrentBet / 2}");
        }

        Console.WriteLine("Doubled Down: ");

        if (player.DoubledDown)
        {
          Console.Write("Yes");
        }
        else
        {
          Console.Write("No");
        }

        foreach (var card in pAD[i].Cards)
        {
          if (pAD[i].IsDealer && !dealerFlipped && card == pAD[i].Cards[0])
          {
            card.IsFaceDown = true;

            Console.WriteLine();
            Console.WriteLine("[Card Face Down]");

            continue;
          }
          Console.Write($"{card.Display}");
          Console.WriteLine();
        }

        if (pAD[i].Cards.Sum(cv => cv.CardValue) > 21 && pAD[i].Cards.Any(c => c.CardNumber == "Ace"))
        {
          foreach (var card in player.Cards.Where(c => c.CardNumber == "Ace"))
          {
            card.ChangeAceValueToOne();
          }
        }

        Console.WriteLine($"Total: {pAD[i].Cards.Where(c => !c.IsFaceDown).Sum(cv => cv.CardValue)}");
        Console.WriteLine("------------------------");

        Console.WriteLine();
      }
    }
  }
}
