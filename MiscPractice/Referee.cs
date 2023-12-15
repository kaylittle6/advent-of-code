namespace MiscPractice
{
  public class Referee
  {
    public int SmallBlind { get; set; } = 25;
    public int BigBlind { get; set; } = 50;
    public int SmallBlindIndex { get; set; } = 1;
    public int BigBlindIndex { get; set; } = 2;
    public HandRankings HandRankings { get; set; }

    public Referee()
    {
      HandRankings = new HandRankings();
    }

    public void DistributePlayerMoney(Game game, int totalMoney)
    {
      int splitMoney = totalMoney / game.Players.Count();

      foreach (var player in game.Players)
      {
        player.Money = splitMoney;
      }
    }

    public void RaiseBlinds()
    {
      SmallBlind *= 2;
      BigBlind *= 2;
    }

    public void ResetBlinds()
    {
      SmallBlind = 25;
      BigBlind = 50;
    }

    public void CollectBlinds(Game game)
    {
      game.Players[SmallBlindIndex].Money -= SmallBlind;
      game.Players[BigBlindIndex].Money -= BigBlind;

      SmallBlindIndex = SmallBlindIndex + 1 >= game.Players.Count ? SmallBlindIndex = 0 : SmallBlindIndex++;
      BigBlindIndex = BigBlindIndex + 1 >= game.Players.Count ? BigBlindIndex = 0 : BigBlindIndex++;
    }
  }
}
