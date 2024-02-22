namespace BJ
{
  public static class EntryPoint
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