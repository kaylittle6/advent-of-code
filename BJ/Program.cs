namespace BJ
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var game = Game.GetGameClient;

      game.StartNewGame();
      game.Dealer.GetDeck(2);
      game.Dealer.DealCards(game.Players);
      game.Display.ShowTable(game.Players);

      Console.ReadLine();
    }
  }
}