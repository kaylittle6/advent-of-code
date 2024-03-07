using TexasHoldEm.Models;

namespace TexasHoldEm.Interfaces
{
  public interface ITable
  {
    List<Player> Players { get; }
    List<int> SidePots { get; }
    decimal SmallBlind { get; set; }
    decimal BigBlind => SmallBlind * 2;
    decimal MinimumBet { get; set; }
    int BlindIndex { get; set; }
    int MainPot { get; set; }
  }
}
