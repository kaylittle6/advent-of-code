namespace BJ
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var game = Game.GetGameClient;
      game.StartNewGame();

      Console.Clear();

      game.Dealer.GetOrShuffleDeck(2);

      Console.ReadLine();
    }
  }
}