namespace BJ
{
  public class Player
  {
    public string? Name { get; set; }
    public int CurrentMoney { get; set; } = 0;
    public List<Card> Cards { get; set; } = new List<Card>();
    public int HandValue => Cards.Sum(c => c.CardValue);
    public bool InHand { get; set; } = true;
    public int PreviousBet { get; set; }

    public Player(string name)
    {
      Name = name;
    }

    public void CheckAceValue()
    {
      if (HandValue > 21 && Cards.Any(c => c.CardNumber == "Ace"))
      {
        foreach (var card in Cards)
        {
          if (card.CardNumber == "Ace")
          {
            card.CardValue = 1;
          } 
        }
      }

    }
 
  }
}
