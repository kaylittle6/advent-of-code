using TexasHoldEm.Interfaces;

namespace TexasHoldEm.Models
{
  public class Table : ITable
  {
    public List<Player> Players { get; set; } = [];
    public List<int> SidePots { get; set; } = [];
    public decimal SmallBlind { get; set; }
    public decimal BigBlind { get; set; }
    public int OpenSeats => 9 - Players.Count;
    public int SmallBlindIndex { get; set; }
    public int BigBlindIndex { get; set; }
    public int DealButtonIndex { get; set; }
    public int MinimumBet { get; set; }
    public int MainPot { get; set; }
  }
}
