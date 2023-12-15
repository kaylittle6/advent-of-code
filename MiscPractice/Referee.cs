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

      SmallBlindIndex = SmallBlindIndex + 1 >= game.Players.Count ? 0 : ++SmallBlindIndex;
      BigBlindIndex = BigBlindIndex + 1 >= game.Players.Count ? 0 : ++BigBlindIndex;
    }

    public void RoundOfBets(Game game)
    {
      foreach (var player in game.Players)
      {
        if (player.IsNPC)
        {
          var result = player.MakePreFlopBet(game);

          if (result == "Fold")
          {
            player.Cards.Clear();
          }
        }
        else
        {
          Console.WriteLine($"{player.Name}, what would you like to do?");
          Console.WriteLine();
          Console.Write("1. Call -- 2. Raise -- 3. Fold");
          Console.WriteLine();

          var response = Console.ReadLine();

          switch (response)
          {
            case "1":
              var callBet = game.CurrentBet;

              if (player.Money <= callBet)
              {
                game.TotalPot += player.Money;
                player.Money = 0;
              }
              else
              {
                game.TotalPot += callBet;
                player.Money -= callBet;
              }
              break;

            case "2":
              var raiseBet = game.CurrentBet != game.Referee.BigBlind 
                ? game.CurrentBet * 3 
                : game.Referee.BigBlind * 3;

              if (player.Money <= raiseBet)
              {
                game.TotalPot += player.Money;
                player.Money = 0;
              }
              else
              {
                game.TotalPot += raiseBet;
                player.Money -= raiseBet;
              }
              break;

            case "3":
              player.Cards.Clear();
              break;
          }
        }
      }
    }
  }
}
