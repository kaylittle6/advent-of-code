using TexasHoldEm.Interfaces;

namespace TexasHoldEm.Models;

public class Card(string cardNumber, string cardSuit) : ICard
{ 
  public string CardSuit { get; } = cardSuit;
  public string CardNumber { get; } = cardNumber;
  public string Display => $"{CardNumber} of {CardSuit}";
  public int CardValue => GetCardValue();

  private int GetCardValue()
  {
    return CardNumber switch
    {
      "Jack" => 11,
      "Queen" => 12,
      "King" => 13,
      "Ace" => 14,
      _ => int.Parse(CardNumber)
    };
  }
  
}