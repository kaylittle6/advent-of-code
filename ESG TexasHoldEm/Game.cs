using TexasHoldEm.Models;
using TexasHoldEm.Static;

namespace TexasHoldEm
{
  public class Game
  {
    private static Game? _gameClient;

    public Dealer Dealer { get; set; } = new();

    public static Game GetGameClient => _gameClient ??= new Game();

    public void StartNewGame()
    {
      Console.Clear();
      Console.WriteLine($"What is your player's name?\n");
      
      var playerName = Console.ReadLine();
      
      while (string.IsNullOrEmpty(playerName))
      {
        Console.Clear();
        Console.WriteLine($"Please select a valid name.\n");
        playerName = Console.ReadLine();
      }
      
      Console.Clear();
      Console.WriteLine("How many opponents will play against? (1-3)\n");
      var amountOfPlayers = GetUserInput(1, 3);
      
      Console.Clear();
      Console.WriteLine("How much money will each player start with? ($1m Maximum)\n");
      var startingMoney = GetUserInput(1, 1000000);
      
      Dealer.Table.Players.Add(new Player(playerName, startingMoney) { IsNpc = false });

      for (var i = 0; i < amountOfPlayers; i++)
      {
        string npcName;
        
        do
        {
          npcName = PlayerNames.PickName();
        } while (Dealer.Table.Players.Any(p => p.Name == npcName));

        Dealer.Table.Players.Add(new Player(npcName, startingMoney) { IsNpc = true });
      }
      
      Console.Clear();
      Console.WriteLine($"How much will the Small Blind start at?\n");
      var smallBlind = GetUserInput(1, startingMoney / 2);

      Dealer.Table.SmallBlind = smallBlind;
      Dealer.Table.MinimumBet = Dealer.Table.BigBlind;

      Display.ShowEntireTable();
    }

    public void CommenceRound()
    {
      Dealer.CollectBets();
      Dealer.DealHoleCards();
      Display.ShowEntireTable();
      
      
    }

    private static int GetUserInput(int min, int max)
    {
      int input;
      while (!int.TryParse(Console.ReadLine(), out input) || input < min || input > max)
      {
        Console.Clear();
        Console.WriteLine("Please enter a valid input");
        Thread.Sleep(3000);
        Console.Clear();
        Console.WriteLine($"Enter a value between {min} and {max}\n");
      }

      return input;
    }
  }
}