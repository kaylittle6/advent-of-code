namespace BJ
{
  public class Player
  {
    public string? Name { get; set; }
    public int CurrentMoney { get; set; } = 0;

    public Player(string name)
    {
      Name = name;
    }
  }
}
