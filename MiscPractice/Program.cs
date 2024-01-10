namespace MiscPractice
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var game = new Game();

      game.StartNewGame();

      Console.WriteLine("New Game created...");

      game.Dealer.DistributePlayerMoney(game, 20000);

      Console.WriteLine("Money Distributed...");

      do
      {
        game.Dealer.CollectBlinds(game);
        game.Dealer.DealInitialCards(game);
        game.Dealer.RoundOfBets(game);


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