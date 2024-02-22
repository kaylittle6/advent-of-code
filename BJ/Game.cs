using System.Data;

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
          Dealer.FlipFaceDownCard();
          
          foreach (var player in Players)
          {
            Display.ShowTable(player, true);
            Dealer.OfferInsurance(player);
          }

          foreach (var player in Players)
          {
            switch (Dealer.HasBlackJack)
            {
              case true when player is { HasBlackJack: false, HasInsurance: true }:
                Console.WriteLine();
                Console.WriteLine($"The Dealer has Blackjack, {player.Name}");
                Console.WriteLine();
                Console.WriteLine($"{player.Name} has Insurance, though! They win {player.CurrentBet / 2} dollars");
                player.CurrentMoney += player.CurrentBet / 2;
                RuleBook.ResetPlayer(player);
                Thread.Sleep(4000);
                break;
              case true when player is { HasBlackJack: false, HasInsurance: false }:
                Console.WriteLine();
                Console.WriteLine($"The Dealer has Blackjack, {player.Name}");
                Console.WriteLine();
                Console.WriteLine($"Sorry, {player.Name}. You don't have Insurance. You lose.");
                RuleBook.ResetPlayer(player);
                Thread.Sleep(4000);
                break;
            }
          }
          
          RuleBook.CheckAndResolveBlackJack();
          return;
        }

        RuleBook.CheckAndResolveBlackJack();
        
        foreach (var player in Players)
        {
          Dealer.AskForPlayerOptions(player);
        }

        if (Dealer.HandValue < 17)
        {
          Dealer.DealerAction();
        }

        var dealerResult = RuleBook.CheckHand(Dealer);

        if (dealerResult == RuleBook.HandResult.HandBusted)
        {
          foreach (var player in Players.Where(player => player.InHand))
          {
            Console.Clear();
            Console.WriteLine($"The Dealer busted, {player.Name}.");
            Console.WriteLine();
            Console.WriteLine($"You win {player.CurrentBet * 2} dollars.");
            RuleBook.WinStandardBet(player);
            RuleBook.ResetPlayer(player);
          }
        }
        
        foreach (var player in Players.Where(player => player is { InHand: true, IsDealer: false }))
        {
          Console.Clear();
          Display.ShowTable(player, true);
          Console.WriteLine();
          Console.WriteLine($"Dealer has {Dealer.HandValue}, you have {player.HandValue}, {player.Name}.");
          Console.WriteLine();
          
          if (player.HandValue > Dealer.HandValue)
          {
            Console.WriteLine($"You win {player.CurrentBet * 2} dollars!");
            RuleBook.WinStandardBet(player);
            RuleBook.ResetPlayer(player);
          }
          else if (player.HandValue < Dealer.HandValue)
          {
            Console.WriteLine($"You lose, {player.Name}. {player.CurrentBet} total in fact.");
            RuleBook.ResetPlayer(player);
          }
          else
          {
            Console.WriteLine($"Push. You get your money back, {player.Name}.");
            RuleBook.PushBet(player);
            RuleBook.ResetPlayer(player);
          }
        }

        if (Dealer.NeedsReshuffle)
        {
          Dealer.GetDeck(Dealer.DeckCount);
        }
      } while (Players.Any(p => p.InHand));
    }
  }
}
