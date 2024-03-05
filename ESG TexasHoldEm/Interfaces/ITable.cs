using TexasHoldEm.Models;

namespace TexasHoldEm.Interfaces
{
  public interface ITable
  {
    List<Player> Players { get; }
    List<int> SidePots { get; }
    decimal SmallBlind { get; set; }
    decimal BigBlind { get; set; }
    int OpenSeats => 9 - Players.Count;
    int SmallBlindIndex { get; set; }
    int BigBlindIndex { get; set; }
    int DealButtonIndex { get; set; }
    int MinimumBet { get; set; }
    int MainPot { get; set; }
  }
}
