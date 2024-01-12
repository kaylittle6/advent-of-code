namespace MiscPractice
{
  public class Player
  {
    private static Game? mainGame;

    public string Name { get; set; }
    public List<Card> HoleCards { get; set; }
    public List<Card> AllCards { get; set; }
    public int Money { get; set; }
    public bool IsNPC { get; set; }
    public bool IsButton { get; set; }
    public bool IsSmallBlind { get; set; }
    public bool IsBigBlind { get; set; }

    public Player(string name)
    {
      Name = name;
      HoleCards = new List<Card>();
      AllCards = new List<Card>();
    }

    public static Game? MainGame
    {
      get
      {
        if (mainGame == null)
        {
          mainGame = new Game();
        }

        return mainGame;
      }
    }

    public string MakePreFlopBet()
    {
      GameState.CurrentBet = GameState.CurrentBigBlind;

      if (HoleCards.Sum(c => c.CardValue) >= 20
        || HoleCards.GroupBy(c => c.CardValue).Any(g => g.Count() >= 2))
      {
        if (GameState.CurrentBet != GameState.CurrentBigBlind)
        {
          var bet = GameState.CurrentBet * 3;

          if (Money <= bet)
          {
            GameState.CurrentPotTotal += Money;
            Money = 0;
          }
          else
          {
            Money -= bet;
            GameState.CurrentPotTotal += bet;
          }

          GameState.CurrentPotTotal = bet;
        }
        else
        {
          var bet = GameState.CurrentBigBlind * 3;

          if (Money <= bet)
          {
            GameState.CurrentPotTotal += Money;
            Money = 0;
          }
          else
          {
            Money -= bet;
            GameState.CurrentPotTotal += bet;
          }

          GameState.CurrentBet = bet;
        }

        return "Raise";
      }

      else if (HoleCards.Sum(c=> c.CardValue) <= 15)
      {
        return "Fold";
      }

      else
      {
        var bet = GameState.CurrentBet;

        if (Money <= bet)
        {
          GameState.CurrentPotTotal += Money;
          Money = 0;
        }
        else
        {
          Money -= bet;
          GameState.CurrentPotTotal += bet;
        }

        GameState.CurrentBet = bet;

        return "Call";
      }
    }
  }
}
