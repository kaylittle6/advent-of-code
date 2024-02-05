namespace BJ
{
  public class Game
  {
    private static Game? gameClient;

    public List<Player> Players { get; set; } = new List<Player>();
    public Dealer Dealer { get; set; } = new Dealer();
    public Display Display { get; set; } = new Display();
    public RuleBook Rules { get; set; } = new RuleBook();
    public int MinimumBet { get; set; }

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

    public void StartNewGame(Dealer dealer)
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

      } while (goodNOP == false);

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
        player.CurrentMoney = Int32.Parse(money!);
        Players.Add(player);
      }

      Players.Add(new Player("Dealer"));

      Console.Clear();
      Console.WriteLine("How many decks would you like to play with?");
      Console.WriteLine();

      Dealer.DeckCount = Int32.Parse(Console.ReadLine()!);

      Dealer.GetDeck(Dealer.DeckCount);

      Console.Clear();
      Console.WriteLine("What is the minimum bet for this table?");
      Console.WriteLine();

      MinimumBet = Int32.Parse(Console.ReadLine()!);
    }

    public void NextRound()
    {
      bool goodHOS;
      bool stay = false;
      string hitOrStay;
      bool[] results = new bool[3];

      Display.ShowTable(this);
      Console.WriteLine();
      Console.WriteLine();

      foreach (var player in Players)
      {
        if (player.Name != "Dealer")
        {
          do
          {
            do
            {
              goodHOS = true;

              Console.WriteLine();
              Console.WriteLine($"{player.Name}, would you like to hit or stay? (hit/stay)");
              Console.WriteLine();

              hitOrStay = Console.ReadLine()!;

              if (hitOrStay == null || (hitOrStay != "hit" && hitOrStay != "Hit" && hitOrStay != "stay"
                && hitOrStay != "Stay" && hitOrStay != "h" && hitOrStay != "s"))
              {
                Console.Clear();
                Console.WriteLine("Please select a valid response");
                Thread.Sleep(3000);
                goodHOS = false;
              }
            } while (goodHOS == false);

            if (hitOrStay == "hit" || hitOrStay == "Hit" || hitOrStay == "h")
            {
              Dealer.DealCard(player);
              player.CheckAceValue();
            }
            else
            {
              stay = true;
            }

            results = Rules.CheckForResult(player);

          } while (stay == false);
        }
        else
        {
          Dealer.MakeSmartMove(Players);
        }
      }
    }
  }
}
