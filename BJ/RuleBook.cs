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

      foreach (var player in game.Players.Where(player => player is { IsDealer: false, InHand: true }))
      {
        foreach (var hand in player.Hand.Where(hand => hand.HasBlackJack))
        {
          WinBlackJackBet(player, hand);

          Console.Clear();
          Display.ShowTable(dealerFlipped);
          Console.WriteLine();
          Console.WriteLine($"{player.Name} has Blackjack! They win ${hand.CurrentBet * 2.5m}");
          Thread.Sleep(4000);
          ResetPlayer(player, hand);
        }
      }
    }
    
    public static void WinStandardBet(Player player, Hand hand)
    {
      player.CurrentMoney += hand.CurrentBet * 2;
    }
    
    private static void WinBlackJackBet(Player player, Hand hand)
    {
      player.CurrentMoney += hand.CurrentBet * 2.5m;
    }

    public static void PushBet(Player player, Hand hand)
    {
      player.CurrentMoney += hand.CurrentBet;
    }

    public static void DoubleDownBet(Player player, Hand hand)
    {
      player.CurrentMoney -= hand.CurrentBet;
      player.DoubledDown = true;
    }
    
    public static HandResult CheckHand(Hand hand)
    {
      do
      {
        if (hand.Cards.Any(c => c is { CardValue: 11 }))
        {
          hand.Cards.First(c => c is { CardValue: 11 }).ChangeAceValueToOne();
        }

        switch (hand.Value)
        {
          case 21:
            return HandResult.HandBlackjack;
          case > 21:
            return HandResult.HandBusted;
        }
      } while (hand.Cards.Any(c => c is { CardValue: 11 }));

      return HandResult.HandValid;
    }

    public static void ReduceAceValueToOne(Hand hand)
    {
      do
      {
        if (hand.Cards.Any(c => c is { CardValue: 11 }) && hand.Value > 21)
        {
          hand.Cards.First(c => c is { CardValue: 11 }).ChangeAceValueToOne();
        }
      } while (hand.Cards.Any(c => c is { CardValue: 11 }));
    }
    
    public static void ResetPlayer(Player player, Hand hand)
    {
      player.InHand = false;
      player.HasInsurance = false;
      player.DoubledDown = false;
      hand.CurrentBet = 0;
      hand.Cards.Clear();
    }
  }
}
