namespace BJ
{
  public class Dealer : Player
  {
    public List<Card> Deck { get; private set; } = new();
    public int DeckCount { get; set; }
    public int MinimumBet { get; set; }
    private bool NeedsReshuffle => GetReshuffle();

    public Dealer(string name, bool isDealer) : base(name, isDealer)
    {
      Name = name;
      IsDealer = isDealer;
    }
    
    private bool GetReshuffle()
    {
      return Deck.Count < DeckCount * 52 * 0.25;
    }
    
    public void GetDeck(int numberOfDecks)
    {
      string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };
      string[] suits = { "Hearts", "Diamonds", "Spades", "Clubs" };
      
      Deck = values.SelectMany(_ => suits, (value, suit) => new Card(value, suit))
        .SelectMany(card => Enumerable.Repeat(card, numberOfDecks))
        .ToList();
      
      Deck = Deck.OrderBy(_ => Guid.NewGuid()).ToList();
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

      for (var i = 0; i < game.Players.Count; i++)
      {
        var player = game.Players[i];

        if (player.IsDealer) continue;
        
        do
        {
          Console.Clear();
          Display.ShowTable(false);
          Console.WriteLine();
          Console.WriteLine($"Minimum bet is {MinimumBet:C2}, would you like to play, {player.Name}?");
          Console.WriteLine();
          Console.WriteLine("Place your bet or type the option you would like:");
          Console.WriteLine();
          Console.WriteLine($"Place Bet (Minimum: {game.Dealer.MinimumBet:C2})");
          Console.WriteLine("Sit this Hand (sit)");
          Console.WriteLine("Leave game (leave)");
          Console.WriteLine();

          var response = Console.ReadLine()?.ToLower();
          
          if (response == "sit")
          {
            player.InHand = false;
            break;
          }
          
          if (response == "leave")
          {
            game.Players.RemoveAt(i);
            i--;
            break;
          }

          if (int.TryParse(response, out var number))
          {
            if (number < MinimumBet)
            {
              Console.WriteLine("Please bet more than the minimum");
              Console.WriteLine();
              Thread.Sleep(3000);
            }
            else
            {
              player.PreviousBet = player.CurrentBet;
              player.CurrentMoney -= number;
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
          if (response != "yes") continue;
          
          player.CurrentMoney -= player.CurrentBet / 2;
          player.CurrentBet += player.CurrentBet / 2;
          player.HasInsurance = true;
        }
        else
        {
          Console.WriteLine("Please select a valid answer");
        }
      } while (response != "yes" && response != "no");
    }

    public void AskForPlayerOptions(Player player)
    {
      if (player.IsDealer || !player.InHand)
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
            DoubleDownAction(player);
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

        var handResult = RuleBook.CheckHand(player);

        Console.Clear();
        Display.ShowTable(player, false);
        
        switch (handResult)
        {
          case RuleBook.HandResult.HandBlackjack:
            Console.WriteLine($"{player.Name}, you have 21, very nice!");
            Console.WriteLine();
            askAgain = false;
            Thread.Sleep(3000);
            break;
          case RuleBook.HandResult.HandBusted:
            Console.WriteLine($"{player.Name}, you busted! You lose ${player.CurrentBet}");
            Console.WriteLine();
            askAgain = false;
            RuleBook.ResetPlayer(player);
            Thread.Sleep(4000);
            break;
          case RuleBook.HandResult.HandValid:
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
      } while (askAgain);
    }
    
    public void DealerAction()
    {
      Console.Clear();
      
      do
      {
        Display.ShowDealersActions(this);
        Console.WriteLine();
        Console.WriteLine("Dealer hitting...");
        Thread.Sleep(3000);
        DealCard(this);
        RuleBook.ReduceAceValueToOne(this);
        Console.Clear();
        Display.ShowDealersActions(this);
        Thread.Sleep(4000);
        Console.Clear();
      } while (HandValue < 17);
    }

    public void DoubleDownAction(Player player)
    {
      Console.Clear();

      Display.ShowTable(player, false);
      Console.WriteLine();
      Console.WriteLine("It's a double down! Here it comes...");
      Thread.Sleep(3000);
      DealCard(player);
      Console.Clear();
      Display.ShowTable(player, false);
      Thread.Sleep(3000);
      Console.Clear();
    }

    public void FinishUpRound(IEnumerable<Player> players)
    {
      Cards.Clear();
        
      if (NeedsReshuffle)
      {
        GetDeck(DeckCount);
      }
        
      foreach (var player in players.Where(p => !p.IsDealer))
      {
        player.InHand = true;
      }
    }

    public static void RemoveBrokeAssPlayers(List<Player> players)
    {
      for (var i = 0; i < players.Count; i++)
      {
        var player = players[i];

        if (player.IsDealer || player.CurrentMoney > 0) continue;
        
        Console.Clear();
        Display.ShowTable(player, false);
        Console.WriteLine();
        Console.WriteLine($"{player.Name}, you're broke. Get outta here");
        Console.WriteLine();
        Thread.Sleep(4000);
        
        players.RemoveAt(i);
        i--;
      }
    }
    
    private void DealCard(Player player)
    {
      var randomIndex = new Random().Next(Deck.Count);
      var randomCard = Deck[randomIndex];

      player.Cards.Add(randomCard);
      Deck.RemoveAt(randomIndex);
    }
  }
}
