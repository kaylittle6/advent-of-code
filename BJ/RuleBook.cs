namespace BJ
{
  public class RuleBook
  {
    public void CheckAndResolveBlackJack()
    {
      var game = Game.GetGameClient;

      foreach (var player in game.Players.Where(p => p is { InHand: true, HasBlackJack: true }))
      {
        WinBlackJackBet(player);

        Console.Clear();
        Display.ShowTable(false);
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine($"{player.Name} has Blackjack! They win ${player.CurrentBet * 1.5m}");
        Thread.Sleep(5000);
        Display.ShowTable(false);
        ResetPlayer(player);
      }
    }
    
    public void WinStandardBet(Player player)
    {
      player.CurrentMoney += player.CurrentBet * 2;
    }
    
    private static void WinBlackJackBet(Player player)
    {
      player.CurrentMoney += player.CurrentBet * 1.5m;
    }

    public void PushBet(Player player)
    {
      player.CurrentMoney += player.CurrentBet;
    }

    public void DoubleDownBet(Player player)
    {
      player.CurrentMoney -= player.CurrentBet;
      player.DoubledDown = true;
    }

    public static void ResetPlayer(Player player)
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
