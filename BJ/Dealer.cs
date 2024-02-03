namespace BJ
{
  public class Dealer
  {
    public List<Card> Deck { get; set; } = new List<Card>();
    public int DeckCount { get; set; }

    public Dealer() { }

    public void GetDeck(int numberOfDecks)
    {
      List<List<Card>> masterDeck = new List<List<Card>>();

      for (int i = 0; i < numberOfDecks; i++)
      {
        List<Card> deck = new List<Card>();
        string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };
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

    public void DealStartingCards(List<Player> players)
    {
      foreach (var player in players)
      {
        for (int i = 0; i < 2; i++)
        {
          Random random = new();
          var randomIndex = random.Next(Deck.Count);
          var randomCard = Deck.ElementAt(randomIndex);

          player.Cards.Add(randomCard);
          Deck.Remove(randomCard);
        }
      }
    }

    public void DealCard(Player player)
    {
      Random random = new();
      var randomIndex = random.Next(Deck.Count);
      var randomCard = Deck.ElementAt(randomIndex);

      player.Cards.Add(randomCard);
      Deck.Remove(randomCard);
    }

    public void MakeSmartMove(List<Player> players)
    {
      bool done = false;
      var dealer = players.FirstOrDefault(p => p.Name == "Dealer");

      do
      {
        if (dealer!.HandValue > 15)
        {

        }
      } while (done == false);
    }

    public void ShuffleDeck()
    {
      Deck = Deck.OrderBy(c => Guid.NewGuid()).ToList();
    }
  }
}
