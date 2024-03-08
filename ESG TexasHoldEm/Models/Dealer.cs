using TexasHoldEm.Interfaces;
using TexasHoldEm.Static;

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
    
    private static List<Card> GetDeck()
    {
      string[] values = ["2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace"];
      string[] suits = ["Hearts", "Diamonds", "Spades", "Clubs"];
      
      var newDeck = values.SelectMany(_ => suits, (value, suit) => new Card(value, suit)).ToList();
      
      return newDeck.OrderBy(_ => Guid.NewGuid()).ToList();
    }
  }
}

