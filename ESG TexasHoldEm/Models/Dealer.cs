using TexasHoldEm.Interfaces;
using TexasHoldEm.Static;

namespace TexasHoldEm.Models;

public class Dealer : IDealer
{
  public List<Card> Deck
  {
    get => GetDeck();
    set => throw new NotImplementedException();
  }

  public Table Table { get; set; } = new();
  public int HandsPlayed { get; set; } = 0;
    
  
  public void CollectBlinds()
  {
    var smallBlindPlayer = Table.Players[Table.BlindIndex];
    var bigBlindPlayer = Table.Players[Table.BlindIndex + 1];
      
    Display.ShowEntireTable();
    Console.WriteLine("Collecting bets...\n");
    Thread.Sleep(3000);
    Console.WriteLine($"{smallBlindPlayer.Name} pays {Table.SmallBlind:C2}");
    Console.WriteLine($"{bigBlindPlayer.Name} pays {Table.BigBlind:C2}");
    smallBlindPlayer.Money -= Table.SmallBlind;
    bigBlindPlayer.Money -= Table.BigBlind;
    smallBlindPlayer.CurrentBet += Table.SmallBlind;
    bigBlindPlayer.CurrentBet += Table.BigBlind;
    Thread.Sleep(3000);
  }
    
  public void RoundOfBets(List<Player> players)
  {
    var inHandPlayers = players.Where(p => p.InHand).ToList();

    for (int i = Table.BlindIndex + 2, pc = 0; pc < inHandPlayers.Count; i++, pc++)
    {
      if (i >= inHandPlayers.Count)
      {
        i -= inHandPlayers.Count;
      }
      
      var activePlayer = inHandPlayers.Count < 3 
        ? inHandPlayers[Table.BlindIndex] 
        : inHandPlayers[i];

      if (!activePlayer.IsNpc)
      {
        int pick;
        
        do
        {
          Console.Clear();
          Console.WriteLine($"{activePlayer.Name}, it's your action, what would you like to do?\n");
          Console.WriteLine($"1. Call ({Table.MinimumBet:C2})");
          Console.WriteLine("2. Raise");
          Console.WriteLine("3. Fold\n");
        } while (!int.TryParse(Console.ReadLine(), out pick)
                 && pick != 1 && pick != 2 && pick != 3);
        
        ExecutePick(activePlayer, pick);
      }
      else
      {
        var stayingInHand = activePlayer.RaiseOrCallableHand();

        if (stayingInHand)
        {
          Random random = new();
          var callOrRaise = random.Next(0, 1);
          
          ExecutePick(activePlayer, callOrRaise);
        }
        else
        {
          ExecutePick(activePlayer, 3);
        }
      }
      
      Thread.Sleep(4000);
    }
  }
    
  public void DealHoleCards()
  {
    foreach (var player in Table.Players)
    {
      DealCards(player.Hand, 2);
    }
  }
    
  public void DealFlop()
  {
    DealCards(Table.CommunityCards, 3);
  }
    
  public void DealTurnOrRiver()
  {
    DealCards(Table.CommunityCards, 1);
  }
  
  public void FinishUpRound(List<Player> players)
  {
    throw new NotImplementedException();
  }
  
  public void RemoveBrokeAssPlayers(List<Player> players)
  {
    throw new NotImplementedException();
  }

  private void DealCards(List<Card> hand, int cardsToDeal)
  {
    for (var i = 0; i < cardsToDeal; i++)
    {
      var randomIndex = new Random().Next(Deck.Count);
      var randomCard = Deck[randomIndex];

      hand.Add(randomCard);
      Deck.RemoveAt(randomIndex);
    }
  }

  private decimal GetRaise(Player player)
  {
    if (player.IsNpc)
    {
      return Table.MinimumBet * 2;
    }
    
    int raise;
    
    do
    {
      Console.WriteLine($"How much you would like to raise, {player.Name}?\t" +
                        $"(Minimum Raise: {Table.MinimumBet * 2:C2})\n");
    } while (!int.TryParse(Console.ReadLine(), out raise)
             && raise < Table.MinimumBet * 2);

    return raise;
  }

  private void ExecutePick(Player player, int pick)
  {
    switch (pick)
    {
      case 1:
        player.Money -= Table.MinimumBet;
        player.CurrentBet += Table.MinimumBet;
        Table.MainPot += Table.MinimumBet;
        Display.ShowPlayerActions(player, 1);
        break;
      case 2:
        var raise = GetRaise(player);
        player.Money -= raise;
        player.CurrentBet += raise;
        Table.MainPot += raise;
        Table.MinimumBet = raise;
        Display.ShowPlayerActions(player, 2);
        break;
      case 3:
        player.InHand = false;
        player.Hand.Clear();
        player.CurrentBet = 0;
        Display.ShowPlayerActions(player, 3);
        break;
    }
  }
    
  private static List<Card> GetDeck()
  {
    string[] values = ["2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace"];
    string[] suits = ["Hearts", "Diamonds", "Spades", "Clubs"];
      
    var newDeck = values.SelectMany(_ => suits, (value, suit) => new Card(value, suit)).ToList();
      
    return newDeck.OrderBy(_ => Guid.NewGuid()).ToList();
  }
}