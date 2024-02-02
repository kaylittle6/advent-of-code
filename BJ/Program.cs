namespace BJ
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var game = Game.GetGameClient;

      game.StartNewGame(game.Dealer);
      game.Dealer.DealStartingCards(game.Players);
      game.Display.ShowTable(game);

      Console.ReadLine();
    }
  }
}