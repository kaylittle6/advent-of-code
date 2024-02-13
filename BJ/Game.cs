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

      Display.ShowTable(this);
    }

    public void CommenceRound()
    {
      var dealer = Players.Where(p => p.Name == "Dealer").FirstOrDefault()!;

      Dealer.CollectAntes(this);
      Dealer.DealStartingCards(Players);

      Console.Clear();

      Display.ShowTable(this);
      Dealer.Rules.CheckAndResolveBlackJack(this);

      // Check for Insurance
      if (dealer.Cards.Any(c => c.CardNumber == "Ace" && !c.IsFaceDown))
      {
        Dealer.Rules.CheckAndIssueInsurance(Players);
        dealer.Cards[0].IsFaceDown = false;

        Console.Clear();
        Display.ShowTable(this);
        Console.WriteLine();
        
        var dealerWin = Dealer.Rules.CheckForResult(dealer);

        // If Dealer has 21
        if (dealerWin[1] == true)
        {

        }
      }

      // Start round
      foreach (var player in Players)
      {
        // Check for Split Hand
        if (player.Cards[0].CardNumber == player.Cards[1].CardNumber)
        {
          bool goodResp = true;

          do
          {
            Console.WriteLine($"{player.Name}, would you like to split you hand?");
            Console.WriteLine();

            var splitHand = Console.ReadLine()?.ToLower();

            if (splitHand != null)
            {
              if (splitHand == "yes")
              {
                Dealer.Rules.PlaySplitHand(player);
              }
              else
              {
                continue;
              }
            }
            else
            {
              Console.WriteLine("Please select a valid response");
              Thread.Sleep(3000);
              goodResp = false;
            }

          } while (!goodResp);
          

          
        }


      }


    }
  }
}
