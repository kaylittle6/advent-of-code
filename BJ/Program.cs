namespace BJ
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      var game = Game.GetGameClient;
      
      game.StartNewGame();
      game.CommenceRound();

      Console.ReadLine();
    }
  }
}