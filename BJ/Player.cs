namespace BJ
{
  public class Player
  {
    public string? Name { get; set; }
    public int CurrentMoney { get; set; } = 0;
    public List<Card> Cards { get; set; } = new List<Card>();

    public Player(string name)
    {
      Name = name;
    }
  }
}
