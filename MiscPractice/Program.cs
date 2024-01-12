namespace MiscPractice
{
  public class Program
  {
    private static Dealer? _dealer;

    public static void Main(string[] args)
    {
      var game = new Game();
      _dealer = new Dealer(game);

      game.Deck = _dealer!.CreateDeck();
      game.StartNewGame();

      var tm = new TableMap();
      tm.DrawTable();

      Console.WriteLine("New Game created...");

      _dealer!.DistributePlayerMoney(20000);

      Console.WriteLine("Money Distributed...");

      tm.ReplacePlaceholders(game);

      Console.ReadLine();

      do
      {
        _dealer.CollectBlinds();
        _dealer.DealInitialCards();
        _dealer.RoundOfBets();


        foreach (var player in game.Players)
        {
          foreach (var card in player.HoleCards)
          {
            Console.WriteLine($"{player.Name} has the {card.CardNumber} of {card.CardSuit}");
          }

          Console.WriteLine($"{player.Name} has ${player.Money} dollars");
          Console.WriteLine();
        }

        foreach (var keys in game.Deck)
        {
          Console.WriteLine(keys.Key);
        }

      } while (game.Players.Count > 2);
    }
  }
}