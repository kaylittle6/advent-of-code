namespace BJ
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var game = Game.GetGameClient;

      game.StartNewGame();

      Console.Clear();
      Console.WriteLine("How many decks would you like to play with?");
      Console.WriteLine();

      var decks = Int32.Parse(Console.ReadLine()!);

      game.Dealer.GetDeck(decks);
      game.Dealer.DealCards(game.Players);
      game.Display.ShowTable(game, decks);

      Console.ReadLine();
    }
  }
}