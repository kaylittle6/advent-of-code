namespace TexasHoldEm.Static;

public static class GameActions
{
  public enum HandRanks
  {
    HighCard = 0,
    Pair = 1,
    TwoPair = 2,
    ThreeOfAKind = 3,
    Straight = 4,
    Flush = 5,
    FullHouse = 6,
    FourOfAKind = 7,
    StraightFlush = 8,
    RoyalFlush = 9
  }

  public enum PlayerOptions
  {
    Bet,
    Fold,
    Call,
    Raise
  }

  public static void WinPot()
  {
    
  }
}