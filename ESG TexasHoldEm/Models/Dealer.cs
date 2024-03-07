using TexasHoldEm.Interfaces;
using TexasHoldEm.Static;
using TexasHoldEm.Utilities;

namespace TexasHoldEm.Models
{
  public class Dealer : IDealer
  {
    public List<Card> Deck
    {
      get => GetDeck();
      set => throw new NotImplementedException();
    }

    public Table Table { get; set; } = new();
    public int HandsPlayed { get; set; } = 0;
    
  
    public void CollectBets()
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
    
    public void DealHoleCards()
    {
      foreach (var player in Table.Players)
      {
        DealCards(hand: player.Hand, 2);
      }
    }
    
    public void RoundOfBets(List<Player> players, GameStates gameState)
    {
      
    }
    
    public void DealFlop()
    {
      throw new NotImplementedException();
    }
    
    public void DealTurnOrRiver()
    {
      throw new NotImplementedException();
    }
  
    public void FinishUpRound(List<Player> players)
    {
      throw new NotImplementedException();
    }
  
    public void RemoveBrokeAssPlayers(List<Player> players)
    {
      throw new NotImplementedException();
    }

    private void DealCards(ICollection<Card> hand, int cardsToDeal)
    {
      for (var i = 0; i < cardsToDeal; i++)
      {
        var randomIndex = new Random().Next(Deck.Count);
        var randomCard = Deck[randomIndex];

        hand.Add(randomCard);
        Deck.RemoveAt(randomIndex);
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
}

