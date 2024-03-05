using TexasHoldEm.Models;

namespace TexasHoldEm.Interfaces
{
  public interface IDealer
  {
    List<Card> Deck { get; set; }
    Table Table { get; set; }
    int HandsPlayed { get; set; }
    
    List<Card> GetDeck();
    void CollectBets();
    void AskPlayerOptions(Player player);
    void DealHoleCards(List<Player> players);
    void DealFlop();
    void DealTurnOrRiver();
    void FinishUpRound(List<Player> players);
    void RemoveBrokeAssPlayers(List<Player> players);
  }
}

