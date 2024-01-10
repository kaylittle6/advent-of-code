namespace MiscPractice
{
  public class Player
  {
    public string Name { get; set; }
    public List<Card> HoleCards { get; set; }
    public List<Card> AllCards { get; set; }
    public int Money { get; set; }
    public bool IsNPC { get; set; }

    public Player(string name)
    {
      Name = name;
      HoleCards = new List<Card>();
      AllCards = new List<Card>();
    }

    public string MakePreFlopBet(Game game)
    {
      game.State.CurrentBet = game.Dealer.BigBlind;

      if (HoleCards.Sum(c => c.CardValue) >= 20
        || HoleCards.GroupBy(c => c.CardValue).Any(g => g.Count() >= 2))
      {
        if (game.State.CurrentBet != game.Dealer.BigBlind)
        {
          var bet = game.State.CurrentBet * 3;

          if (Money <= bet)
          {
            game.State.CurrentPotTotal += Money;
            Money = 0;
          }
          else
          {
            Money -= bet;
            game.State.CurrentPotTotal += bet;
          }

          game.State.CurrentPotTotal = bet;
        }
        else
        {
          var bet = game.Dealer.BigBlind * 3;

          if (Money <= bet)
          {
            game.State.CurrentPotTotal += Money;
            Money = 0;
          }
          else
          {
            Money -= bet;
            game.State.CurrentPotTotal += bet;
          }

          game.State.CurrentBet = bet;
        }

        return "Raise";
      }

      else if (HoleCards.Sum(c=> c.CardValue) <= 15)
      {
        return "Fold";
      }

      else
      {
        var bet = game.State.CurrentBet;

        if (Money <= bet)
        {
          game.State.CurrentPotTotal += Money;
          Money = 0;
        }
        else
        {
          Money -= bet;
          game.State.CurrentPotTotal += bet;
        }

        game.State.CurrentBet = bet;

        return "Call";
      }
    }
  }
}
