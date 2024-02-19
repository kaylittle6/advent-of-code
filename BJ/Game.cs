namespace BJ
{
  public class Game
  {
    private static Game? _gameClient;

    public List<Player> Players { get; set; } = new List<Player>();
    public Dealer Dealer { get; set; } = new Dealer("Dealer", true);
    public Display Display { get; set; } = new Display();
    
    public static Game GetGameClient => _gameClient ??= new Game();

    public void StartNewGame()
    {
      int p;
      bool goodNop;

      do
      {
        goodNop = true;

        Console.Clear();
        Console.WriteLine("How many players will play? (Maximum 4 players)");
        Console.WriteLine();

        var nOp = Console.ReadLine();

        if (nOp == null || (nOp != "1" && nOp != "2" && nOp != "3" && nOp != "4"))
        {
          Console.Clear();
          Console.WriteLine("Please select a valid response");
          Thread.Sleep(3000);
          goodNop = false;
        }

        p = Int32.Parse(nOp!);

      } while (!goodNop);

      for (int i = 0; i < p; i++)
      {
        Console.Clear();
        Console.WriteLine("What is this Player's name?");
        Console.WriteLine();

        var playerName = Console.ReadLine();

        Console.Clear();
        Console.WriteLine("Please select a starting amount of money (Numbers Only)");
        Console.WriteLine();

        var money = Console.ReadLine();

        Player player = new(playerName!, false)
        {
          CurrentMoney = int.Parse(money!)
        };
        
        Players.Add(player);
      }

      Players.Add(Dealer);

      Console.Clear();
      Console.WriteLine("How many decks would you like to play with?");
      Console.WriteLine();

      Dealer.DeckCount = int.Parse(Console.ReadLine()!);
      Dealer.GetDeck(Dealer.DeckCount);

      Console.Clear();
      Console.WriteLine("What is the minimum bet for this table?");
      Console.WriteLine();

      Dealer.MinimumBet = int.Parse(Console.ReadLine()!);

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
        foreach (var player in Players)
        {
          Dealer.OfferInsurance(player);
          Dealer.FlipFaceDownCard();
          Display.ShowTable(player, true);

          switch (Dealer.HasBlackJack)
          {
            case true when player is { HasBlackJack: false, HasInsurance: true }:
              player.CurrentMoney += player.CurrentBet / 2;
              Console.WriteLine();
              Console.WriteLine($"The Dealer has Blackjack, {player.Name}");
              Console.WriteLine();
              Console.WriteLine($"{player.Name} has Insurance, though! They win {player.CurrentBet / 2} dollars");
              player.CurrentMoney += player.CurrentBet;
              Dealer.Rules.ResetPlayer(player);
              Thread.Sleep(4000);
              break;
            case true when player is { HasBlackJack: false, HasInsurance: false }:
              Console.WriteLine();
              Console.WriteLine($"The Dealer has Blackjack, {player.Name}");
              Console.WriteLine();
              Console.WriteLine($"Sorry, {player.Name}. You don't have Insurance. You lose.");
              Dealer.Rules.ResetPlayer(player);
              Thread.Sleep(4000);
              break;
          }
        }
      }

      Dealer.Rules.CheckAndResolveBlackJack();


      Console.ReadLine();
    }
  }
}
