namespace BJ
{
  public class Player
  {
    public string? Name { get; protected init; }
    public List<Hand> Hand { get; set; }
    public decimal CurrentMoney { get; set; }
    public bool IsDealer => Name == "Dealer"; 
    public bool InHand { get; set; } = true;
    public bool HasInsurance { get; set; }
    public bool DoubledDown { get; set; }

    public Player(string name)
    {
      Name = name;
      Hand = new List<Hand> { new() };
    }
  }
}
