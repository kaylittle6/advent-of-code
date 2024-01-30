namespace BJ
{
  public class Game
  {
    private static Game? gameClient;

    public List<Player> Players { get; set; } = new List<Player>();
    public Dealer Dealer { get; set; } = new Dealer();
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

    public void StartNewGame()
    {
      var goodNOP = false;
      int p;

      do
      {
        Console.WriteLine();
        Console.WriteLine("How many players will play? (Maximum 4 players)");
        Console.WriteLine();

        var nOP = Console.ReadLine();

        if (nOP == null && nOP != "1" && nOP != "2" && nOP != "3" && nOP != "4")
        {
          Console.WriteLine();
          Console.WriteLine("Please select a valid response");
        }
        else
        {
          goodNOP = true;
        }

        p = Int32.Parse(nOP!);

      } while (goodNOP == false);

      for (int i = 0; i < p; i++)
      {
        Console.WriteLine();
        Console.WriteLine("What is this Player's name?");
        Console.WriteLine();

        var playerName = Console.ReadLine();

        Console.WriteLine();
        Console.WriteLine("Please select a starting amount of money (Numbers Only)");
        Console.WriteLine();

        var money = Console.ReadLine();

        Player player = new(playerName!);
        player.CurrentMoney = Int32.Parse(money!);
        Players.Add(player);
      }
    }
  }
}
