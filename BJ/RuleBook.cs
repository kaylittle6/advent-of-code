namespace BJ
{
  public class RuleBook
  {
    public void CheckAndResolveBlackJack()
    {
      Game game = Game.GetGameClient;

      foreach (var player in game.Players)
      {
        if (player is { InHand: true, HasBlackJack: true })
        {
          WinBlackJackBet(player);

          Console.Clear();
          game.Display.ShowTable(false);
          Console.WriteLine();
          Console.WriteLine();
          Console.WriteLine($"{player.Name} has Blackjack! They win ${player.CurrentBet * 1.5m}");
          Thread.Sleep(5000);
          game.Display.ShowTable(false);
          ResetPlayer(player);
        }
      }
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

    public void DoubleDownBet(Player player)
    {
      player.CurrentMoney -= player.CurrentBet;
      player.DoubledDown = true;
    }

    public void ResetPlayer(Player player)
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
