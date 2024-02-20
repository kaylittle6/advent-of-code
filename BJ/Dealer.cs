namespace BJ
{
  public class Dealer : Player
  {
    public List<Card> Deck { get; private set; } = new();
    public RuleBook Rules { get; set; } = new();
    public int DeckCount { get; set; }
    public int MinimumBet { get; set; }

    public Dealer(string name, bool isDealer) : base(name, isDealer)
    {
      Name = name;
      IsDealer = isDealer;
    }

    public void GetDeck(int numberOfDecks)
    {
      string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };
      string[] suits = { "Hearts", "Diamonds", "Spades", "Clubs" };
      
      Deck = values.SelectMany(_ => suits, (value, suit) => new Card(value, suit))
        .SelectMany(card => Enumerable.Repeat(card, numberOfDecks))
        .ToList();
      
      ShuffleDeck();
    }

    public void DealStartingCards(IEnumerable<Player> players)
    {
      Random random = new();
      foreach (var player in players.Where(player => player.InHand))
      {
        for (var i = 0; i < 2; i++)
        {
          var randomIndex = random.Next(Deck.Count);
          var randomCard = Deck[randomIndex];

          player.Cards.Add(randomCard);
          Deck.RemoveAt(randomIndex);
        }
      }
    }

    public void CollectAntes()
    {
      var game = Game.GetGameClient;

      foreach (var player in game.Players.Where(p => !p.IsDealer))
      {
        do
        {
          Console.Clear();
          Display.ShowTable(false);
          Console.WriteLine();
          Console.WriteLine($"Minimum bet is {MinimumBet:C2}, would you like to play, {player.Name}?");
          Console.WriteLine();
          Console.WriteLine("Type in at least the minimum bet to play, or 'no' to sit this hand out:");
          Console.WriteLine();

          var response = Console.ReadLine()?.ToLower();

          if (response != "no" && int.TryParse(response, out var number))
          {
            if (number < MinimumBet)
            {
              Console.WriteLine("Please bet more than the minimum");
              Console.WriteLine();
              Thread.Sleep(3000);
            }
            else
            {
              player.CurrentMoney -= number;
              player.PreviousBet = number;
              player.CurrentBet = number;
              break;
            }
          }
          else
          {
            Console.WriteLine("Please either place a bet, or sit this hand out");
            Console.WriteLine();
            Thread.Sleep(3000);
          }
          
        } while (true);
      }
    }

    public static void OfferInsurance(Player player)
    {
      if (!player.InHand || player.IsDealer) return;

      Console.Clear();
      Display.ShowTable(player, false);
      Console.WriteLine();
      Console.WriteLine($"The Dealer is showing an Ace, {player.Name}. Would you like insurance?");
      Console.WriteLine();
      Console.WriteLine($"Insurance Cost: {player.CurrentBet / 2:C2}");
      Console.WriteLine();

      string? response;
      
      do
      {
        response = Console.ReadLine()?.ToLower();
        
        if (response != null)
        {
          if (response == "yes")
          {
            player.CurrentMoney -= player.CurrentBet / 2;
            player.HasInsurance = true;
          }
          else
          {
            Console.WriteLine();
            Console.WriteLine("Fine then");
            Thread.Sleep(4000); 
          }
        }
        else
        {
          Console.WriteLine("Please select a valid answer");
        }
      } while (response != "yes" && response != "no");
    }

    public void AskForPlayerOptions(Player player)
    {
      if (player.IsDealer)
      {
        return;
      }

      var askAgain = true;

      do
      {
        Console.Clear();
        Display.ShowTable(player, false);
        Console.WriteLine();
        Console.WriteLine($"{player.Name}, here are you options:");
        Console.WriteLine();

        if (player.Cards.Count == 2)
        {
          Console.WriteLine("Double Down");

          if (player.Cards[0].CardNumber == player.Cards[1].CardNumber)
          {
            Console.WriteLine("Split");
          }
        }

        Console.WriteLine("Hit");
        Console.WriteLine("Stand");
        Console.WriteLine();

        var response = Console.ReadLine()?.ToLower();

        switch (response)
        {
          case "double down":
            RuleBook.DoubleDownBet(player);
            DealCard(player);
            askAgain = false;
            break;
          case "split":
            // Method to Split Cards here
            break;
          case "hit":
            DealCard(player);
            break;
          case "stand":
            return;
        }
        
        if ()
      } while (askAgain);
    }

    private void DealCard(Player player)
    {
      var randomIndex = new Random().Next(Deck.Count);
      var randomCard = Deck[randomIndex];

      player.Cards.Add(randomCard);
      Deck.RemoveAt(randomIndex);
    }

    public void FlipFaceDownCard()
    {
      Cards.Where(card => card.IsFaceDown).ToList().ForEach(card => card.IsFaceDown = false);
      Console.Clear();
    }

    private void ShuffleDeck()
    {
      Deck = Deck.OrderBy(_ => Guid.NewGuid()).ToList();
    }
  }
}
