namespace MiscPractice
{
  public class Game
  {
    public List<Player> Players { get; set; }
    public DeckDealer Dealer { get; set; }
    public Referee Referee { get; set; }
    public List<Card> CommunityCards { get; set; }
    public int CurrentBet { get; set; } = 0;
    public int TotalPot { get; set; } = 0;

    public Dictionary<string, Card> Deck = new();
    
    public Game()
    {
      Players = new List<Player>();
      Dealer = new DeckDealer();
      Referee = new Referee();
      CommunityCards = new List<Card>();
      Deck = Dealer.CreateDeck(this);
    }
  }
}
