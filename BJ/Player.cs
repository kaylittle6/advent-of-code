namespace BJ
{
  public class Player
  {
    public string? Name { get; set; }
    public List<Card> Cards { get; set; } = new List<Card>();
    public decimal CurrentMoney { get; set; } = 0;
    public decimal CurrentBet { get; set; }
    public decimal PreviousBet { get; set; } = 0;
    public bool IsDealer { get; set; } = false;
    public bool HasBlackJack => Cards.Sum(cv => cv.CardValue) == 21 ? true : false;
    public bool InHand { get; set; } = true;
    public bool HasInsurance { get; set; } = false;
    public bool DoubledDown { get; set; } = false;

    public Player(string name)
    {
      Name = name;
    }
  }
}
