namespace MiscPractice
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var mainGame = new Game();

      mainGame.Players.Add(new Player("Chris") { IsNPC = true });
      mainGame.Players.Add(new Player("Ed") { IsNPC = true });
      mainGame.Players.Add(new Player("Matt") { IsNPC = true });
      mainGame.Players.Add(new Player("Kyle") { IsNPC = false });

      mainGame.Dealer.DealInitialCards(mainGame);
      mainGame.Referee.DistributePlayerMoney(mainGame, 20000);
      mainGame.Referee.CollectBlinds(mainGame);
      





      foreach (var player in mainGame.Players)
      {
        foreach (var card in player.Cards)
        {
          Console.WriteLine($"{player.Name} has the {card.CardNumber} of {card.CardSuit}");  
        }

        Console.WriteLine($"{player.Name} has ${player.Money} dollars");
        Console.WriteLine();
      }

      foreach (var keys in mainGame.Deck)
      {
        Console.WriteLine(keys.Key);
      }
      
      
    }
  }
}