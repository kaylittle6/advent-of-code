namespace MiscPractice
{
  public class Player
  {
    public string Name { get; set; }
    public List<Card> Cards { get; set; }
    public int Money { get; set; }
    public bool IsNPC { get; set; }

    public Player(string name)
    {
      Name = name;
      Cards = new List<Card>();
    }
  }
}
