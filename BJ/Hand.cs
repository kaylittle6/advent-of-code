namespace BJ
{
  public class Hand
  {
    public List<Card> Cards { get; set; } = new();
    public int Value => Cards.Where(c => !c.IsFaceDown).Sum(c => c.CardValue);
    public bool HasBlackJack => Cards.Sum(cv => cv.CardValue) == 21;
    public bool DoubledDown { get; set; } = false;
    public decimal CurrentBet { get; set; } = 0;
  }
}
