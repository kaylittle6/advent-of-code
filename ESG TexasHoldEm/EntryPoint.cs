namespace TexasHoldEm
{
  public static class EntryPoint
  {
    public static void Main(string[] args)
    {
      var gameClient = Game.GetGameClient;
      
      gameClient.StartNewGame();
      gameClient.CommenceRound();
      
      Console.ReadLine();
      
    }
  }
}