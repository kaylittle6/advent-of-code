namespace BJ
{
  public static class RuleBook
  {
    public enum HandResult
    {
      HandValid,
      HandBusted,
      HandBlackjack
    }
    
    public static void CheckAndResolveBlackJack(bool dealerFlipped)
    {
      var game = Game.GetGameClient;

      foreach (var player in game.Players.Where(p => p is { IsDealer: false, InHand: true, HasBlackJack: true }))
      {
        WinBlackJackBet(player);

        Console.Clear();
        Display.ShowTable(dealerFlipped);
        Console.WriteLine();
        Console.WriteLine($"{player.Name} has Blackjack! They win ${player.CurrentBet * 2.5m}");
        Thread.Sleep(5000);
        ResetPlayer(player);
      }
    }
    
    public static void WinStandardBet(Player player)
    {
      player.CurrentMoney += player.CurrentBet * 2;
    }
    
    private static void WinBlackJackBet(Player player)
    {
      player.CurrentMoney += player.CurrentBet * 2.5m;
    }

    public static void PushBet(Player player)
    {
      player.CurrentMoney += player.CurrentBet;
    }

    public static void DoubleDownBet(Player player)
    {
      player.CurrentMoney -= player.CurrentBet;
      player.DoubledDown = true;
    }
    
    public static HandResult CheckHand(Player player)
    {
      do
      {
        if (player.Cards.Any(c => c is { CardValue: 11 }))
        {
          player.Cards.First(c => c is { CardValue: 11 }).ChangeAceValueToOne();
        }

        switch (player.HandValue)
        {
          case 21:
            return HandResult.HandBlackjack;
          case > 21:
            return HandResult.HandBusted;
        }
      } while (player.Cards.Any(c => c is { CardValue: 11 }));

      return HandResult.HandValid;
    }

    public static void ReduceAceValueToOne(Player player)
    {
      do
      {
        if (player.Cards.Any(c => c is { CardValue: 11 }) && player.HandValue > 21)
        {
          player.Cards.First(c => c is { CardValue: 11 }).ChangeAceValueToOne();
        }
      } while (player.Cards.Any(c => c is { CardValue: 11 }));
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
