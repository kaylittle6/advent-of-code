using TexasHoldEm.Interfaces;

namespace TexasHoldEm.Models
{
  public class Table : ITable
  {
    public List<Player> Players { get; } = [];
    public List<int> SidePots { get; set; } = [];
    public decimal SmallBlind { get; set; }
    public decimal BigBlind => SmallBlind * 2;
    public decimal MinimumBet { get; set; }
    public int BlindIndex { get; set; } = 0;
    public int MainPot { get; set; }

    private void MoveBlindIndex()
    {
      BlindIndex = BlindIndex + 1 > Players.Count ? BlindIndex = 0 : BlindIndex++;
    }
  }
}
