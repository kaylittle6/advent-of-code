namespace BJ
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var game = Game.GetGameClient;

      game.StartNewGame();
      game.Dealer.GetDeck(2);

      foreach (var card in game.Dealer.Deck)
      {
        Console.WriteLine(card.Display);
      }

      Console.ReadLine();
    }
  }
}