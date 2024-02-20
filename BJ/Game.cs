namespace BJ
{
  public class Game
  {
    private static Game? _gameClient;

    public List<Player> Players { get; } = new();
    public Dealer Dealer { get; } = new("Dealer", true);
    
    public Display Display { get; } = new();
    
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
      Dealer.CollectAntes();
      Dealer.DealStartingCards(Players);

      Console.Clear();

      Display.ShowTable(false);
      
      // Check if Dealer has Blackjack
      if (Dealer.Cards.Any(c => c is { IsAce: true, IsFaceDown: false }))
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
      
      // Offer Options to each Player
      foreach (var player in Players)
      {
        Dealer.AskForPlayerOptions(player);
      }

      Console.ReadLine();
    }
  }
}
