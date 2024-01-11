namespace MiscPractice
{
  public class Game
  {
    private static Game? mainGame;

    public List<Player> Players { get; set; }
    public List<Card> CommunityCards { get; set; }
    public Dictionary<string, Card>? Deck { get; set; }
    
    public Game()
    {
      Players = new List<Player>();
      CommunityCards = new List<Card>();
      Deck = new Dictionary<string, Card>();
    }

    public static Game MainGame
    {
      get
      {
        if (mainGame == null)
        {
          mainGame = new Game();
        }

        return mainGame;
      }
    }

    public void StartNewGame()
    {
      var pc = false;
      int pn;

      do
      {
        Console.WriteLine();
        Console.WriteLine("How many players will play? (Please select between 4 and 8)");
        Console.WriteLine();

        var p = Console.ReadLine();

        if (p == null && p != "4" && p != "5" && p != "6" && p!= "7" && p != "8")
        {
          Console.WriteLine("Please select a valid response");
        }
        else
        {
          pc = true;
        }

        pn = Int32.Parse(p!);

      } while (pc == false);

      for (int i = 0; i < pn; i++)
      {
        Console.WriteLine();
        Console.WriteLine("What is this Player's name?");
        Console.WriteLine();

        var playerName = Console.ReadLine();

        Player newPlayer = new(playerName!);

        Console.WriteLine("Is this Player a NPC? (yes/no)");
        Console.WriteLine();
        
        var npc = Console.ReadLine();

        newPlayer.IsNPC = npc == "yes" || npc == "y" ? true : false;

        Players.Add(newPlayer);
      }
    }
  }
}
