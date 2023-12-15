namespace MiscPractice
{
  public class DeckDealer
  {
    public DeckDealer()
    {
      
    }

    public Dictionary<string, Card> CreateDeck(Game game)
    {
      string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
      string[] suits = { "Hearts", "Diamonds", "Spades", "Clubs" };

      foreach (var value in values)
      {
        foreach (var suit in suits)
        {
          var cardName = $"{value} of {suit}";
          game.Deck.Add(cardName, new Card(value, suit));
        }
      }

      return game.Deck;
    }

    public void DealInitialCards(Game game)
    {
      for (int c = 0; c < 2; c++)
      {
        foreach (var player in game.Players)
        {
          Random random = new();
          var randomIndex = random.Next(game.Deck.Count);
          var randomCard = game.Deck.ElementAt(randomIndex);

          player.Cards.Add(randomCard.Value);
          game.Deck.Remove(randomCard.Key);
        }
      }
    }

    public void DealFlopCards(Game game)
    {



    }

    public void DealTurnOrRiverCard(Game game)
    {



    }

  }
}
