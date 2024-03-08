using TexasHoldEm.Interfaces;

namespace TexasHoldEm.Models
{
  public class Table : ITable
  {
    public List<Player> Players { get; } = [];
    public List<Card> CommunityCards { get; set; } = [];
    public List<int> SidePots { get; } = [];
    public decimal SmallBlind { get; set; }
    public decimal BigBlind => SmallBlind * 2;
    public int BlindIndex { get; set; } = 0;
    public int MainPot { get; set; }
  }
}
