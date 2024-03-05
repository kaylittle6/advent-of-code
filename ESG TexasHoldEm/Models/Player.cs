using TexasHoldEm.Interfaces;

namespace TexasHoldEm.Models;

public class Player(string? name, int money) : IPlayer
{
  public string? Name { get; set; } = name;
  public List<Card> Hand { get; set; } = [];
  public decimal Money { get; set; } = money;
  public bool InHand { get; set; } = true;
}