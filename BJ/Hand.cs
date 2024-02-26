namespace BJ
{
  public class Hand
  {
    public List<Card> Cards { get; set; } = new();
    public int Value => Cards.Where(c => !c.IsFaceDown).Sum(cv => cv.CardValue);
    public bool HasBlackJack => Cards.Sum(cv => cv.CardValue) == 21;
    public decimal CurrentBet { get; set; }
  }
}
