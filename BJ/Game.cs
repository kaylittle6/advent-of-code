namespace BJ
{
  public class Game
  {
    private static Game? _gameClient;

    public List<Player> Players { get; } = new();
    public Dealer Dealer { get; } = new("Dealer", true);
    
    public static Game GetGameClient => _gameClient ??= new Game();

    public void StartNewGame()
    {
      int p;

       Console.Clear();
       Console.WriteLine("How many players will play? (Maximum 4 players)");
       Console.WriteLine();

       while (!int.TryParse(Console.ReadLine(), out p) || p < 1 || p > 4)
       {
         Console.Clear();
         Console.WriteLine("Please select a valid response");
         Thread.Sleep(3000);
         Console.Clear();
         Console.WriteLine("How many players will play? (Maximum 4 players)");
         Console.WriteLine();
       }
     
       for (var i = 0; i < p; i++)
       {
         int money;
         
         Console.Clear();
         Console.WriteLine("What is this Player's name?");
         Console.WriteLine();
     
         var name = Console.ReadLine();
          
         Console.Clear();
         Console.WriteLine("Please select a starting amount of money (Numbers Only)");
         Console.WriteLine();

         while (!int.TryParse(Console.ReadLine(), out money))
         {
           Console.Clear();
           Console.WriteLine("Please enter a valid amount");
           Console.WriteLine("Please select a starting amount of money (Numbers Only)");
           Console.WriteLine();
         }
     
         Players.Add(new Player(name!, false) { CurrentMoney = money });
       }
     
       Players.Add(Dealer);
     
       Console.Clear();
       Console.WriteLine("How many decks would you like to play with?");
       Console.WriteLine();
     
       int deckCount;
       
       while (!int.TryParse(Console.ReadLine(), out deckCount))
       {
         Console.Clear();
         Console.WriteLine("Please enter a valid number of decks");
         Console.WriteLine("How many decks would you like to play with?");
         Console.WriteLine();
       }
       
       Dealer.DeckCount = deckCount;
       Dealer.GetDeck(Dealer.DeckCount);
     
       Console.Clear();
       Console.WriteLine("What is the minimum bet for this table?");
       Console.WriteLine();
     
       int minimumBet;
       
       while (!int.TryParse(Console.ReadLine(), out minimumBet))
       {
         Console.Clear();
         Console.WriteLine("Please enter a valid minimum bet");
         Console.WriteLine("What is the minimum bet for this table?");
         Console.WriteLine();
       }
       
       Dealer.MinimumBet = minimumBet;
     
       Display.ShowTable(false);
     }

    public void CommenceRound()
    {
      do
      {
        Dealer.CollectAntes();
        Dealer.DealStartingCards(Players);

        Console.Clear();

        Display.ShowTable(false);
        
        // Check if Dealer has Blackjack
        if (Dealer.Cards.Any(c => c is { CardValue: 11, IsFaceDown: false }))
        {
          Console.Clear();

          foreach (var player in Players)
          {
            Display.ShowTable(player, true);
            Dealer.OfferInsurance(player);
          }

          if (Dealer.Cards[0].CardValue == 10)
          {
            Dealer.Cards[0].IsFaceDown = false;
            Console.Clear();
            Display.ShowTable(true);
            Console.WriteLine();
            Console.WriteLine("The Dealer has Blackjack, let's look at the Players");
            Thread.Sleep(3000);

            foreach (var player in Players)
            {
              if (player is { HasBlackJack: false, HasInsurance: true })
              {
                Console.WriteLine();
                Console.WriteLine($"{player.Name} has Insurance! Smart. You win {player.CurrentBet / 2} dollars");
                player.CurrentMoney += player.CurrentBet / 2;
                RuleBook.ResetPlayer(player);
                Thread.Sleep(4000);
              }
              if (player is { HasBlackJack: false, HasInsurance: false })
              {
                Console.WriteLine();
                Console.WriteLine($"Sorry, {player.Name}. You don't have Insurance. You lose.");
                RuleBook.ResetPlayer(player);
                Thread.Sleep(4000);
              }
              if (player.HasBlackJack)
              {
                RuleBook.CheckAndResolveBlackJack(true);
              }
            }
          }
        }

        RuleBook.CheckAndResolveBlackJack(false);
        
        foreach (var player in Players)
        {
          Dealer.AskForPlayerOptions(player);
        }

        if (!Players.Any(p => p is { InHand: true, IsDealer: false }))
        {
          Console.Clear();
          Console.WriteLine("Well shit, nobody is left, guess we'll do it again");
          Dealer.RemoveBrokeAssPlayers(Players);
          Dealer.FinishUpRound(Players);
          Thread.Sleep(4000);
          continue;
        }

        Dealer.Cards[0].IsFaceDown = false;
        
        if (Dealer.HandValue < 17)
        {
          Dealer.DealerAction();
        }
        else
        {
          Console.Clear();
          Display.ShowTable(true);
          Console.WriteLine();
          Console.WriteLine($"Dealer stands with {Dealer.HandValue}");
          Thread.Sleep(4000);
        }

        var dealerResult = RuleBook.CheckHand(Dealer);

        if (dealerResult == RuleBook.HandResult.HandBusted)
        {
          foreach (var player in Players.Where(player => player is { InHand: true, IsDealer: false }))
          {
            Console.Clear();
            Display.ShowTable(player, true);
            Console.WriteLine($"The Dealer busted, {player.Name}.");
            Console.WriteLine();
            Console.WriteLine($"You win {player.CurrentBet * 2:C2} dollars.");
            RuleBook.WinStandardBet(player);
            RuleBook.ResetPlayer(player);
            Thread.Sleep(4000);
          }
        }

        foreach (var player in Players.Where(player => player is { InHand: true, IsDealer: false }))
        {
          Console.Clear();
          Display.ShowTable(player, true);
          Console.WriteLine($"Dealer has {Dealer.HandValue}, you have {player.HandValue}, {player.Name}.");
          Console.WriteLine();
          
          if (player.HandValue > Dealer.HandValue)
          {
            Console.WriteLine($"You win {player.CurrentBet * 2:C2} dollars!");
            RuleBook.WinStandardBet(player);
            RuleBook.ResetPlayer(player);
            Thread.Sleep(4000);
          }
          else if (player.HandValue < Dealer.HandValue)
          {
            Console.WriteLine($"You lose. {player.CurrentBet:C2} total in fact.");
            RuleBook.ResetPlayer(player);
            Thread.Sleep(4000);
          }
          else
          {
            Console.WriteLine($"Push. You get your money back, all {player.CurrentBet:C2}.");
            RuleBook.PushBet(player);
            RuleBook.ResetPlayer(player);
            Thread.Sleep(4000);
          }
        }
        
        Dealer.RemoveBrokeAssPlayers(Players);
        Dealer.FinishUpRound(Players);
        
      } while (Players.Any(player => !player.IsDealer));
    }
  }
}
