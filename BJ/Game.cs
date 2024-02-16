namespace BJ
{
  public class Game
  {
    private static Game? gameClient;

    public List<Player> Players { get; set; } = new List<Player>();
    public Dealer Dealer { get; set; } = new Dealer("Dealer", true);
    public Display Display { get; set; } = new Display();

    public Game() { }

    public static Game GetGameClient
    {
      get
      {
        if (gameClient == null)
        {
          gameClient = new Game();
        }

        return gameClient;
      }
    }

    public void StartNewGame()
    {
      int p;
      bool goodNOP;

      do
      {
        goodNOP = true;

        Console.Clear();
        Console.WriteLine("How many players will play? (Maximum 4 players)");
        Console.WriteLine();

        var nOP = Console.ReadLine();

        if (nOP == null || (nOP != "1" && nOP != "2" && nOP != "3" && nOP != "4"))
        {
          Console.Clear();
          Console.WriteLine("Please select a valid response");
          Thread.Sleep(3000);
          goodNOP = false;
        }

        p = Int32.Parse(nOP!);

      } while (!goodNOP);

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

        Player player = new(playerName!);
        player.CurrentMoney = int.Parse(money!);
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
      if (Dealer.Cards.Any(c => c.IsAce && !c.IsFaceDown))
      {
        foreach (var player in Players)
        {
          Dealer.OfferInsurance(player);
          Dealer.FlipFaceDownCard();
          Display.ShowTable(player, true);

          if (Dealer.HasBlackJack && !player.HasBlackJack && player.HasInsurance)
          {
            player.CurrentMoney += player.CurrentBet / 2;
            Console.WriteLine();
            Console.WriteLine($"The Dealer has Blackjack, {player.Name}");
            Console.WriteLine();
            Console.WriteLine($"{player.Name} has Insurance, though! They win {player.CurrentBet / 2} dollars");
            Dealer.Rules.ResetPlayer(player);
            Thread.Sleep(4000);
          }
          else if (Dealer.HasBlackJack && !player.HasBlackJack && !player.HasInsurance)
          {
            Console.WriteLine();
            Console.WriteLine($"The Dealer has Blackjack, {player.Name}");
            Console.WriteLine();
            Console.WriteLine($"Sorry, {player.Name}. You don't have Insurance. You lose.");
            Dealer.Rules.ResetPlayer(player);
            Thread.Sleep(4000);
          }


        }
      }

      Dealer.Rules.CheckAndResolveBlackJack();


      Console.ReadLine();
    }
  }
}
