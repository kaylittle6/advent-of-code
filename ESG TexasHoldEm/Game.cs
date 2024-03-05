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
      int amountOfPlayers;
      int startingMoney;
      int smallBlind;
      
      Console.Clear();
      Console.WriteLine("How many players will play? (2-4)\n");
  
      while (!int.TryParse(Console.ReadLine(), out amountOfPlayers)
             || amountOfPlayers < 1 || amountOfPlayers > 4)
      {
        Console.Clear();
        Console.WriteLine("Please select a valid amount of Players");
        Thread.Sleep(3000);
        Console.Clear();
        Console.WriteLine("How many players will play? (2-4)\n");
      }
  
      Console.Clear();
      Console.WriteLine("How much money will each players start with? ($1m Maximum)\n");
  
      while (!int.TryParse(Console.ReadLine(), out startingMoney)
             || startingMoney < 1 || startingMoney > 1000000)
      {
        Console.Clear();
        Console.WriteLine("Please select a valid amount of money");
        Thread.Sleep(3000);
        Console.Clear();
        Console.WriteLine("How much money will each player start with? ($1m Maximum)\n");
      }
      
      for (var i = 0; i < amountOfPlayers; i++)
      {
        string? playerName;
        
        Console.Clear();
        Console.WriteLine($"What is player {i + 1}'s name?\n");
        
        while (string.IsNullOrEmpty(playerName = Console.ReadLine()))
        {
          Console.Clear();
          Console.WriteLine("Please select a valid name");
          Thread.Sleep(3000);
          Console.Clear();
          Console.WriteLine($"What is player {i + 1}'s name?\n");
        }
  
        Dealer.Table.Players.Add(new Player(playerName, startingMoney));
      }

      Console.Clear();
      Console.WriteLine($"How much will the Small Blind start at?\n");

      while (!int.TryParse(Console.ReadLine(), out smallBlind)
             || smallBlind < 1 || smallBlind > startingMoney / 2)
      {
        Console.Clear();
        Console.WriteLine("Please select a valid number");
        Thread.Sleep(3000);
        Console.Clear();
        Console.WriteLine("How much will the Small Blind start at?\n");
      }
      
      Dealer.Table.SmallBlind = smallBlind;
      Dealer.Table.BigBlind = smallBlind * 2;
      
      Display.ShowEntireTable();
    }
  }
}