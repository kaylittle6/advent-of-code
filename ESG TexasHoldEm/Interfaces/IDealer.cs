using TexasHoldEm.Models;
using TexasHoldEm.Utilities;

namespace TexasHoldEm.Interfaces
{
  public interface IDealer
  {
    List<Card> Deck { get; set; }
    Table Table { get; set; }
    int HandsPlayed { get; set; }
    void CollectBets();
    void DealHoleCards();
    void RoundOfBets(List<Player> players);
    void DealFlop();
    void DealTurnOrRiver();
    void FinishUpRound(List<Player> players);
    void RemoveBrokeAssPlayers(List<Player> players);
  }
}

