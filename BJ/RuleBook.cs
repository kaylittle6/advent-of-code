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

    public void CheckAndResolveBlackJack()
    {
      Game game = Game.GetGameClient;

      foreach (var player in game.Players)
      {
        if (player.InHand && player.Cards.Sum(cv => cv.CardValue) == 21)
        {
          var payout = player.PreviousBet * 1.5m;
          player.CurrentMoney += payout + player.PreviousBet;
          
          Console.Clear();
          game.Display.ShowTable();
          Console.WriteLine();
          Console.WriteLine();
          Console.WriteLine($"{player.Name} has Blackjack! They win ${payout}");
          Thread.Sleep(5000);
          game.Display.ShowTable();
          player.InHand = false;
        }
      }
    }

    public void PlaySplitHand(Player player)
    {
      List<List<Card>> splitHands = new List<List<Card>>();
      splitHands[0] = new List<Card>();
      splitHands[1] = new List<Card>();

      //splitHands[0].Add(player.Cards);

      
    }

    public void WinStandardBet(Player player)
    {
      player.CurrentMoney += player.CurrentBet * 2;
    }
    public void WinBlackJackBet(Player player)
    {
      player.CurrentMoney += player.CurrentBet * 1.5m;
    }

    public void PushBet(Player player)
    {
      player.CurrentMoney += player.CurrentBet;
    }

    public void PlayerLoses(Player player)
    {
      player.InHand = false;
      player.HasInsurance = false;
      player.DoubledDown = false;
      player.PreviousBet = player.CurrentBet;
      player.CurrentBet = 0;
      player.Cards.Clear();
    }
  }
}
