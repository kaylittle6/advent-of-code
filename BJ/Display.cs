namespace BJ
{
  public class Display
  {
    public void ShowTable(Game game)
    {
      Console.Clear();

      foreach (var player in game.Players)
      {
        // Write Name
        Console.WriteLine($"Player: {player.Name}  ");

        // Write Money if not Dealer
        if (player.Name != "Dealer")
        {
          Console.WriteLine($"Money: {player.CurrentMoney}  ");
        }

        Console.WriteLine();

        // Write Cards
        foreach (var card in player.Cards)
        {
          Console.Write($"{card.Display}  ");
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
        Console.Write($"Total: {player.Cards.Sum(cv => cv.CardValue)}");


        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
      }
      Console.WriteLine($"Total Decks: {game.Dealer.DeckCount}");
      Console.WriteLine($"Cards Remaining: {game.Dealer.Deck.Count}");
      Console.WriteLine($"Minimum Bet: {game.MinimumBet}");
    }
  }
}
