namespace BJ
{
  public class RuleBook
  {
    public bool CheckForBust(Player player)
    {
      if (player.Cards.Sum(cv => cv.CardValue) > 21 && player.Cards.Any(c => c.CardNumber == "Ace"))
      {
        foreach (var card in player.Cards.Where(c => c.CardNumber == "Ace"))
        {
          card.CardValue = 1;
        }
      }
    }
  }
}
