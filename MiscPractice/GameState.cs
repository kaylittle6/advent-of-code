namespace MiscPractice
{
  public class GameState
  {
    public int NumberOfPlayers { get; set; }
    public int StartingMoney { get; set; }
    public string? CurrentPlayerAction { get; set; }
    public int CurrentBet { get; set; }
    public int CurrentDealerButton { get; set; }
    public int CurrentSmallBlind { get; set; }
    public int CurrentBigBlind { get; set; }
    public int CurrentPotTotal { get; set; }
  }
}
