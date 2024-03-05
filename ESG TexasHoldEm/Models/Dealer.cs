using TexasHoldEm.Interfaces;

namespace TexasHoldEm.Models;

public class Dealer : IDealer
{
  public List<Card> Deck
  {
    get => ((IDealer)this).GetDeck();
    set => throw new NotImplementedException();
  }
  
  public Table Table { get; set; } = new();
  public int HandsPlayed { get; set; } = 0;
  
  List<Card> IDealer.GetDeck()
  {
    string[] values = ["2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace"];
    string[] suits = ["Hearts", "Diamonds", "Spades", "Clubs"];
    
    var newDeck = values.SelectMany(_ => suits, (value, suit) => new Card(value, suit)).ToList();
    
    return newDeck.OrderBy(_ => Guid.NewGuid()).ToList();
  }

  public void CollectBets()
  {
    var game = Game.GetGameClient;
    
    

  }

  public void AskPlayerOptions(Player player)
  {
    throw new NotImplementedException();
  }

  public void DealHoleCards(List<Player> players)
  {
    throw new NotImplementedException();
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
}