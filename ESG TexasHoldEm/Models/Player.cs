using TexasHoldEm.Interfaces;

namespace TexasHoldEm.Models;

public class Player(string? name, int money) : IPlayer
{
  public string? Name { get; set; } = name;
  public List<Card> Hand { get; set; } = [];
  public decimal Money { get; set; } = money;
  public decimal CurrentBet { get; set; }
  public bool InHand { get; set; } = true;
  public bool IsNpc { get; set; }

  public bool RaiseOrCallableHand()
  {
    return Hand[0].CardNumber == Hand[1].CardNumber
           || Hand[0].CardSuit == Hand[1].CardSuit
           || Hand.Sum(c => c.CardValue) >= 20
           || Math.Abs(Hand[0].CardValue - Hand[1].CardValue) == 1;
  }
}