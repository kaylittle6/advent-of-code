namespace BJ
{
  public class Dealer : Player
  {
    public List<Card> Deck { get; private set; } = new List<Card>();
    public List<int> PlayerBets { get; set; } = new List<int>();
    public RuleBook Rules { get; set; } = new RuleBook();
    public int DeckCount { get; set; }
    public int MinimumBet { get; set; }

    public Dealer(string name, bool isDealer) : base(name, isDealer)
    {
      Name = name;
      IsDealer = isDealer;
    }

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
          if (player.InHand)
          {
            Random random = new();
            var randomIndex = random.Next(Deck.Count);
            var randomCard = Deck.ElementAt(randomIndex);

            player.Cards.Add(randomCard);
            Deck.Remove(randomCard);
          }
        }
      }
    }

    public void CollectAntes()
    {
      Game game = Game.GetGameClient;
      bool goodResponse = true;

      foreach (var player in game.Players)
      {
        if (!player.IsDealer)
        {
          do
          {
            Console.Clear();

            game.Display.ShowTable(false);

            Console.WriteLine();
            Console.WriteLine($"Minimum bet is {MinimumBet:C2}, would you like to play, {player.Name}?");
            Console.WriteLine();
            Console.WriteLine("Type in at least the minimum bet to play, or 'no' to sit this hand out:");
            Console.WriteLine();

            var response = Console.ReadLine()?.ToLower();
            var isNumber = int.TryParse(response, out int number);

            if (response != null)
            {
              if (!isNumber)
              {
                player.InHand = false;
              }
              else if (number < MinimumBet)
              {
                goodResponse = false;
                Console.WriteLine("Please bet more than the minimum");
                Console.WriteLine();
                Thread.Sleep(3000);
              }
            }
            else
            {
              goodResponse = false;
              Console.WriteLine("Please either place a bet, or sit this hand out");
              Console.WriteLine();
              Thread.Sleep(3000);
            }

            player.CurrentMoney = player.InHand ? player.CurrentMoney -= number : player.CurrentMoney;
            player.PreviousBet = number;
            player.CurrentBet += number;

          } while (!goodResponse);
        }
      }
    }

    public void OfferInsurance(Player player)
    {
      bool goodResp = true;

      if (player is not { InHand: true, HasBlackJack: true }) return;
      do
      {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine($"The Dealer is showing an Ace, {player.Name}. Would you like insurance?");
        Console.WriteLine();
        Console.WriteLine($"Insurance Cost: {player.CurrentBet / 2}");
        Console.WriteLine();

        var response = Console.ReadLine()?.ToLower();

        if (response != null)
        {
          if (response == "yes")
          {
            player.CurrentMoney -= player.CurrentBet / 2;
            player.HasInsurance = true;
          }
          else
          {
            Console.WriteLine("Fine then");
            Thread.Sleep(4000); 
          }
        }
        else
        {
          Console.WriteLine("Please select a valid answer");
          goodResp = false;
        }
      } while (!goodResp);


    }

    // public void AskForPlayerOptions(Player player, bool firstAsk)
    // {
    //   bool goodResp = true;
    //
    //   if (firstAsk)
    //   {
    //
    //   }
    //
    // }

    public void DealCard(Player player)
    {
      Random random = new();
      var randomIndex = random.Next(Deck.Count);
      var randomCard = Deck.ElementAt(randomIndex);

      player.Cards.Add(randomCard);
      Deck.Remove(randomCard);
    }

    public void FlipFaceDownCard()
    {
      foreach (var card in Cards)
      {
        if (card.IsFaceDown)
        {
          card.IsFaceDown = false;
        }
      }
      Console.Clear();
    }

    public void ShuffleDeck()
    {
      Deck = Deck.OrderBy(c => Guid.NewGuid()).ToList();
    }
  }
}
