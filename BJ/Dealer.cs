namespace BJ
{
  public class Dealer
  {
    public Dictionary<string, Card>? Deck { get; set; }
    public bool NeedsReshuffle { get; set; } = false;

    public Dealer() { }

    public Dictionary<string, Card> GetOrShuffleDeck(int numberOfDecks)
    {
      Dictionary<string, Card> deck = new Dictionary<string, Card>();
      string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
      string[] suits = { "Hearts", "Diamonds", "Spades", "Clubs" };

      for (int i = 0; i < numberOfDecks; i++)
      {
        foreach (var value in values)
        {
          foreach (var suit in suits)
          {
            var cardName = $"{value} of {suit}";
            deck.Add(cardName, new Card(value, suit));
          }
        }
      }

      return deck;
    }

    public void SetupNewPlayer()
    {

    }
  }
}
