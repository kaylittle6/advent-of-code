namespace BJ
{
  public class Player
  {
    public string? Name { get; set; }
    public decimal CurrentMoney { get; set; } = 0;
    public List<Card> Cards { get; set; } = new List<Card>();
    public int HandValue => Cards.Sum(c => c.CardValue);
    public bool InHand { get; set; } = true;
    public bool IsDealer { get; set; } = false;
    public decimal CurrentBet { get; set; }
    public decimal PreviousBet { get; set; } = 0;
    public bool HasInsurance { get; set; } = false;
    public bool DoubledDown { get; set; } = false;

    public Player(string name)
    {
      Name = name;
    }
  }
}
