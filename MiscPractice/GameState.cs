namespace MiscPractice
{
  public static class GameState
  {
    public static int NumberOfPlayers { get; set; }
    public static int StartingMoney { get; set; }
    public static string? CurrentPlayerAction { get; set; }
    public static int CurrentBet { get; set; }
    public static int CurrentDealerButton { get; set; }
    public static int CurrentSmallBlind { get; set; }
    public static int CurrentBigBlind { get; set; }
    public static int CurrentPotTotal { get; set; }
    public static int DealerButtonIndex { get; set; } = 0;
    public static int SmallBlindIndex { get; set; } = 1;
    public static int BigBlindIndex { get; set; } = 2;
  }
}
