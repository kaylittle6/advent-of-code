namespace MiscPractice
{
  public class Player
  {
    public string Name { get; set; }
    public List<Card> Cards { get; set; }
    public int Money { get; set; }
    public bool IsNPC { get; set; }

    public Player(string name)
    {
      Name = name;
      Cards = new List<Card>();
    }

    public string MakePreFlopBet(Game game)
    {
      game.CurrentBet = game.Dealer.BigBlind;

      if (Cards.Sum(c => c.CardValue) >= 20
        || Cards.GroupBy(c => c.CardValue).Any(g => g.Count() >= 2))
      {
        if (game.CurrentBet != game.Dealer.BigBlind)
        {
          var bet = game.CurrentBet * 3;

          if (Money <= bet)
          {
            game.TotalPot += Money;
            Money = 0;
          }
          else
          {
            Money -= bet;
            game.TotalPot += bet;
          }

          game.CurrentBet = bet;
        }
        else
        {
          var bet = game.Dealer.BigBlind * 3;

          if (Money <= bet)
          {
            game.TotalPot += Money;
            Money = 0;
          }
          else
          {
            Money -= bet;
            game.TotalPot += bet;
          }

          game.CurrentBet = bet;
        }

        return "Raise";
      }

      else if (Cards.Sum(c=> c.CardValue) <= 15)
      {
        return "Fold";
      }

      else
      {
        var bet = game.CurrentBet;

        if (Money <= bet)
        {
          game.TotalPot += Money;
          Money = 0;
        }
        else
        {
          Money -= bet;
          game.TotalPot += bet;
        }

        game.CurrentBet = bet;

        return "Call";
      }
    }
  }
}
