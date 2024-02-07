namespace BJ
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var game = Game.GetGameClient;
      var gameOn = true;

      game.LetsPlayBlackJack(game);

      game.Dealer.CollectAntes(game);

      //do
      //{
      //  game.CommenceRound();
      //} while (gameOn);

      Console.ReadLine();
    }
  }
}