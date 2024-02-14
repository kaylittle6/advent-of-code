namespace BJ
{
  public class Dealer : Player
  {
    public List<Card> Deck { get; set; } = new List<Card>();
    public List<int> PlayerBets { get; set; } = new List<int>();
    public RuleBook Rules { get; set; } = new RuleBook();
    public int DeckCount { get; set; }
    public int MinimumBet { get; set; }

    public Dealer(string name, bool isDealer) : base(name)
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

            game.Display.ShowTable();

            Console.WriteLine();
            Console.WriteLine($"Minimum bet is {MinimumBet.ToString("C2")}, would you like to play, {player.Name}?");
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

    public void OfferInsuranceAndResolve(Player player)
    {

    }

    public bool AskToHitOrStay(Player player)
    {
      bool goodResp = true;
      bool isHitting = false;

      do
      {
        Console.WriteLine();
        Console.WriteLine($"{player.Name}, would you like to hit or stay?");
        Console.WriteLine();

        var response = Console.ReadLine()?.ToLower();

        if (response != null)
        {
          if (response == "hit")
          {
            isHitting = true;
          }
          else
          {
            isHitting = false;
          }
        }
        else
        {
          goodResp = false;
          Console.WriteLine();
          Console.WriteLine("Please select a valid response");
          Thread.Sleep(3000);
        }
      } while (!goodResp);

      return isHitting;
    }

    public void DealCard(Player player)
    {
      Random random = new();
      var randomIndex = random.Next(Deck.Count);
      var randomCard = Deck.ElementAt(randomIndex);

      player.Cards.Add(randomCard);
      Deck.Remove(randomCard);
    }

    public void ShuffleDeck()
    {
      Deck = Deck.OrderBy(c => Guid.NewGuid()).ToList();
    }
  }
}
