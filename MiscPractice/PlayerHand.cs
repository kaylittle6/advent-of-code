namespace MiscPractice
{
  public class PlayerHand
  {
    public Player Player { get; set; }
    public List<Card> Cards { get; set; }
    
    public PlayerHand(Player player)
    {
      Player = player;
      Cards = new List<Card>();
    }
  }
}
