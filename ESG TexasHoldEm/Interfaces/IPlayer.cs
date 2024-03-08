using TexasHoldEm.Models;

namespace TexasHoldEm.Interfaces;

public interface IPlayer
{
  string? Name { get; set; }
  List<Card> Hand { get; set; }
  decimal Money { get; set; }
  decimal CurrentBet { get; set; }
  bool InHand { get; set; }
  bool IsNpc { get; set; }
}