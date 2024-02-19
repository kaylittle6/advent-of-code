namespace BJ
{
  public class Player
  {
    public string? Name { get; protected init; }
    public List<Card> Cards { get; } = new();
    public decimal CurrentMoney { get; set; }
    public decimal CurrentBet { get; set; }
    public decimal PreviousBet { get; set; }
    public bool IsDealer { get; set; }
    public bool HasBlackJack => Cards.Sum(cv => cv.CardValue) == 21;
    public bool InHand { get; set; } = true;
    public bool HasInsurance { get; set; }
    public bool DoubledDown { get; set; }

    public Player(string name, bool isDealer)
    {
      Name = name;
      IsDealer = isDealer;
    }
  }
}
