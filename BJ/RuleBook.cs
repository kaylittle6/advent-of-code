namespace BJ
{
  public class RuleBook
  {
    // 0 = Bust, 1 = Win, 2 = No Action
    public bool[] CheckForResult(Player player)
    {
      bool[] results = new bool[3];

      // Did player Bust (more than 21 total)?
      results[0] = player.Cards.Sum(cv => cv.CardValue) > 21
        ? true : false;

      // Did player Win (21 total)?
      results[1] = player.Cards.Sum(cv => cv.CardValue) == 21
        ? true : false;

      // No Action (less than 21 total)?
      results[2] = player.Cards.Sum(cv => cv.CardValue) < 21
        ? true : false;

      return results;
    }

    public void CheckForBlackJack(Game game)
    {
      foreach (var player in game.Players)
      {
        if (player.InHand && player.Cards.Sum(cv => cv.CardValue) == 21)
        {
          var payout = player.PreviousBet * 1.5m;
          player.CurrentMoney += payout + player.PreviousBet;
          
          Console.Clear();
          game.Display.ShowTable(game);
          Console.WriteLine();
          Console.WriteLine();
          Console.WriteLine($"{player.Name} has Blackjack! They win ${payout}");
          Thread.Sleep(3000);
          game.Display.ShowTable(game);
        }
      }
    }
  }
}
