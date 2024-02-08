namespace BJ
{
  public class Player
  {
    public string? Name { get; set; }
    public decimal CurrentMoney { get; set; } = 0;
    public List<Card> Cards { get; set; } = new List<Card>();
    public int HandValue => Cards.Sum(c => c.CardValue);
    public bool InHand { get; set; } = true;
    public decimal PreviousBet { get; set; }

    public Player(string name)
    {
      Name = name;
    }
  }
}
