namespace BJ
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      var game = Game.GetGameClient;
      var gameOn = true;

      game.StartNewGame();
      game.CommenceRound();

      //do
      //{
      //  game.CommenceRound();
      //} while (gameOn);

      Console.ReadLine();
    }
  }
}