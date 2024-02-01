namespace BJ
{
  public class Dealer
  {
    public List<Card> Deck { get; set; } = new List<Card>();
    public bool NeedsReshuffle { get; set; } = false;

    public Dealer() { }

    public void GetDeck(int numberOfDecks)
    {
      List<List<Card>> masterDeck = new List<List<Card>>();

      for (int i = 0; i < numberOfDecks; i++)
      {
        List<Card> deck = new List<Card>();
        string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
        string[] suits = { "Hearts", "Diamonds", "Spades", "Clubs" };

        foreach (var value in values)
        {
          foreach (var suit in suits)
          {
            deck.Add(new Card(value, suit));
          }
        }
        masterDeck.Add(deck);
      }
      Deck = masterDeck.SelectMany(l => l).ToList();
      ShuffleDeck();
    }

    public void ShuffleDeck()
    {
      Deck = Deck.OrderBy(c => Guid.NewGuid()).ToList();
    }
  }
}
