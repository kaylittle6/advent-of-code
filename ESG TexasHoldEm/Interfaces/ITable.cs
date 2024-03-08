using TexasHoldEm.Models;

namespace TexasHoldEm.Interfaces;

public interface ITable
{
  List<Player> Players { get; }
  List<Card> CommunityCards { get; set; }
  List<int> SidePots { get; }
  decimal SmallBlind { get; }
  decimal BigBlind => SmallBlind * 2;
  decimal MinimumBet { get; set; }
  decimal MainPot { get; set; }
  int BlindIndex { get; set; }
}